using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopXeMay.Models;

namespace ShopXeMay.Dao
{
    public class TaiKhoanDao
    {
        private BanXeMayEntities db = new BanXeMayEntities();
        public long InsertForFacebook(TaiKhoan entity)
        {
            var user = db.TaiKhoan.SingleOrDefault(x => x.TenDangNhap == entity.TenDangNhap);
            if (user == null)
            {
                db.TaiKhoan.Add(entity);
                db.SaveChanges();
                return entity.Id;
            }
            else
            {
                return user.Id;
            }
        }


        public TaiKhoan SaveTaiKhoan(TaiKhoan taiKhoan)
        {
            db.TaiKhoan.Add(taiKhoan);
            db.SaveChanges();
            return null;
        }
    }
}