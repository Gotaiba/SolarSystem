using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SolarSystemConsumption.Models;

namespace SolarSystemConsumption.Controllers
{
    public class ItemsController : Controller
    {
        // GET: Items
        SolarDbEntities db = new SolarDbEntities();
        public ActionResult Index()
        {
            return View(db.Items.Where(i=>i.IsDeleted!=true).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Item item)
        {          
            db.Items.Add(item);
            db.SaveChanges();
            TempData["Success"] = "Item created successfuly";
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            return View(db.Items.Find(id));
        }
        [HttpPost]
        public ActionResult Edit(Item item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            Item item = db.Items.Find(id);
            item.IsDeleted = true;
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}