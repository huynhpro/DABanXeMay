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
        public double GiaTien { get; set; }
        public int soluong { get; set; }
        public double? giamgia { get; set; }
        public double thanhTien
        {
            get { return soluong * GiaTien; }
        }
        public Giohang(int ID)
        {
            idSanPham = ID;
            SanPham sanpham = db.SanPham.FirstOrDefault(n => n.ID == idSanPham);
            AnhXe ax = db.AnhXe.FirstOrDefault(n => n.idSanPham == idSanPham && n.IsDefault == true);
            TenSanPham = sanpham.TenSanPham;
            AnhSP = ax.Anh;
            GiaTien = sanpham.GiaBan;
            giamgia = sanpham.GiamGia.PhanTramGiam;
            soluong = 1;
        }
    }
}