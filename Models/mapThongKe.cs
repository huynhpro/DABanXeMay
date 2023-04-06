//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace ShopXeMay.Models
//{
//    public class mapThongKe
//    {
//        BanXeMayEntities db = new BanXeMayEntities();
//        public int SoLuongSanPham { get; set; }
//        public int SoLuongDonHang (int trangthai)
//        {
//            return SoLuongSanPham;
//        }
//        public double DoanhThu { get; set; }
//        public List<DonHang> BaoCao_TheoSanPham(int nam, int? thang)
//        {
//            try
//            {
//                return db.
//            }
//            catch 
//            {

//                throw;
//            }
//        }
//        public List<DonHang> BaoCao_SoLuongDon(int nam)
//        {
//            try
//            {
//                return db.BaoCaoSoLuongDon(nam).ToList();
//            }
//            catch
//            {

//                throw;
//            }
//        }
//    }
//}