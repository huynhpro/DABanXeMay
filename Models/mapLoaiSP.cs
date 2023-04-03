using ShopXeMay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace ShopXeMay.Models
{
    public class mapLoaiSP
    {
        public List<LoaiSP> DanhSach()
        {
            var db = new BanXeMayEntities();
            var sanPhams = db.LoaiSP.ToList();
            return sanPhams;
        }
        public List<LoaiSP> TimKiem(string tenloaisp)
        {
            var db = new BanXeMayEntities();
            var sanPhams = db.LoaiSP.Where(m => m.TenLoai.ToLower().Contains(tenloaisp.ToLower()) == true || string.IsNullOrEmpty(tenloaisp)).ToList();
            //var sanPhams2 = (from c in db.SanPham
            //                 where(c.TenSanPham.ToLower().Contains(timkiem.ToLower()) == true || string.IsNullOrEmpty(timkiem)) && (idLoaiSPs == null || c.IDLoaiSP == idLoaiSPs)
            //                 select c
            //                 ).ToList();
            return sanPhams;
        }
    }
}