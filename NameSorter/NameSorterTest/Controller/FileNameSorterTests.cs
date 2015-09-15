using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using NameSorter.Common;
using NameSorter.DataProvider;
using NameSorter.Model;

namespace NameSorterTest.Controller
{
    [TestClass]
    public class FileNameSorterTests
    {
        private const string InputFileName = "names.txt";
        private const string OutputFileName = "names-sorted.txt";
        private Mock<INameSorter> _nameSorter;
        private Mock<FileNameSorterWrapper> _target;
        private Mock<ITextFileReaderWritter> _textFileHandler;

        [TestInitialize]
        public void Intialize()
        {
            _textFileHandler = new Mock<ITextFileReaderWritter>();
            _nameSorter = new Mock<INameSorter>();

            _target = new Mock<FileNameSorterWrapper>(InputFileName, OutputFileName)
            {
                CallBase = true
            };
        }

        [TestMethod]
        [DisplayName("Test Perform sort basic flow")]
        public void PerformSort()
        {
            //Arrange
            _target.Protected()
                .SetupGet<ITextFileReaderWritter>("TextFileReaderWritter")
                .Returns(_textFileHandler.Object);
            _target.Protected().SetupGet<INameSorter>("NameSorterWrapper").Returns(_nameSorter.Object);

            _textFileHandler.Setup(t => t.ReadAll(It.IsAny<string>()))
                .Returns(It.IsAny<List<NameModel>>()).Verifiable();

            _nameSorter.Setup(n => n.SortNameByLastAndFirstName(It.IsAny<List<NameModel>>()))
                .Returns(It.IsAny<List<NameModel>>()).Verifiable();

            _textFileHandler.Setup(t => t.WriteAll(It.IsAny<List<NameModel>>(), It.IsAny<string>()))
                .Verifiable();

            //Act
            _target.Object.PerformSortWrapper();

            //Assert
            _target.Verify();
            _textFileHandler.Verify();
            _nameSorter.Verify();
        }

        [TestMethod]
        public void PerformSort_EmptyFile()
        {
            //Arrange

            _target.Protected().SetupGet<ITextFileReaderWritter>("TextFileReaderWritter")
                .Returns(_textFileHandler.Object).Verifiable();
            _textFileHandler.Setup(t => t.ReadAll(It.IsAny<string>()))
                .Returns(It.IsAny<IList<NameModel>>()).Verifiable();

            //Act
            _target.Object.PerformSortWrapper();

            //Assert
            _target.Verify();
            _textFileHandler.Verify();
        }

        [TestMethod]
        public void DisplaySortedResult_DisplayResultOnConsole()
        {
            //Arrange
            // _target.Protected().Setup("Log", "File does not exist.").Verifiable();
            IList<NameModel> names = new List<NameModel>
            {
                new NameModel {FirstName = "Andrew", LastName = "Smith"}
            };
            _target.Protected().Setup<bool>("IsFileExist", OutputFileName).Returns(true).Verifiable();
            _target.Protected().Setup<string>("GetFileName", OutputFileName).Returns(OutputFileName).Verifiable();
            _target.Protected().Setup("WriteSortedNamesToConsole", names).Verifiable();
            _target.Protected().Setup("Log", "Finished: Created " + OutputFileName).Verifiable();

            _target.Protected().SetupGet<ITextFileReaderWritter>("TextFileReaderWritter")
                .Returns(_textFileHandler.Object).Verifiable();
            _textFileHandler.Setup(t => t.ReadAll(It.IsAny<string>()))
                .Returns(names).Verifiable();

            //Act
            _target.Object.DisplaySortedResultWrapper();

            //Assert
            _target.Verify();
        }

        [TestMethod]
        public void DisplaySortedResult_OutputFileDoesNotExist()
        {
            //Arrange
            _target.Protected().Setup<bool>("IsFileExist", OutputFileName).Returns(false).Verifiable();

            _target.Protected().Setup("Log", FileSorterMessage.OutputFileDoesNotExist).Verifiable();

            //Act
            _target.Object.DisplaySortedResultWrapper();

            //Assert
            _target.Verify();
        }

        [TestMethod]
        public void WriteSortedNamesToConsole_TakesNamesList()
        {
            //Arrange

            var name = new NameModel {FirstName = "Andrew", LastName = "KENT"};
            var names = new List<NameModel> {name};
            _target.Protected().Setup("Log", name.ToString()).Verifiable();


            //Act
            _target.Object.WriteSortedNamesToConsoleWrapper(names);

            //Assert
            _target.Verify();
        }

        [TestMethod]
        public void WriteSortedNamesToConsole_TakesNullList()
        {
            //Act
            _target.Object.WriteSortedNamesToConsoleWrapper(null);

            //Assert
            _target.Verify();
        }

        [TestMethod]
        public void ValidateInputParameter_InputFileDoesNotExist()
        {
            //Arrange
            _target.Protected().Setup<bool>("IsFileExist", InputFileName).Returns(false).Verifiable();

            _target.Protected().Setup("Log", FileSorterMessage.InputFileDoesNotExist + InputFileName).Verifiable();

            //Act
            var actual = _target.Object.ValidateInputParameterWrapper();

            //Assert
            _target.Verify();
            Assert.AreEqual(actual, false);
        }

        [TestMethod]
        public void ValidateInputParameter_InputFileExist()
        {
            //Arrange
            _target.Protected().Setup<bool>("IsFileExist", InputFileName).Returns(true).Verifiable();

            //Act
            var actual = _target.Object.ValidateInputParameterWrapper();

            //Assert
            _target.Verify();
            Assert.AreEqual(actual, true);
        }
    }
}