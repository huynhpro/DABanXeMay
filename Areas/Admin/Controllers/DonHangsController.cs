using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ShopXeMay.App_Start;
using ShopXeMay.Models;

namespace ShopXeMay.Areas.Admin.Controllers
{
    //[AdminAuthorize(idPhanQuyen = 1)]
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
            ViewBag.songuoitruycap = HttpContext.Application["SoNguoiTruyCap"].ToString() as string;
            ViewBag.online = HttpContext.Application["Online"].ToString() as string;
            ViewBag.nam = nam ?? DateTime.Now.Year;
            ViewBag.thang = thang;
            ViewBag.tongthanhvien = TongThanhVien();
            ViewBag.tongdonhang = TongDonHang();
            ViewBag.tongdoanhthu = ThongKeDoanhThu();
            //ViewBag.tongdoanhthuthang = ThongKeDoanhThuThang(thang, nam);
            
            List<DataPoint> dataPoints = new List<DataPoint>();
            var sanpham = db.SanPham.ToList();
            foreach (var item in sanpham)
            {
                var check = db.SanPham_DatHang.Where(n => n.idSanPham == item.ID).ToList().Count;
                if (check > 0)
                {
                    double temp = double.Parse(item.GiaBan.ToString());
                    dataPoints.Add(new DataPoint(item.TenSanPham, check*temp*100/ThongKeDoanhThu()));
                }
            }
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View();
        }
      
        public double ThongKeDoanhThu()
        {
            double TongDoanhThu = db.DonHang.Sum(n => n.TongTien);
            return TongDoanhThu;
        }
        //public double ThongKeDoanhThuThang(int? thang, int? nam)
        //{
        //    double TongDoanhThu = db.DonHang.Where(n => n.NgayGiao.Value.Month == thang && n.NgayGiao.Value.Year == nam).Sum(n => n.TongTien);
        //    return TongDoanhThu;
        //}
        public double TongDonHang()
        {
            double sl = db.DonHang.ToList().Count();
            return sl;
        }
        public double TongThanhVien()
        {
            double sl = db.TaiKhoan.ToList().Count();
            return sl;
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
