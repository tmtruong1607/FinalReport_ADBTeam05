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
    public class KhoaController : Controller
    {

        private MongoDBConnect dbconnect;
        private IMongoCollection<KhoaModel> khoaCollection;

        public KhoaController()
        {
            dbconnect = new MongoDBConnect();
            khoaCollection = dbconnect.database.GetCollection<KhoaModel>("khoa");
        }
        // GET: Khoa
        public ActionResult Index(string searchTerm)
        {
            List<KhoaModel> khoas = khoaCollection.AsQueryable<KhoaModel>().ToList();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                ViewBag.cont = controllerName;
                khoas = khoaCollection.AsQueryable<KhoaModel>().Where(x => x.TenKhoa.ToLower().Contains(searchTerm.ToLower())).ToList();
            }
            ViewBag.SearchTerm = searchTerm;

            return View(khoas);
        }

        // GET: Khoa/Details/5
        public ActionResult Details(string id)
        {
            var khoa_id = new ObjectId(id);
            var khoa = khoaCollection.AsQueryable<KhoaModel>().SingleOrDefault(x => x._id == khoa_id);

            return View(khoa);
        }

        // GET: Khoa/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Khoa/Create
        [HttpPost]
        public ActionResult Create(KhoaModel khoa)
        {
            try
            {
                khoaCollection.InsertOne(khoa);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Khoa/Edit/5
        public ActionResult Edit(string id)
        {
            var khoa_id = new ObjectId(id);
            var khoa = khoaCollection.AsQueryable<KhoaModel>().SingleOrDefault(x => x._id == khoa_id);

            return View(khoa);
        }

        // POST: Khoa/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, KhoaModel khoa)
        {
            try
            {
                var filter = Builders<KhoaModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<KhoaModel>.Update
                    .Set("TenKhoa", khoa.TenKhoa)
                    .Set("NamThanhLap", khoa.NamThanhLap)
                    .Set("DiaChi", khoa.DiaChi);
                var result = khoaCollection.UpdateOne(filter, update);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Khoa/Delete/5
        public ActionResult Delete(string id)
        {
            var khoa_id = new ObjectId(id);
            var khoa = khoaCollection.AsQueryable<KhoaModel>().SingleOrDefault(x => x._id == khoa_id);

            return View(khoa);
        }

        // POST: Khoa/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, KhoaModel khoa)
        {
            try
            {
                khoaCollection.DeleteOne(Builders<KhoaModel>.Filter.Eq("_id", ObjectId.Parse(id)));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }        
    }
}
