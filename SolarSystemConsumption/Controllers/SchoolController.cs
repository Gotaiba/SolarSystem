using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SolarSystemConsumption.Models;

namespace SolarSystemConsumption.Controllers
{
    public class SchoolController : Controller
    {
        // GET: School
        SolarDbEntities db = new SolarDbEntities();
        public ActionResult Index()
        {
            var schools = db.Schools.Where(s=>s.IsDeleted!=true).ToList();
            return View(schools);
        }
        public ActionResult AddSchool()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSchool(School school)
        {
            school.Created = DateTime.Now;
            db.Schools.Add(school);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            return View(db.Schools.Where(s => s.Id == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(School school)
        {
            db.Entry(school).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteSchool(int id)
        {
            var school = db.Schools.Find(id);
            school.IsDeleted = true;
           // db.Entry(school)System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            var schools = db.Schools.Where(s => s.IsDeleted !=true).ToList();
            return RedirectToAction("Index");
        }

        public ActionResult SchoolResult(int id)
        {
            var final = new FinalData();
            var finallist = new List<FinalData>();
            var install = db.ItemsInstalleds.Where(s => s.SchoolNo == id).ToList();            
            var items = db.Items.Where(i=>i.IsDeleted!=true).ToList();
            foreach (var item in items)
            {
                int? qt = 0;
                foreach (var device in install.Where(s=>s.ItemNo==item.Id).ToList())
                {
                    qt += device.Quantity;
                }
                final = new FinalData()
                {
                    Device = item.Name,
                    NoDevice = qt,
                    Consumption=item.Consumptions                    
                };
                finallist.Add(final);
            }
            return View(finallist);
        }
        public ActionResult CreateArea()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateArea(Area area)
        {
            if (ModelState.IsValid)
            {
                db.Areas.Add(area);
                db.SaveChanges();
                TempData["Success"] = "Area Created Successfuly";
            }           
            return RedirectToAction("GetAreas");
        }
        public ActionResult GetAreas()
        {
            return View(db.Areas.Where(i=>i.IsDeleted!=true).ToList());
        }
        public ActionResult EditArea(int id)
        {
            return View(db.Areas.Find(id));
        }
        [HttpPost]
        public ActionResult EditArea(Area area)
        {
            if (ModelState.IsValid)
            {
                db.Entry(area).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetAreas");
            }
            return View(area);
        }
        public ActionResult DeleteArea(int id)
        {
            Area area = db.Areas.Find(id);
            area.IsDeleted = true;
            db.Entry(area).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("GetAreas");
        }

        #region Items for Classes
        public ActionResult AddItems(int id)
        {          
            ViewBag.SchoolName = db.Schools.Where(r=>r.Id==id).Select(s => s.Name).FirstOrDefault();
            ViewBag.Items = new SelectList(db.Items.Where(r => r.IsDeleted != true).OrderBy(i => i.Name), "Id", "Name");
            ViewBag.Areas = new SelectList(db.Areas.Where(r=>r.IsDeleted!=true).OrderBy(i => i.AreaName), "Id", "AreaName");
            return View();
        }       
        [HttpPost]
        public ActionResult AddItems(int id,ItemsInstalled installeds,FormCollection fc)
        {
            var count = int.Parse(fc["ItemCount"].ToString());        
            var ItemsInstalled = new ItemsInstalled();
            for(int i=0;i<count;i++)
            {
                ItemsInstalled = new ItemsInstalled()
                {
                    ItemNo = int.Parse(fc["[" + i + "].ItemNo"].ToString()),
                    AreaNo = int.Parse(fc["[" + i + "].AreaNo"].ToString()),
                    SchoolNo = id,
                    Quantity = int.Parse(fc["[" + i + "].Quantity"].ToString()),
                    Created = DateTime.Now,
                };
                db.ItemsInstalleds.Add(ItemsInstalled);
            }
            db.SaveChanges();
            ViewBag.Success = true;
            ViewBag.SchoolName = db.Schools.Where(r => r.Id == id).Select(s => s.Name).FirstOrDefault();
            ViewBag.Items = new SelectList(db.Items.Where(r => r.IsDeleted != true).OrderBy(i => i.Name), "Id", "Name");
            ViewBag.Areas = new SelectList(db.Areas.Where(r => r.IsDeleted != true).OrderBy(i => i.AreaName), "Id", "AreaName");
            return View();
        }
        public JsonResult DeleteItemForClasses(int id)
        {
            var ItemInstalled = db.ItemsInstalleds.Find(id);
            db.ItemsInstalleds.Remove(ItemInstalled);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion
        //////////////////////////////////////////////////////////////
    }
}