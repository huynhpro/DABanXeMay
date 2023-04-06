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
    public class DonHangsController : Controller
    {
        private BanXeMayEntities db = new BanXeMayEntities();

        // GET: Admin/DonHangs
        public ActionResult Index()
        {
            var donHang = db.DonHang.Include(d => d.GiaoHang).Include(d => d.HinhThucThanhToan).Include(d => d.TaiKhoan);
            return View(donHang.ToList());
        }

        // GET: Admin/DonHangs/Details/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHang.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrangThaiGiaoHang = new SelectList(db.GiaoHang, "ID", "TenTrangThai", donHang.TrangThaiGiaoHang);
            ViewBag.idHinhThucThanhToan = new SelectList(db.HinhThucThanhToan, "ID", "TenHinhThuc", donHang.idHinhThucThanhToan);
            ViewBag.idTaiKhoan = new SelectList(db.TaiKhoan, "Id", "TenDangNhap", donHang.idTaiKhoan);
            return View(donHang);
        }

        // POST: Admin/DonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_DonHang,idTaiKhoan,DaThanhToan,TrangThaiGiaoHang,NgayDat,NgayGiao,DiaChiGiao,TongTien,idHinhThucThanhToan")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TrangThaiGiaoHang = new SelectList(db.GiaoHang, "ID", "TenTrangThai", donHang.TrangThaiGiaoHang);
            ViewBag.idHinhThucThanhToan = new SelectList(db.HinhThucThanhToan, "ID", "TenHinhThuc", donHang.idHinhThucThanhToan);
            ViewBag.idTaiKhoan = new SelectList(db.TaiKhoan, "Id", "TenDangNhap", donHang.idTaiKhoan);
            return View(donHang);
        }
        public ActionResult ThongKe(int? thang, int? nam)
        {
            ViewBag.nam = nam ?? DateTime.Now.Year;
            ViewBag.thang = thang;
            return View();
        }
        public ActionResult GiaoHang(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHang.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrangThaiGiaoHang = new SelectList(db.GiaoHang, "ID", "TenTrangThai", donHang.TrangThaiGiaoHang);
            ViewBag.idHinhThucThanhToan = new SelectList(db.HinhThucThanhToan, "ID", "TenHinhThuc", donHang.idHinhThucThanhToan);
            ViewBag.idTaiKhoan = new SelectList(db.TaiKhoan, "Id", "TenDangNhap", donHang.idTaiKhoan);
            return View(donHang);
        }

        // POST: Admin/DonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GiaoHang([Bind(Include = "ID_DonHang,idTaiKhoan,DaThanhToan,TrangThaiGiaoHang,NgayDat,NgayGiao,DiaChiGiao,TongTien,idHinhThucThanhToan")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TrangThaiGiaoHang = new SelectList(db.GiaoHang, "ID", "TenTrangThai", donHang.TrangThaiGiaoHang);
            ViewBag.idHinhThucThanhToan = new SelectList(db.HinhThucThanhToan, "ID", "TenHinhThuc", donHang.idHinhThucThanhToan);
            ViewBag.idTaiKhoan = new SelectList(db.TaiKhoan, "Id", "TenDangNhap", donHang.idTaiKhoan);
            return View(donHang);
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
