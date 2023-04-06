using ShopXeMay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopXeMay.App_Start
{
    public class AdminAuthorize : AuthorizeAttribute
    {
        BanXeMayEntities context =  new BanXeMayEntities(); 
        public int idPhanQuyen { get; set; }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string id = HttpContext.Current.Session["Id"].ToString();
            int temp = int.Parse(id);
            TaiKhoan tkSession = context.TaiKhoan.FirstOrDefault(m => m.Id == temp);
            if (tkSession != null)
            {
                BanXeMayEntities db = new BanXeMayEntities();
                var count = db.TaiKhoan.Count(m => m.Id == tkSession.Id & m.idPhanQuyen == idPhanQuyen);
                if (count != 0)
                {
                    return;
                }
                else
                {
                    var returnUrl = filterContext.RequestContext.HttpContext.Request.Url;
                    filterContext.Result = new RedirectToRouteResult(new
                        RouteValueDictionary(
                        new
                        {
                            Controller = "Home",
                            action = "Register",
                            returnUrl = returnUrl.ToString()
                        }));
                }
                return;
            }
            else
            {
                var returnUrl = filterContext.RequestContext.HttpContext.Request.Url;
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(
                    new
                    {
                        Controller = "home",
                        action = "Login",
                        returnUrl = returnUrl.ToString()
                    }));
            }

        }
    }
}