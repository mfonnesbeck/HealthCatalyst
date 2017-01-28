using HealthCatalystWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HealthCatalystWebApp.Controllers
{
    public class SearchController : Controller
    {
        /// <summary>
        /// Default Search page view
        /// </summary>
        /// <returns>Default view</returns>
        public ActionResult Index()
        {
            //Set title and return default view
            ViewBag.Title = "Person Search";
            return View();
        }
        
        /// <summary>
        /// Search person method called as REST interface
        /// </summary>
        /// <param name="value">The Search person name/term to find</param>
        /// <returns>JSON Action string with list of person data</returns>
        public ActionResult SearchPersons(string value)
        {
            //Deserialize the JSON string input
            string jsonSearchTerm = new JavaScriptSerializer().Deserialize<string>(value);
            
            //Create the DB context and pull the data that applies
            PeopleContext pc = new PeopleContext();
            List<PeopleModel> pm = pc.People.Where(i => i.firstName.Contains(jsonSearchTerm) || i.lastName.Contains(jsonSearchTerm)).ToList();

            //return the count and the resulting data
            return Json(pm, JsonRequestBehavior.AllowGet);
        }
    }
}
