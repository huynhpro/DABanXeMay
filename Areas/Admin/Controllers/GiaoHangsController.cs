using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopXeMay.App_Start;
using ShopXeMay.Models;

namespace ShopXeMay.Areas.Admin.Controllers
{
    //[AdminAuthorize(idPhanQuyen = 1)]
    public class GiaoHangsController : Controller
    {
        private BanXeMayEntities db = new BanXeMayEntities();

        // GET: Admin/GiaoHangs
        public ActionResult Index()
        {
            return View(db.GiaoHang.ToList());
        }

        // GET: Admin/GiaoHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiaoHang giaoHang = db.GiaoHang.Find(id);
            if (giaoHang == null)
            {
                return HttpNotFound();
            }
            return View(giaoHang);
        }

        // GET: Admin/GiaoHangs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/GiaoHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenTrangThai")] GiaoHang giaoHang)
        {
            if (ModelState.IsValid)
            {
                db.GiaoHang.Add(giaoHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(giaoHang);
        }

        // GET: Admin/GiaoHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiaoHang giaoHang = db.GiaoHang.Find(id);
            if (giaoHang == null)
            {
                return HttpNotFound();
            }
            return View(giaoHang);
        }

        // POST: Admin/GiaoHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenTrangThai")] GiaoHang giaoHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(giaoHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(giaoHang);
        }

        // GET: Admin/GiaoHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiaoHang giaoHang = db.GiaoHang.Find(id);
            if (giaoHang == null)
            {
                return HttpNotFound();
            }
            return View(giaoHang);
        }

        // POST: Admin/GiaoHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GiaoHang giaoHang = db.GiaoHang.Find(id);
            db.GiaoHang.Remove(giaoHang);
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
