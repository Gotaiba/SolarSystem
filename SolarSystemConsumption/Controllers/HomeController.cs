using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SolarSystemConsumption.Models;

namespace SolarSystemConsumption.Controllers
{
    public class HomeController : Controller
    {
        SolarDbEntities db = new SolarDbEntities();
        public ActionResult Index()
        {        
            return View(db.Schools.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}