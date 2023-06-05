using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;
using ch_nhac_cu.Models;



namespace ch_nhac_cu.Areas.admin.Controllers
{
    public class ReportsController : Controller
    {
        private QLCHNCEntities db = new QLCHNCEntities();

        // GET: admin/Reports
       

        public ActionResult Index(DateTime? fromDate, DateTime? toDate)
        {

                ViewBag.ValueFromDate = fromDate?.ToString("yyyy-MM-dd") ;
                ViewBag.ValueToDate = toDate?.ToString("yyyy-MM-dd");
                ViewBag.printFromDate = fromDate?.ToString("dd 'tháng' MM 'năm' yyyy");
                ViewBag.printToDate = toDate?.ToString("dd 'tháng' MM 'năm' yyyy");

            var salesReport = db.Database.SqlQuery<SalesReportModel>(
                    "EXEC GetSalesReport @FromDate, @ToDate",
                    new SqlParameter("FromDate", fromDate ?? SqlDateTime.MinValue),
                    new SqlParameter("ToDate", toDate ?? SqlDateTime.MaxValue)
                ).ToList();
          
                return View(salesReport);

        }



        // GET: admin/Reports/Details/5
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

        // GET: admin/Reports/Report
        public ActionResult Report(string id)
        {
            var cTHoaDons = db.CTHoaDons.Include(c => c.HoaDon).Include(c => c.NhacCu);
            return View(cTHoaDons.ToList());
        }
        // GET: admin/Reports/Create
        public ActionResult Create()
        {
            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "MaKH");
            ViewBag.MaNC = new SelectList(db.NhacCus, "MaNC", "TenNC");
            return View();
        }

        // POST: admin/Reports/Create
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

        // GET: admin/Reports/Edit/5
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

        // POST: admin/Reports/Edit/5
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

        // GET: admin/Reports/Delete/5
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

        // POST: admin/Reports/Delete/5
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
