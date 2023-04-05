using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopXeMay.Models;

namespace ShopXeMay.Areas.Admin.Controllers
{
    public class GiamGiasController : Controller
    {
        private BanXeMayEntities db = new BanXeMayEntities();

        // GET: Admin/GiamGias
        public ActionResult Index()
        {
            return View(db.GiamGia.ToList());
        }

        // GET: Admin/GiamGias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiamGia giamGia = db.GiamGia.Find(id);
            if (giamGia == null)
            {
                return HttpNotFound();
            }
            return View(giamGia);
        }

        // GET: Admin/GiamGias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/GiamGias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PhanTramGiam")] GiamGia giamGia)
        {
            if (ModelState.IsValid)
            {
                db.GiamGia.Add(giamGia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(giamGia);
        }

        // GET: Admin/GiamGias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiamGia giamGia = db.GiamGia.Find(id);
            if (giamGia == null)
            {
                return HttpNotFound();
            }
            return View(giamGia);
        }

        // POST: Admin/GiamGias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PhanTramGiam")] GiamGia giamGia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(giamGia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(giamGia);
        }

        // GET: Admin/GiamGias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiamGia giamGia = db.GiamGia.Find(id);
            if (giamGia == null)
            {
                return HttpNotFound();
            }
            return View(giamGia);
        }

        // POST: Admin/GiamGias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GiamGia giamGia = db.GiamGia.Find(id);
            db.GiamGia.Remove(giamGia);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
