using ShopXeMay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopXeMay.Models
{
    public class mapSanPham
    {
        public bool CapNhatHinhAnh(int id, string v)
        {
            try
            {
                BanXeMayEntities db = new BanXeMayEntities();
                var sanPhams = db.SanPham.Find(id);
                sanPhams.AnhSP = v;
                db.SaveChanges();
                return true;
            }
            catch 
            {

                return false;
            }

        }

        public List<SanPham> DanhSach()
        {
            var db = new BanXeMayEntities();
            var sanPhams = db.SanPham.ToList();
            return sanPhams;
        }
        public List<SanPham> TimKiem(string timkiem, int? idLoaiSPs)
        {
            var db = new BanXeMayEntities();
            var sanPhams2 = (from c in db.SanPham
                             where (c.TenSanPham.ToLower().Contains(timkiem.ToLower()) == true || string.IsNullOrEmpty(timkiem)) && (idLoaiSPs == null || c.IDLoaiSP == idLoaiSPs)
                             select c
                             ).ToList();
            return sanPhams2;
        }
       
    }
}
