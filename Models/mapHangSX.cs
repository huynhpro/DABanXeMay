using ShopXeMay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopXeMay.Models
{
    public class mapHangSX
    {
        public List<HangSX> DanhSach()
        {
            var db = new BanXeMayEntities();
            var hangSX = db.HangSX.ToList();
            return hangSX;
        }
        public List<HangSX> TimKiem(string timkiem)
        {
            var db = new BanXeMayEntities();
            var hangSX = db.HangSX.Where(m => m.TenHangSX.ToLower().Contains(timkiem.ToLower()) == true || string.IsNullOrEmpty(timkiem)).ToList();
            var hangSX1 = db.HangSX.Where(m => m.TenHangSX.ToLower().Contains(timkiem.ToLower()) == true || string.IsNullOrEmpty(timkiem)).ToList();
            return hangSX;
        }
    }
}