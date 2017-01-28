using HealthCatalystWebApp.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthCatalystWebApp.Controllers
{
    public class AddController : Controller
    {
        /// <summary>
        /// Default Index page for Add Person page
        /// </summary>
        /// <returns>Default Add Person form view</returns>
        public ActionResult Index()
        {
            //Send in the title of the page and turned off success flag
            ViewBag.Title = "Add Person";
            ViewBag.IsSaveSuccess = false;
            return View();
        }

        /// <summary>
        /// Postback Save Person page that saves data to database
        /// </summary>
        /// <param name="person">Person information from page form</param>
        /// <returns>Default Add Person form view with success flag to raise popup</returns>
        [HttpPost]
        public ActionResult Index(PeopleModel person)
        {
            //Valid web image types
            var validImageTypes = new string[]
                {
                    "image/gif",
                    "image/jpeg",
                    "image/pjpeg",
                    "image/png"
                };

            ViewBag.Title = "Add Person";
            //If everything validated on the form process it
            if (ModelState.IsValid)
            {
                //Get the image uploaded
                HttpPostedFileBase ImageUrl = Request.Files["picture"];
                if (ImageUrl == null || ImageUrl.ContentLength == 0)
                {
                    ModelState.AddModelError("picture", "Personal Image field is required");
                }
                else if (!validImageTypes.Contains(ImageUrl.ContentType))
                {
                    ModelState.AddModelError("picture", "Please choose either a GIF, JPG or PNG image.");
                }
                else if (ImageUrl.ContentLength > 0)
                {
                    //Save off the file to images directory, with personID as filename
                    var pid = Guid.NewGuid();
                    var fileName = Path.GetFileName(ImageUrl.FileName);
                    var extension = Path.GetExtension(ImageUrl.FileName);
                    var path = Path.Combine(Server.MapPath("~/images"), pid.ToString() + extension);
                    ImageUrl.SaveAs(path);

                    //Update the person object with picture filename and save
                    using (PeopleContext db = new PeopleContext())
                    {
                        person.picture = @"images/" + pid.ToString() + extension;
                        db.People.Add(person);
                        db.SaveChanges();
                    }
                    //Send success flag to display success popup
                    ViewBag.IsSaveSuccess = true;
                    return View();
                }
            }
            //Failure to save, don't send the success flag, display view again
            ViewBag.IsSaveSuccess = false;
            return View();
        }
    }
}