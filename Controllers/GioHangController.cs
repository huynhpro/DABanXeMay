using Newtonsoft.Json.Linq;
using ShopXeMay.Models;
using ShopXeMay;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopXeMay.MoMo;

namespace ShopXeMay.Controllers
{
    public class GioHangController : Controller
    {
        BanXeMayEntities db = new BanXeMayEntities();
        // GET: GioHang
        public List<Giohang> LayGioHang()
        {
            List<Giohang> lstGioHang = Session["Giohang"] as List<Giohang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<Giohang>();
                Session["Giohang"] = lstGioHang;
            }
            return lstGioHang;
        }
        //thêm 1 sản phẩm vào giỏ hàng
        public ActionResult ThemGioHang(int idSanPham, string strURL)
        {
            List<Giohang> lstGioHang = LayGioHang();
            Giohang sanpham = lstGioHang.Find(n => n.idSanPham == idSanPham);
            if (sanpham == null)
            {
                sanpham = new Giohang(idSanPham);
                lstGioHang.Add(sanpham);
                return Redirect(strURL);

            }
            else
            {
                sanpham.soluong++;
                var d = db.SanPham.Find(idSanPham);
                return Redirect(strURL);
            }
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
        private decimal? TongTien()
        {
            decimal? tongTien = 0;
            List<Giohang> lstGioHang = Session["Giohang"] as List<Giohang>;
            if (lstGioHang != null)
            {
                tongTien = lstGioHang.Sum(n => n.thanhTien);
            }
            return tongTien;
        }
        private decimal? TongTienSauKhuyenMai()
        {
            decimal? tongTien = 0;
            List<Giohang> lstGioHang = Session["Giohang"] as List<Giohang>;
            if (lstGioHang != null)
            {
                tongTien = lstGioHang.Sum(n => n.thanhTien) + 150000;
            }
            return tongTien;
        }
        //xem danh sách giỏ hàng
        public ActionResult GioHang()
        {
            List<Giohang> lstGioHang = LayGioHang();
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongtiensaukhuyenmai = TongTienSauKhuyenMai();
            return View(lstGioHang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView("GioHangPartial");
        }
        public ActionResult CartPartial()
        {
            List<Giohang> lstGioHang = LayGioHang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView("CartPartial", lstGioHang);
        }
        public ActionResult XoaGioHang(int idSanPham)
        {
            List<Giohang> lstGioHang = LayGioHang();
            Giohang sanpham = lstGioHang.SingleOrDefault(n => n.idSanPham == idSanPham);
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.idSanPham == idSanPham);
                return RedirectToAction("GioHang");

            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapNhatGioHang(int idSanPham, FormCollection f)
        {
            List<Giohang> lstGioHang = LayGioHang();
            Giohang sanpham = lstGioHang.SingleOrDefault(n => n.idSanPham == idSanPham);
            if (sanpham != null)
            {
                sanpham.soluong = int.Parse(f["txtSoLuong"].ToString());

            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaTatCaGioHang()
        {
            List<Giohang> lstGioHang = LayGioHang();
            lstGioHang.Clear();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TenDangNhap"] == null || Session["TenDangNhap"].ToString() == "")
            {
                return RedirectToAction("Login", "Home");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Giohang> lstGioHang = LayGioHang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongtiensaukhuyenmai = TongTienSauKhuyenMai();
            return View(lstGioHang);
        }
        public ActionResult DatHang(FormCollection collection, string payment)
        {
            DonHang dh = new DonHang();
            string a = Session["TenDangNhap"].ToString();
            TaiKhoan tk = new BanXeMayEntities().TaiKhoan.FirstOrDefault(m => m.TenDangNhap == a);
            //TaiKhoan tk = (TaiKhoan)Session["TenDangNhap"];
            List<Giohang> lstGioHang = LayGioHang();

            dh.ID_DonHang = long.Parse(DateTime.Now.Ticks.ToString());
            dh.idTaiKhoan = tk.Id;
            dh.NgayDat = DateTime.Now;
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["Ngaygiao"]);
            var diachigiao = collection["DiaChiGiao"].ToString();
            dh.NgayGiao = DateTime.Parse(ngaygiao);
            dh.DiaChiGiao = diachigiao;
            dh.TrangThaiGiaoHang = 1;
            dh.DaThanhToan = false;
            
            if (payment == "online")
            {
                dh.idHinhThucThanhToan = 2;
            }
            if (payment == "offline")
            {
                dh.idHinhThucThanhToan = 1;
            }
            dh.TongTien = TongTienSauKhuyenMai();
            db.DonHang.Add(dh);
            db.SaveChanges();

            foreach (var item in lstGioHang)
            {
                SanPham_DatHang ct = new SanPham_DatHang();
                ct.idDonHang = dh.ID_DonHang;
                ct.idSanPham = item.idSanPham;
                ct.SoLuong = item.soluong;
                ct.GiaBan = (decimal)item.GiaTien;
                db.SanPham_DatHang.Add(ct);
                db.SaveChanges();

            }
            Session["Giohang"] = null;
            if (dh.idHinhThucThanhToan == 2)
            {
                return RedirectToAction("Payment", "GioHang", new { order = dh.ID_DonHang });
            }
            return RedirectToAction("Xacnhandonhang", "GioHang");

        }
        public ActionResult Xacnhandonhang()
        {
            return View();
        }
        public ActionResult Payment(long order)
        {
            //request params need to request to MoMo system
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            var dhDAO = new mapDonHang();
            DonHang dh = dhDAO.GetDonHangFindByID(order);
            string partnerCode = "MOMOOJOI20210710";
            string accessKey = "iPXneGmrJH0G8FOP";
            string serectkey = "sFcbSGRSJjwGxwhhcEktCHWYUuTuPNDB";
            string orderInfo = Session["TenNguoiDung"] + " Đặt hàng với số tiền" +" " + String.Format("{0:0,0} đ", dh.TongTien.ToString());
            string returnUrl = "https://localhost:44351/GioHang/ConfirmPaymentClient";
            string notifyurl = "https://9dc2-113-161-73-78.ap.ngrok.io/GioHang/SavePayment"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test
            var tienthanhtoan = "1000";
            string amount = tienthanhtoan.ToString();
            string orderid = order.ToString();
            string requestId = order.ToString();
            string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
                extraData;

            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };

            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);

            return Redirect(jmessage.GetValue("payUrl").ToString());
        }
        public ActionResult ConfirmPaymentClient(Result result)
        {
            //lấy kết quả Momo trả về và hiển thị thông báo cho người dùng (có thể lấy dữ liệu ở đây cập nhật xuống db)
            string rMessage = result.message;
            string rOrderId = result.orderId;
            string rErrorCode = result.errorCode; // = 0: thanh toán thành công
            if (rErrorCode == "0")
            {
                var dhDAO = new mapDonHang();
                DonHang dh = dhDAO.GetDonHangFindByID(long.Parse(rOrderId));
                dh.DaThanhToan = true;
                db.DonHang.AddOrUpdate(dh);
                db.SaveChanges();
                ViewBag.message = "CẢM ƠN QUÝ KHÁCH ĐÃ TIN TƯỞNG VÀ ỦNG HỘ SHOP";
            }
            return View();
        }
    }
}