using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NameSorter.Model;

namespace NameSorterTest.DataProvider
{
    [TestClass]
    public class TextFileReaderWritterTests
    {
        [TestMethod]
        public void GetNameModel_TakesCommaSeperatedNames()
        {
            //Arrange
            string[] fields = new[] { "Smith", "Andrew"}; 
            var target = new Mock<TextFileReaderWritterWrapper>();

            //Act
            var actual = target.Object.GetNameModel(fields);

            //Verify
            target.Verify();
            Assert.AreEqual(actual.FirstName, "Andrew");
            Assert.AreEqual(actual.LastName, "Smith");
        }

        [TestMethod]
        public void GetNameModel_TakesOnlyFirstName()
        {
            //Arrange
            string[] fields = new[] { "Smith" };
            var target = new Mock<TextFileReaderWritterWrapper>();

            //Act
            var actual = target.Object.GetNameModel(fields);

            //Verify
            target.Verify();
            Assert.AreEqual(actual.FirstName, "");
            Assert.AreEqual(actual.LastName, "Smith");
        }

        [TestMethod]
        public void GetNameModel_EmptyLine()
        {
            //Arrange
            var target = new Mock<TextFileReaderWritterWrapper>();

            //Act
            var actual = target.Object.GetNameModel(null);

            //Verify
            target.Verify();
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetTextLine_TakesNameModel()
        {
            //Arrange
            NameModel name = new NameModel() { FirstName = "Andrew", LastName = "Smith" };
            var target = new Mock<TextFileReaderWritterWrapper>();

            //Act
            var actual = target.Object.GetTextLine(name);

            //Verify
            target.Verify();
            Assert.AreEqual(actual, string.Format("{0}, {1}",name.LastName,name.FirstName));
           
        }

        [TestMethod]
        public void GetTextLine_TakesOnlyFirstName()
        {
            //Arrange
            NameModel name = new NameModel() { LastName = string.Empty, FirstName = "Andrew"};
            var target = new Mock<TextFileReaderWritterWrapper>();

            //Act
            var actual = target.Object.GetTextLine(name);

            //Verify
            target.Verify();
            Assert.AreEqual(actual, string.Format("{0}, {1}", name.LastName, name.FirstName));

        }

        [TestMethod]
        public void GetTextLine_TakesEmptyName()
        {
            //Arrange
            NameModel name = new NameModel() { LastName = string.Empty, FirstName = string.Empty };
            var target = new Mock<TextFileReaderWritterWrapper>();

            //Act
            var actual = target.Object.GetTextLine(name);

            //Verify
            target.Verify();
            Assert.AreEqual(actual, string.Empty);

        }

        [TestMethod]
        public void GetTextLine_TakesOnlyLastName()
        {
            //Arrange
            NameModel name = new NameModel() { LastName = "Smith", FirstName = string.Empty };
            var target = new Mock<TextFileReaderWritterWrapper>();

            //Act
            var actual = target.Object.GetTextLine(name);

            //Verify
            target.Verify();
            Assert.AreEqual(actual, name.LastName);

        }
    }
}
