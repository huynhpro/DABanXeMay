using ShopXeMay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopXeMay.Models
{
    public class Giohang
    {
        BanXeMayEntities db = new BanXeMayEntities();
        public int idSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string AnhSP { get; set; }
        public decimal? GiaTien { get; set; }
        public int soluong { get; set; }
        public double? giamgia { get; set; }
        public decimal? thanhTien
        {
            get { return soluong * GiaTien; }
        }
        public Giohang(int ID)
        {
            idSanPham = ID;
            SanPham sanpham = db.SanPham.Single(n => n.ID == idSanPham);
            TenSanPham = sanpham.TenSanPham;
            AnhSP = sanpham.AnhSP;
            GiaTien = sanpham.GiaBan;
            giamgia = sanpham.GiamGia.PhanTramGiam;
            soluong = 1;
        }
    }
}