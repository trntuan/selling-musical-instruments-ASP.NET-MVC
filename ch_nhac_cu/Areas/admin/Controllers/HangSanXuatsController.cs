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
    public class HangSanXuatsController : Controller
    {
        private QLCHNCEntities db = new QLCHNCEntities();
        string LayMaHSX()
        {
            var maMax = db.HangSanXuats.ToList().Select(n => n.MaHSX).Max();
            int maHSX = int.Parse(maMax.Substring(3)) + 1;
            string HSX = String.Concat("0", maHSX.ToString());
            return "HSX" + HSX.Substring(maHSX.ToString().Length - 1);
        }

        // GET: admin/HangSanXuats
        [HttpGet]
        public ActionResult Index(string maHSX = "", string TenHSX = "", string DiaChi = "", string SDT ="")
        {

            ViewBag.MaHSX = maHSX;
            ViewBag.TenHSX = TenHSX;
            ViewBag.DiaChi = DiaChi;
            ViewBag.SDT = SDT;        
            var hangSanXuats = db.HangSanXuats.SqlQuery("HangSanXuat_TimKiem'" + maHSX + "',N'" + TenHSX + "',N'" + DiaChi + "',N'" + SDT + "'");

            if (hangSanXuats.Count() == 0)
            {
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            }
            return View(hangSanXuats.ToList());
        }

        // GET: admin/HangSanXuats/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangSanXuat hangSanXuat = db.HangSanXuats.Find(id);
            if (hangSanXuat == null)
            {
                return HttpNotFound();
            }
            return View(hangSanXuat);
        }

        // GET: admin/HangSanXuats/Create
        public ActionResult Create()
        {
            ViewBag.MaHSX = LayMaHSX();
            return View();
        }

        // POST: admin/HangSanXuats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHSX,TenHSX,DiaChi,SDT")] HangSanXuat hangSanXuat)
        {
            if (ModelState.IsValid)
            {
                hangSanXuat.MaHSX = LayMaHSX();
                db.HangSanXuats.Add(hangSanXuat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hangSanXuat);
        }

        // GET: admin/HangSanXuats/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangSanXuat hangSanXuat = db.HangSanXuats.Find(id);
            if (hangSanXuat == null)
            {
                return HttpNotFound();
            }
            return View(hangSanXuat);
        }

        // POST: admin/HangSanXuats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHSX,TenHSX,DiaChi,SDT")] HangSanXuat hangSanXuat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hangSanXuat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hangSanXuat);
        }

        // GET: admin/HangSanXuats/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangSanXuat hangSanXuat = db.HangSanXuats.Find(id);
            if (hangSanXuat == null)
            {
                return HttpNotFound();
            }
            return View(hangSanXuat);
        }

        // POST: admin/HangSanXuats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HangSanXuat hangSanXuat = db.HangSanXuats.Find(id);
            db.HangSanXuats.Remove(hangSanXuat);
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
