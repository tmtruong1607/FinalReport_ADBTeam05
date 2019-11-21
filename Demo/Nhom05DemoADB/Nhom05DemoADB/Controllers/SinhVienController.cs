using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver.Core;
using System.Configuration;
using Nhom05DemoADB.App_Start;
using MongoDB.Driver;
using Nhom05DemoADB.Models;

namespace Nhom05DemoADB.Controllers
{
    public class SinhVienController : Controller
    {
        private MongoDBConnect dbconnect;
        private IMongoCollection<SinhVienModel> sinhvienCollection;
        
        public SinhVienController()
        {
            dbconnect = new MongoDBConnect();
            sinhvienCollection = dbconnect.database.GetCollection<SinhVienModel>("sinhvien");
        }


        // GET: SinhVien
        public ActionResult Index(string searchTerm)
        {
            List<SinhVienModel> sinhviens = sinhvienCollection.AsQueryable<SinhVienModel>().ToList();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                ViewBag.cont = controllerName;
                sinhviens = sinhvienCollection.AsQueryable<SinhVienModel>().Where(x => x.HoTen.ToLower().Contains(searchTerm.ToLower())).ToList();
            }
            ViewBag.SearchTerm = searchTerm;

            return View(sinhviens);
        }

        // GET: SinhVien/Details/5
        public ActionResult Details(string id)
        {
            var sinhvien_id = new ObjectId(id);
            var sinhvien = sinhvienCollection.AsQueryable<SinhVienModel>().SingleOrDefault(x => x._id == sinhvien_id);
            
            return View(sinhvien);
        }

        // GET: SinhVien/Create

        public ActionResult Create()
        {
            
            return View();
        }

        // POST: SinhVien/Create
        [HttpPost]
        public ActionResult Create(SinhVienModel sinhvien)
        {
            try
            {
                sinhvienCollection.InsertOne(sinhvien);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SinhVien/Edit/5
        public ActionResult Edit(string id)
        {
            var sinhvien_id = new ObjectId(id);
            var sinhvien = sinhvienCollection.AsQueryable<SinhVienModel>().SingleOrDefault(x => x._id == sinhvien_id);

            return View(sinhvien);          
        }

        // POST: SinhVien/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, SinhVienModel sinhvien)
        {
            try
            {
                var filter = Builders<SinhVienModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<SinhVienModel>.Update
                    .Set("HoTen", sinhvien.HoTen)
                    .Set("QueQuan", sinhvien.QueQuan)
                    .Set("SDT", sinhvien.SDT);
                var result = sinhvienCollection.UpdateOne(filter, update);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SinhVien/Delete/5
        public ActionResult Delete(string id)
        {
            var sinhvien_id = new ObjectId(id);
            var sinhvien = sinhvienCollection.AsQueryable<SinhVienModel>().SingleOrDefault(x => x._id == sinhvien_id);

            return View(sinhvien);
        }

        // POST: SinhVien/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, SinhVienModel sinhvien)
        {
            try
            {
                sinhvienCollection.DeleteOne(Builders<SinhVienModel>.Filter.Eq("_id", ObjectId.Parse(id)));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
