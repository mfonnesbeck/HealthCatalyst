using HealthCatalystWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HealthCatalystWebApp.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Person Search";
            return View();
        }

        public ActionResult SearchPersons(string value)
        {
            string jsonSearchTerm = new JavaScriptSerializer().Deserialize<string>(value);
            
            //Create the DB context and pull the data that applies
            PeopleContext pc = new PeopleContext();
            List<PeopleModel> pm = pc.People.Where(i => i.firstName.Contains(jsonSearchTerm) || i.lastName.Contains(jsonSearchTerm)).ToList();

            //return the count and the resulting data
            return Json(pm, JsonRequestBehavior.AllowGet);
        }
    }
}
