using Facebook;
using PagedList;
using ShopXeMay.Dao;
using ShopXeMay.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ShopXeMay.Controllers
{
    public class HomeController : Controller
    {
        private BanXeMayEntities db = new BanXeMayEntities();
        public ActionResult TimKiem(string sanpham, int? page)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo LinkID mới có thể phân trang.
            var sp = (from l in db.SanPham
                      select l).OrderBy(x => x.ID);

            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 3;
            ViewBag.sanpham = sanpham;
            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            var Links = db.SanPham.OrderByDescending(x => x.ID).Where(m => m.TenSanPham.ToLower().Contains(sanpham.ToLower()) == true)
                  .ToList();
            return View(Links.ToPagedList(pageNumber, pageSize));
            // 5. Trả về các Link được phân trang theo kích thước và số trang.
        }
        public ActionResult Index(string TimKiem, int? id, int? page)
        {
            mapSanPham map = new mapSanPham();
            var data = map.TimKiem(TimKiem, id).ToList();
            ViewBag.TimKiem = TimKiem;
            ViewBag.Id = id;
            ViewBag.Tongsoluong = TongSoLuong();
            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;
            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo BookID mới có thể phân trang.
            var sanPhams = db.SanPham.Include(b => b.LoaiSP).Include(b => b.HangSX).OrderBy(b => b.ID);
            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 8;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các Link được phân trang theo kích thước và số trang.
            return View(sanPhams.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Contact()
        {
            ViewBag.email = "hutech.edu@gmail.vn";
            ViewBag.diachi = "VQ4P+249, Phường Tân Phú, Quận 9, Thành phố Hồ Chí Minh";
            ViewBag.sdt = "0777777777";
            return View();
        }
        public ActionResult BaiViet()
        {
            return View();
        }
        //GET: Register

        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Id,TenDangNhap,MatKhau,TenNguoiDung")] TaiKhoan taiKhoan)
        {
            var checkDK = db.TaiKhoan.Where(s => s.TenDangNhap.Equals(taiKhoan.TenDangNhap)).ToList();
            if (ModelState.IsValid && checkDK.Count == 0)
            {
                taiKhoan.MatKhau = GetMD5(taiKhoan.MatKhau);
                taiKhoan.TinhTrang = true;
                taiKhoan.idPhanQuyen = 3;
                taiKhoan.NgayDangKy = DateTime.Now;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.TaiKhoan.Add(taiKhoan);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            if (ModelState.IsValid && checkDK.Count > 0)
            {
                ViewBag.error = "Mail đã tồn tại";
                ViewBag.idPhanQuyen = new SelectList(db.PhanQuyen, "IDPhanQuyen", "ChucVu", taiKhoan.idPhanQuyen);
                return View(taiKhoan);
            }
            else
            {
                ViewBag.idPhanQuyen = new SelectList(db.PhanQuyen, "IDPhanQuyen", "ChucVu", taiKhoan.idPhanQuyen);
                return View(taiKhoan);
            }

        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string TenDangNhap, string MatKhau)
        {

            var f_password = GetMD5(MatKhau);
            var data = db.TaiKhoan.Where(s => s.TenDangNhap.Equals(TenDangNhap) && s.MatKhau.Equals(f_password) && s.TinhTrang == true).ToList();
            var data1 = db.TaiKhoan.Where(s => s.TenDangNhap.Equals(TenDangNhap) && s.MatKhau.Equals(f_password) && s.TinhTrang == true && s.idPhanQuyen == 1).ToList();
            if (ModelState.IsValid && data.Count() == 0)
            {
                ViewBag.error = "Tài Khoản Đã Bị Khóa";
                return RedirectToAction("Login", "Home");
            }
            if (data.Count() > 0 && data1.Count == 0)
            {
                //add session
                Session["TenNguoiDung"] = data.FirstOrDefault().TenNguoiDung;
                Session["TenDangNhap"] = data.FirstOrDefault().TenDangNhap;
                Session["Id"] = data.FirstOrDefault().Id;
                return RedirectToAction("Index", "Home");
            }
            if (data1.Count() > 0)
            {
                //add session
                Session["TenNguoiDung"] = data.FirstOrDefault().TenNguoiDung;
                Session["TenDangNhap"] = data.FirstOrDefault().TenDangNhap;
                Session["Id"] = data.FirstOrDefault().Id;
                return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
            }
            else
            {
                ViewBag.error = "Login failed";
                return RedirectToAction("Login", "Home");
            }
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private int TongSoLuong()
        {
            int tong = 0;
            List<Giohang> lstGioHang = Session["Giohang"] as List<Giohang>;
            if (lstGioHang != null)
            {
                tong = lstGioHang.Sum(n => n.soluong);
            }
            return tong;
        }
        private Uri RediredtUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        [AllowAnonymous]
        public ActionResult Facebook()

        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "183853191108679",
                client_secret = "a618f63ab9c17c1fa5a9f947b38ef124",
                redirect_uri = RediredtUri.AbsoluteUri,
                response_type = "code",
                scope = "email"
            });
            return Redirect(loginUrl.AbsoluteUri);
        }
        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = "183853191108679",
                client_secret = "a618f63ab9c17c1fa5a9f947b38ef124",
                redirect_uri = RediredtUri.AbsoluteUri,
                code = code
            });
            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                Session["AccessToken"] = accessToken;
                fb.AccessToken = accessToken;
                dynamic me = fb.Get("me?fields=link,first_name,currency,last_name,email,gender,locale,timezone,verified,picture,age_range");
                Session["TenNguoiDung"] = me.first_name + me.last_name;
                string email = me.email;
                string firstname = me.first_name;
                string lastname = me.last_name;
                TempData["picture"] = me.picture.data.url;
                var taikhoan = new TaiKhoan();
                taikhoan.TenDangNhap = email;
                taikhoan.TinhTrang = true;
                taikhoan.TenNguoiDung = lastname + " " + firstname;
                taikhoan.idPhanQuyen = 3;
                taikhoan.NgayDangKy = DateTime.Now;
                var resultInsert = new TaiKhoanDao().InsertForFacebook(taikhoan);
                if (resultInsert > 0)
                {
                    Session["TenNguoiDung"] = taikhoan.TenNguoiDung;
                    Session["TenDangNhap"] = taikhoan.TenDangNhap;
                    Session["Id"] = taikhoan.Id;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login", "Home");
                }
            }
            else
            {

            }
            return RedirectToAction("Index", "Home");

        }
        public ActionResult Chat()
        {
            return View();
        }

        public ActionResult Baiviet()
        {
            return View();
        }
    }
}