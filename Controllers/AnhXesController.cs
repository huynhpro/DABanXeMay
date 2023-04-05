using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopXeMay.Models;

namespace ShopXeMay.Controllers
{
    public class AnhXesController : Controller
    {
        private BanXeMayEntities db = new BanXeMayEntities();

        // GET: AnhXes
        public ActionResult Index()
        {
            var anhXe = db.AnhXe.Include(a => a.SanPham);
            return View(anhXe.ToList());
        }

        // GET: AnhXes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnhXe anhXe = db.AnhXe.Find(id);
            if (anhXe == null)
            {
                return HttpNotFound();
            }
            return View(anhXe);
        }
        // GET: Home  
        public ActionResult UploadFiles()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(List<HttpPostedFileBase> postedFiles)
        {
            string path = Server.MapPath("~/images/product/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach (HttpPostedFileBase pFile in postedFiles)
            {
                if (pFile != null)
                {
                    string fileName = Path.GetFileName(pFile.FileName);
                    pFile.SaveAs(path + fileName);
                    ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
                }
            }

            return View();
        }
        [HttpPost]  //Now we are getting array of files check sign []
        public ActionResult UploadFiles(HttpPostedFileBase[] files)
        {

            //iterating through multiple file collection   
            foreach (HttpPostedFileBase file in files)
            {
                //Checking file is available to save.  
                if (file != null)
                {
                    var InputFileName = Path.GetFileName(file.FileName);
                    var ServerSavePath = Path.Combine(Server.MapPath("~/images/product/") + InputFileName);
                    //Save file to server folder  
                    file.SaveAs(ServerSavePath);
                    //assigning file uploaded status to ViewBag for showing message to user.  
                    ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                }

            }

            return View();
        }
        // GET: AnhXes/Create
        public ActionResult Create()
        {
            ViewBag.idSanPham = new SelectList(db.SanPham, "ID", "TenSanPham");
            return View();
        }

        // POST: AnhXes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idAnh,idSanPham,Anh,IsDefault")] AnhXe anhXe)
        {
            if (ModelState.IsValid)
            {
                db.AnhXe.Add(anhXe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idSanPham = new SelectList(db.SanPham, "ID", "TenSanPham", anhXe.idSanPham);
            return View(anhXe);
        }

        // GET: AnhXes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnhXe anhXe = db.AnhXe.Find(id);
            if (anhXe == null)
            {
                return HttpNotFound();
            }
            ViewBag.idSanPham = new SelectList(db.SanPham, "ID", "TenSanPham", anhXe.idSanPham);
            return View(anhXe);
        }

        // POST: AnhXes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idAnh,idSanPham,Anh,IsDefault")] AnhXe anhXe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(anhXe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idSanPham = new SelectList(db.SanPham, "ID", "TenSanPham", anhXe.idSanPham);
            return View(anhXe);
        }

        // GET: AnhXes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnhXe anhXe = db.AnhXe.Find(id);
            if (anhXe == null)
            {
                return HttpNotFound();
            }
            return View(anhXe);
        }

        // POST: AnhXes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AnhXe anhXe = db.AnhXe.Find(id);
            db.AnhXe.Remove(anhXe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
