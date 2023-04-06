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
    public class HinhThucThanhToansController : Controller
    {
        private BanXeMayEntities db = new BanXeMayEntities();

        // GET: Admin/HinhThucThanhToans
        public ActionResult Index()
        {
            return View(db.HinhThucThanhToan.ToList());
        }

        // GET: Admin/HinhThucThanhToans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhThucThanhToan hinhThucThanhToan = db.HinhThucThanhToan.Find(id);
            if (hinhThucThanhToan == null)
            {
                return HttpNotFound();
            }
            return View(hinhThucThanhToan);
        }

        // GET: Admin/HinhThucThanhToans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/HinhThucThanhToans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenHinhThuc")] HinhThucThanhToan hinhThucThanhToan)
        {
            if (ModelState.IsValid)
            {
                db.HinhThucThanhToan.Add(hinhThucThanhToan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hinhThucThanhToan);
        }

        // GET: Admin/HinhThucThanhToans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhThucThanhToan hinhThucThanhToan = db.HinhThucThanhToan.Find(id);
            if (hinhThucThanhToan == null)
            {
                return HttpNotFound();
            }
            return View(hinhThucThanhToan);
        }

        // POST: Admin/HinhThucThanhToans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenHinhThuc")] HinhThucThanhToan hinhThucThanhToan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hinhThucThanhToan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hinhThucThanhToan);
        }

        // GET: Admin/HinhThucThanhToans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhThucThanhToan hinhThucThanhToan = db.HinhThucThanhToan.Find(id);
            if (hinhThucThanhToan == null)
            {
                return HttpNotFound();
            }
            return View(hinhThucThanhToan);
        }

        // POST: Admin/HinhThucThanhToans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HinhThucThanhToan hinhThucThanhToan = db.HinhThucThanhToan.Find(id);
            db.HinhThucThanhToan.Remove(hinhThucThanhToan);
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
