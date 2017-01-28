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
        // GET: Add
        public ActionResult Index()
        {
            ViewBag.Title = "Add Person";
            ViewBag.IsSaveSuccess = false;
            return View();
        }

        // POST: /Add/Index
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Index([Bind(Include = "firstName,lastName,address,age,interests,picture")] PeopleModel person)
        public ActionResult Index(PeopleModel person)
        {
            var validImageTypes = new string[]
                {
                    "image/gif",
                    "image/jpeg",
                    "image/pjpeg",
                    "image/png"
                };

            ViewBag.Title = "Add Person";
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
                    //Save off the file to images directory
                    var pid = Guid.NewGuid();
                    var fileName = Path.GetFileName(ImageUrl.FileName);
                    var extension = Path.GetExtension(ImageUrl.FileName);
                    var path = Path.Combine(Server.MapPath("~/images"), pid.ToString() + extension);
                    ImageUrl.SaveAs(path);

                    //Update the person object and save
                    using (PeopleContext db = new PeopleContext())
                    {
                        person.picture = @"images/" + pid.ToString() + extension;
                        db.People.Add(person);
                        db.SaveChanges();
                    }
                    ViewBag.IsSaveSuccess = true;
                    return View();
                }
            }
            ViewBag.IsSaveSuccess = false;
            return View();
        }
    }
}