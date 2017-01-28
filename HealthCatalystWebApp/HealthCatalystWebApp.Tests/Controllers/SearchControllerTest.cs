using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HealthCatalystWebApp.Controllers;
using System.Collections.Generic;
using HealthCatalystWebApp.Models;
using Newtonsoft.Json;

namespace HealthCatalystWebApp.Tests.Controllers
{
    [TestClass]
    public class SearchControllerTest
    {
        [TestMethod]
        public void IndexTest()
        {
            //Arrange
            SearchController controller = new SearchController();

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Person Search", result.ViewBag.Title);
        }

        [TestMethod]
        public void SearchAllPersonsTest()
        {
            //Get Search Controller
            SearchController controller = new SearchController();

            //Call Search Person with empty string to pull all records
            JsonResult result = controller.SearchPersons(JsonConvert.SerializeObject(string.Empty)) as JsonResult;

            //Assertions, not null, and more than 0 records
            Assert.IsNotNull(result);
            List<PeopleModel> personList = (List<PeopleModel>)result.Data;
            Assert.IsTrue(personList.Count > 0);
        }

        [TestMethod]
        public void SearchOnePersonsTest()
        {
            //Get Search Controller
            SearchController controller = new SearchController();

            //Call Search Person with empty string to pull all records
            JsonResult result = controller.SearchPersons(JsonConvert.SerializeObject(string.Empty)) as JsonResult;

            //Save off one person in order to search for them
            List<PeopleModel> personList = (List<PeopleModel>)result.Data;
            PeopleModel person = personList[0];
            result = controller.SearchPersons(JsonConvert.SerializeObject(person.lastName)) as JsonResult;

            //Assertions, not null, person found, right person
            Assert.IsNotNull(result);
            personList = (List<PeopleModel>)result.Data;
            Assert.IsTrue(personList.Count > 0);
            Assert.AreEqual(personList[0].lastName, person.lastName);
        }

        [TestMethod]
        public void SearchNonePersonsTest()
        {
            //Get Search Controller
            SearchController controller = new SearchController();

            //Search for something random with no results
            JsonResult result = controller.SearchPersons(JsonConvert.SerializeObject("ASDFGHJKL!@#$%")) as JsonResult;

            //Assertions, not null, no one found
            Assert.IsNotNull(result);
            List<PeopleModel> personList = (List<PeopleModel>)result.Data;
            Assert.IsTrue(personList.Count == 0);
        }
    }
}
