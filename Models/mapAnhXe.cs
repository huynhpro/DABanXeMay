using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopXeMay.Models
{
    public class mapAnhXe
    {
        public List<AnhXe> GetAnhXeByID(int id)
        {
            var db = new BanXeMayEntities();
            return db.AnhXe.Where(m => m.idSanPham == id).ToList();
        }
        public AnhXe GetAnhXeDefault(int id)
        {
            var db = new BanXeMayEntities();
            return db.AnhXe.FirstOrDefault(m => m.idSanPham == id && m.IsDefault == true);
        }
    }
}