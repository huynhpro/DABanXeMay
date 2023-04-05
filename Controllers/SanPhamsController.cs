using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ShopXeMay.Models;

namespace ShopXeMay.Controllers
{
    public class SanPhamsController : Controller
    {
        private BanXeMayEntities db = new BanXeMayEntities();

        public ActionResult DanhSach(string TimKiem, int? id, int? page)
        {
            mapSanPham map = new mapSanPham();
            var data = map.TimKiem(TimKiem, id).ToList();
            ViewBag.TimKiem = TimKiem;
            ViewBag.Id = id;
            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;
            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo BookID mới có thể phân trang.
            var sanPhams = db.SanPham.Include(b => b.LoaiSP).Include(b => b.HangSX).OrderBy(b => b.TenSanPham);
            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 6;
            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);
            ViewBag.Soluong = sanPhams.Count();
            // 5. Trả về các Link được phân trang theo kích thước và số trang.
            return View(sanPhams.ToPagedList(pageNumber, pageSize));
        }
        // GET: SanPhams
        public ActionResult Index()
        {
            var sanPham = db.SanPham.Include(s => s.GiamGia).Include(s => s.HangSX).Include(s => s.LoaiSP);
            return View(sanPham.ToList());
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.idGiamGia = new SelectList(db.GiamGia, "ID", "ID");
            ViewBag.IdHangSX = new SelectList(db.HangSX, "Id", "TenHangSX");
            ViewBag.IDLoaiSP = new SelectList(db.LoaiSP, "Id", "TenLoai");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenSanPham,GiaBan,NgayNhap,ConHang,SoLuong,BaiViet,IDLoaiSP,AnhSP,IdHangSX,idGiamGia")] SanPham sanPham, HttpPostedFileBase[] files)
        {
            if (ModelState.IsValid)
            {
                db.SanPham.Add(sanPham);
                AnhXe anhXe = new AnhXe();
                int i = 0;
                foreach (HttpPostedFileBase file in files)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/assets/images/product/"+sanPham.TenSanPham+"/") + InputFileName);
                        if (i == 0)
                        {
                            anhXe.idSanPham = sanPham.ID;
                            anhXe.Anh = InputFileName;
                            anhXe.IsDefault = true;
                            
                        }
                        else if (i>0)
                        {
                            anhXe.idSanPham = sanPham.ID;
                            anhXe.Anh = InputFileName;
                            anhXe.IsDefault = false;
                        }
                        i++;
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        db.AnhXe.Add(anhXe);
                        db.SaveChanges();
                        //assigning file uploaded status to ViewBag for showing message to user.  
                        ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                    }

                }
                
                return RedirectToAction("Index");
            }

            ViewBag.idGiamGia = new SelectList(db.GiamGia, "ID", "ID", sanPham.idGiamGia);
            ViewBag.IdHangSX = new SelectList(db.HangSX, "Id", "TenHangSX", sanPham.IdHangSX);
            ViewBag.IDLoaiSP = new SelectList(db.LoaiSP, "Id", "TenLoai", sanPham.IDLoaiSP);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.idGiamGia = new SelectList(db.GiamGia, "ID", "ID", sanPham.idGiamGia);
            ViewBag.IdHangSX = new SelectList(db.HangSX, "Id", "TenHangSX", sanPham.IdHangSX);
            ViewBag.IDLoaiSP = new SelectList(db.LoaiSP, "Id", "TenLoai", sanPham.IDLoaiSP);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenSanPham,GiaBan,NgayNhap,ConHang,SoLuong,BaiViet,IDLoaiSP,AnhSP,IdHangSX,idGiamGia")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idGiamGia = new SelectList(db.GiamGia, "ID", "ID", sanPham.idGiamGia);
            ViewBag.IdHangSX = new SelectList(db.HangSX, "Id", "TenHangSX", sanPham.IdHangSX);
            ViewBag.IDLoaiSP = new SelectList(db.LoaiSP, "Id", "TenLoai", sanPham.IDLoaiSP);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPham.Find(id);
            db.SanPham.Remove(sanPham);
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
