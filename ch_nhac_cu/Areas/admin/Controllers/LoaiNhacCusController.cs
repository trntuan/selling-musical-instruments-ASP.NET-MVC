using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ch_nhac_cu.Models;

namespace ch_nhac_cu.Areas.admin.Controllers
{
    public class LoaiNhacCusController : Controller
    {
        private QLCHNCEntities db = new QLCHNCEntities();

        // GET: admin/LoaiNhacCus
        [HttpGet]
        public ActionResult Index(string maLoaiNC = "", string TenLoaiNC = "")
        {
            ViewBag.MaLoaiNC = maLoaiNC;
            ViewBag.TenLoaiNC = TenLoaiNC;
            var loaiNhacCus = db.LoaiNhacCus.SqlQuery("LoaiNhacCu_TimKiem'" + maLoaiNC + "',N'" + TenLoaiNC + "'");
            if (loaiNhacCus.Count() == 0)
            {
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            }
            return View(loaiNhacCus.ToList());
        }

        // GET: admin/LoaiNhacCus/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiNhacCu loaiNhacCu = db.LoaiNhacCus.Find(id);
            if (loaiNhacCu == null)
            {
                return HttpNotFound();
            }
            return View(loaiNhacCu);
        }

        // GET: admin/LoaiNhacCus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/LoaiNhacCus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLoaiNC,TenLoaiNC, AnhLoaiNC")] LoaiNhacCu loaiNhacCu)
        {
            var imgLoaiNC = Request.Files["AnhNC"];

            string postedFileName = System.IO.Path.GetFileName(imgLoaiNC.FileName);
            //Lưu hình đại diện về Server
            var path = Server.MapPath("/Content/assets/img/LoaiNC/" + postedFileName);
            imgLoaiNC.SaveAs(path);
            if (ModelState.IsValid)
            {
                loaiNhacCu.AnhLoaiNC = postedFileName;
                db.LoaiNhacCus.Add(loaiNhacCu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiNhacCu);
        }

        // GET: admin/LoaiNhacCus/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiNhacCu loaiNhacCu = db.LoaiNhacCus.Find(id);
            if (loaiNhacCu == null)
            {
                return HttpNotFound();
            }
            return View(loaiNhacCu);
        }

        // POST: admin/LoaiNhacCus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLoaiNC,TenLoaiNC, AnhLoaiNC")] LoaiNhacCu loaiNhacCu)
        {
            var imgLoaiNC = Request.Files["AnhLoaiNC"];
                //Lấy thông tin từ input type=file có tên Avatar
                string postedFileName = System.IO.Path.GetFileName(imgLoaiNC.FileName);
                //Lưu hình đại diện về Server
                var path = Server.MapPath("/Content/assets/img/LoaiNC/" + postedFileName);
                imgLoaiNC.SaveAs(path);

            if (ModelState.IsValid)
            {
                //loaiNhacCu.AnhLoaiNC= postedFileName
                db.Entry(loaiNhacCu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiNhacCu);
        }

        // GET: admin/LoaiNhacCus/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiNhacCu loaiNhacCu = db.LoaiNhacCus.Find(id);
            if (loaiNhacCu == null)
            {
                return HttpNotFound();
            }
            return View(loaiNhacCu);
        }

        // POST: admin/LoaiNhacCus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            LoaiNhacCu loaiNhacCu = db.LoaiNhacCus.Find(id);
            db.LoaiNhacCus.Remove(loaiNhacCu);
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
