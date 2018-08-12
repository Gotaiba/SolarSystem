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
            var schools = db.Schools.ToList();
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
            return View();
        }

        public ActionResult SchoolResult(int id)
        {
            var final = new FinalData();
            var finallist = new List<FinalData>();
            var install = db.ItemsInstalleds.Where(s => s.SchoolNo == id).ToList();            
            var items = db.Items.ToList();
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

        #region Items for Classes
        public ActionResult AddItemsForClasses(int id)
        {
            ViewBag.AddedItems = db.ItemsInstalleds.Where(r => r.SchoolNo == id && r.ForClass == true).ToList();
            ViewBag.SchoolName = db.Schools.Where(r=>r.Id==id).Select(s => s.Name).FirstOrDefault();
            ViewBag.Items = new SelectList(db.Items.OrderBy(i => i.Name), "Id", "Name");           
            return View();
        }       
        [HttpPost]
        public ActionResult AddItemsForClasses(int id,ItemsInstalled installeds,FormCollection fc)
        {
            var count = int.Parse(fc["ItemCount"].ToString());        
            var ItemsInstalled = new ItemsInstalled();
            for(int i=0;i<count;i++)
            {
                ItemsInstalled = new ItemsInstalled()
                {
                    ItemNo = int.Parse(fc["[" + i + "].ItemNo"].ToString()),
                    SchoolNo = id,
                    Quantity = int.Parse(fc["[" + i + "].Quantity"].ToString()),
                    Created = DateTime.Now,
                    ForClass = true
                };
                db.ItemsInstalleds.Add(ItemsInstalled);
            }
            db.SaveChanges();
            ViewBag.Success = true;
            ViewBag.AddedItems = db.ItemsInstalleds.Where(r => r.SchoolNo == id && r.ForClass == true).ToList();
            ViewBag.SchoolName = db.Schools.Where(r => r.Id == id).Select(s => s.Name).FirstOrDefault();
            ViewBag.Items = new SelectList(db.Items.OrderBy(i => i.Name), "Id", "Name");
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
        #region Items for Offices
        public ActionResult AddItemsForOffices(int id)
        {
            ViewBag.AddedItems = db.ItemsInstalleds.Where(r => r.SchoolNo == id && r.ForOffice == true).ToList();
            ViewBag.SchoolName = db.Schools.Where(r => r.Id == id).Select(s => s.Name).FirstOrDefault();
            ViewBag.Items = new SelectList(db.Items.OrderBy(i => i.Name), "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult AddItemsForOffices(int id, ItemsInstalled installeds, FormCollection fc)
        {
            var count = int.Parse(fc["ItemCount"].ToString());
            var ItemsInstalled = new ItemsInstalled();
            for (int i = 0; i < count; i++)
            {
                ItemsInstalled = new ItemsInstalled()
                {
                    ItemNo = int.Parse(fc["[" + i + "].ItemNo"].ToString()),
                    SchoolNo = id,
                    Quantity = int.Parse(fc["[" + i + "].Quantity"].ToString()),
                    Created = DateTime.Now,
                    ForOffice = true
                };
                db.ItemsInstalleds.Add(ItemsInstalled);
            }
            db.SaveChanges();
            ViewBag.Success = true;
            ViewBag.AddedItems = db.ItemsInstalleds.Where(r => r.SchoolNo == id && r.ForOffice == true).ToList();
            ViewBag.SchoolName = db.Schools.Where(r => r.Id == id).Select(s => s.Name).FirstOrDefault();
            ViewBag.Items = new SelectList(db.Items.OrderBy(i => i.Name), "Id", "Name");
            return View();
        }
        public JsonResult DeleteItemForOffices(int id)
        {
            var ItemInstalled = db.ItemsInstalleds.Find(id);
            db.ItemsInstalleds.Remove(ItemInstalled);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion
        //////////////////////////////////////////////////////////////
        #region Items for Kafterias
        public ActionResult AddItemsForKafterias(int id)
        {
            ViewBag.AddedItems = db.ItemsInstalleds.Where(r => r.SchoolNo == id && r.ForKafteria == true).ToList();
            ViewBag.SchoolName = db.Schools.Where(r => r.Id == id).Select(s => s.Name).FirstOrDefault();
            ViewBag.Items = new SelectList(db.Items.OrderBy(i => i.Name), "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult AddItemsForKafterias(int id, ItemsInstalled installeds, FormCollection fc)
        {
            var count = int.Parse(fc["ItemCount"].ToString());
            var ItemsInstalled = new ItemsInstalled();
            for (int i = 0; i < count; i++)
            {
                ItemsInstalled = new ItemsInstalled()
                {
                    ItemNo = int.Parse(fc["[" + i + "].ItemNo"].ToString()),
                    SchoolNo = id,
                    Quantity = int.Parse(fc["[" + i + "].Quantity"].ToString()),
                    Created = DateTime.Now,
                    ForKafteria = true
                };
                db.ItemsInstalleds.Add(ItemsInstalled);
            }
            db.SaveChanges();
            ViewBag.Success = true;
            ViewBag.AddedItems = db.ItemsInstalleds.Where(r => r.SchoolNo == id && r.ForKafteria == true).ToList();
            ViewBag.SchoolName = db.Schools.Where(r => r.Id == id).Select(s => s.Name).FirstOrDefault();
            ViewBag.Items = new SelectList(db.Items.OrderBy(i => i.Name), "Id", "Name");
            return View();
        }
        public JsonResult DeleteItemForKafterias(int id)
        {
            var ItemInstalled = db.ItemsInstalleds.Find(id);
            db.ItemsInstalleds.Remove(ItemInstalled);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}