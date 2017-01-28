using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HealthCatalystWebApp.Controllers;
using HealthCatalystWebApp.Models;
using System;

namespace HealthCatalystWebApp.Tests.Controllers
{
    [TestClass]
    public class AddControllerTest
    {
        [TestMethod]
        public void IndexTest()
        {
            //Arrange
            AddController controller = new AddController();

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Add Person", result.ViewBag.Title);
            Assert.AreEqual(false, result.ViewBag.IsSaveSuccess);
        }

        [TestMethod]
        public void IndexSavePersonNoPictureErrorTest()
        {
            PeopleModel person = new PeopleModel()
            {
                PID = Guid.NewGuid(),
                firstName = "Shoeless Joe",
                lastName = "Jackson",
                address = "100 N 200 W, SLC",
                age = 25,
                interests = "Baseball, sports",
                picture = ""
            };

            //Get controller and call Index method with person object
            AddController controller = new AddController();
            try
            {
                //Since we cannot populate the Request object with an uploaded image
                //only test that calling it this way causes an exception
                ViewResult result = controller.Index(person) as ViewResult;
            }
            catch (Exception ex)
            {
                //No picture attached in the request object blows up
                Assert.AreEqual(ex.GetType(), typeof(NullReferenceException));
            }
        }
    }
}
