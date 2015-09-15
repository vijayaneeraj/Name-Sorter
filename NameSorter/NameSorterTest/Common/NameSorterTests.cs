
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NameSorter.Model;

namespace NameSorterTest.Common
{
     [TestClass]
    public class NameSorterTests
    {
        [TestMethod]
        public void SortNameByLastAndFirstName_TakesNames()
        {
            //Arrange
            var unsortedNames = new List<NameModel>()
             {
                 new NameModel() {LastName = "BAKER", FirstName = "THEODORE"},             
                 new NameModel() {LastName = "SMITH", FirstName = "ANDREW"},
                 new NameModel() {LastName = "SMITH", FirstName = "FREDRICK"},
                 new NameModel() {LastName = "KENT", FirstName = "MADISON"},
             };

            var sortedNames = new List<NameModel>
             {
                 new NameModel() {LastName = "BAKER", FirstName = "THEODORE"},
                 new NameModel() {LastName = "KENT", FirstName = "MADISON"},
                 new NameModel() {LastName = "SMITH", FirstName = "ANDREW"},
                 new NameModel() {LastName = "SMITH", FirstName = "FREDRICK"},
             };

            var target = new Mock<NameSorter.Common.NameSorter>();
            //Act
            List<NameModel> actual = (List<NameModel>)target.Object.SortNameByLastAndFirstName(unsortedNames);

            //Assert
            target.Verify();
            Assert.AreEqual(sortedNames[0].FirstName, actual[0].FirstName);
            Assert.AreEqual(sortedNames[0].LastName, actual[0].LastName);
            Assert.AreEqual(sortedNames[1].FirstName, actual[1].FirstName);
            Assert.AreEqual(sortedNames[1].LastName, actual[1].LastName);
            Assert.AreEqual(sortedNames[2].FirstName, actual[2].FirstName);
            Assert.AreEqual(sortedNames[2].LastName, actual[2].LastName);
            Assert.AreEqual(sortedNames[3].FirstName, actual[3].FirstName);
            Assert.AreEqual(sortedNames[3].LastName, actual[3].LastName);
        }

        [TestMethod]
        public void SortNameByLastAndFirstName_TakesEmptyList()
        {
            //Arrange
            var names = new List<NameModel>();
            var target = new Mock<NameSorter.Common.NameSorter>();
            //Act
            var actual = target.Object.SortNameByLastAndFirstName(names);

            //Verify
            target.Verify();
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void SortNameByLastAndFirstName_TakesNullList()
        {
            //Arrange
            var target = new Mock<NameSorter.Common.NameSorter>();
            //Act
            var actual = target.Object.SortNameByLastAndFirstName(null);

            //Verify
            target.Verify();
            Assert.IsNull(actual);
        }
    }
}
