using ShopXeMay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopXeMay.Models
{
    public class mapDonHang
    {
        public List<DonHang> DanhSach()
        {
            var db = new BanXeMayEntities();
            var donHangs = db.DonHang.ToList();
            return donHangs;
        }
        public List<DonHang> TimKiem(string timkiem)
        {
            var db = new BanXeMayEntities();
            var donhangs = (from c in db.DonHang
                             where (c.TaiKhoan.TenNguoiDung.ToLower().Contains(timkiem.ToLower()) == true || string.IsNullOrEmpty(timkiem))
                             select c
                             ).ToList();
            return donhangs;
        }
        public DonHang GetDonHangFindByID(long id)
        {
            var db = new BanXeMayEntities();
            return db.DonHang.Find(id);
        }
        public DonHang AddDonHang(DonHang dh)
        {
            var db = new BanXeMayEntities();
            return null;
        }

        public DonHang SaveDH(DonHang dh)
        {
            try
            {
                var db = new BanXeMayEntities();
                DonHang donHang = db.DonHang.Add(dh);
                db.SaveChanges();
                return donHang;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }
    }
}