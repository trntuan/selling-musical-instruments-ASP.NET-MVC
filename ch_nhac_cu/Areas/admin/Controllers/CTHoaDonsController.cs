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
    public class CTHoaDonsController : Controller
    {
        private QLCHNCEntities db = new QLCHNCEntities();

        // GET: admin/CTHoaDons
        public ActionResult Index()
        {
            var cTHoaDons = db.CTHoaDons.Include(c => c.HoaDon).Include(c => c.NhacCu);
            ViewBag.MaNC = new SelectList(db.NhacCus, "MaNC", "TenNC");
            return View(cTHoaDons.ToList());
        }

        // GET: admin/CTHoaDons/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTHoaDon cTHoaDon = db.CTHoaDons.Find(id);
            if (cTHoaDon == null)
            {
                return HttpNotFound();
            }
            return View(cTHoaDon);
        }

        // GET: admin/CTHoaDons/Create
        public ActionResult Create()
        {
            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "MaKH");
            ViewBag.MaNC = new SelectList(db.NhacCus, "MaNC", "TenNC");
            return View();
        }

        // POST: admin/CTHoaDons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHD,MaNC,SoLuongBan,DonGiaBan")] CTHoaDon cTHoaDon)
        {
            if (ModelState.IsValid)
            {
                db.CTHoaDons.Add(cTHoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "MaKH", cTHoaDon.MaHD);
            ViewBag.MaNC = new SelectList(db.NhacCus, "MaNC", "TenNC", cTHoaDon.MaNC);
            return View(cTHoaDon);
        }

        // GET: admin/CTHoaDons/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTHoaDon cTHoaDon = db.CTHoaDons.Find(id);
            if (cTHoaDon == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "MaKH", cTHoaDon.MaHD);
            ViewBag.MaNC = new SelectList(db.NhacCus, "MaNC", "TenNC", cTHoaDon.MaNC);
            return View(cTHoaDon);
        }

        // POST: admin/CTHoaDons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHD,MaNC,SoLuongBan,DonGiaBan")] CTHoaDon cTHoaDon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cTHoaDon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "MaKH", cTHoaDon.MaHD);
            ViewBag.MaNC = new SelectList(db.NhacCus, "MaNC", "TenNC", cTHoaDon.MaNC);
            return View(cTHoaDon);
        }

        // GET: admin/CTHoaDons/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTHoaDon cTHoaDon = db.CTHoaDons.Find(id);
            if (cTHoaDon == null)
            {
                return HttpNotFound();
            }
            return View(cTHoaDon);
        }

        // POST: admin/CTHoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CTHoaDon cTHoaDon = db.CTHoaDons.Find(id);
            db.CTHoaDons.Remove(cTHoaDon);
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
