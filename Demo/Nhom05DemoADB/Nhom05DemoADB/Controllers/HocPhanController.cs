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
    public class HocPhanController : Controller
    {
        private MongoDBConnect dbconnect;
        private IMongoCollection<HocPhanModel> hocphanCollection;

        public HocPhanController()
        {
            dbconnect = new MongoDBConnect();
            hocphanCollection = dbconnect.database.GetCollection<HocPhanModel>("hocphan");
        }


        // GET: SinhVien
        public ActionResult Index(string searchTerm)
        {
            List<HocPhanModel> hocphans= hocphanCollection.AsQueryable<HocPhanModel>().ToList();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                ViewBag.cont = controllerName;
                hocphans = hocphanCollection.AsQueryable<HocPhanModel>().Where(x => x.TenHP.ToLower().Contains(searchTerm.ToLower())).ToList();
            }
            ViewBag.SearchTerm = searchTerm;

            return View(hocphans);
        }

        // GET: SinhVien/Details/5
        public ActionResult Details(string id)
        {
            var hocphan_id = new ObjectId(id);
            var hocphan = hocphanCollection.AsQueryable<HocPhanModel>().SingleOrDefault(x => x._id == hocphan_id);

            return View(hocphan);
        }

        // GET: SinhVien/Create

        public ActionResult Create()
        {

            return View();
        }

        // POST: SinhVien/Create
        [HttpPost]
        public ActionResult Create(HocPhanModel hocphan)
        {
            try
            {
                hocphanCollection.InsertOne(hocphan);

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
            var hocphan_id = new ObjectId(id);
            var hocphan = hocphanCollection.AsQueryable<HocPhanModel>().SingleOrDefault(x => x._id == hocphan_id);

            return View(hocphan);
        }

        // POST: SinhVien/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, HocPhanModel hocphan)
        {
            try
            {
                var filter = Builders<HocPhanModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<HocPhanModel>.Update
                    .Set("TenHP", hocphan.TenHP)
                    .Set("TinChi", hocphan.TinChi)
                    .Set("MoTa", hocphan.MoTa);
                var result = hocphanCollection.UpdateOne(filter, update);
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
            var hocphan_id = new ObjectId(id);
            var hocphan = hocphanCollection.AsQueryable<HocPhanModel>().SingleOrDefault(x => x._id == hocphan_id);

            return View(hocphan);
        }

        // POST: SinhVien/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, HocPhanModel hocphan)
        {
            try
            {
                hocphanCollection.DeleteOne(Builders<HocPhanModel>.Filter.Eq("_id", ObjectId.Parse(id)));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
