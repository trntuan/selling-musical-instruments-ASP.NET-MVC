using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ch_nhac_cu.Models;

namespace ch_nhac_cu.Areas.admin.Controllers
{
    public class BillsController : Controller
    {
        private QLCHNCEntities db = new QLCHNCEntities();

        // GET: admin/Bills
        public ActionResult Index()
        {
            var hoaDons = db.HoaDons.Include(h => h.KhachHang).Include(h => h.NhanVien);
            return View(hoaDons.ToList());
        }
        public ActionResult HoaDonDaDuyet()
        {
            var hoaDons = db.HoaDons.Include(h => h.KhachHang).Include(h => h.NhanVien);
            return View(hoaDons.ToList());
        }
        public ActionResult HoaDonChuaDuyet()
        {
            var hoaDons = db.HoaDons.Include(h => h.KhachHang).Include(h => h.NhanVien);
            return View(hoaDons.ToList());
        }
        public ActionResult CTHD(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cTHoaDons = db.CTHoaDons.Include(c => c.HoaDon).Include(c => c.NhacCu).Where(n => n.MaHD == id);
            ViewBag.MaNC = new SelectList(db.NhacCus, "MaNC", "TenNC");
            if (cTHoaDons == null)
            {
                return HttpNotFound();
            }
            return View(cTHoaDons.ToList());
        }

        // GET: admin/Bills/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // GET: admin/Bills/Create
        public ActionResult Create()
        {
            var nhacCus = db.NhacCus.Include(n => n.HangSanXuat).Include(n => n.LoaiNhacCu).Include(n => n.NhaCungCap);
      
            return View(nhacCus.ToList());
        }

        // POST: admin/Bills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHD,NgayDH,NgayGH,MaKH,MaNV,TinhTrangDuyet,TinhTrangDonHang")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.HoaDons.Add(hoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTenKH", hoaDon.MaKH);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV", hoaDon.MaNV);
            return View(hoaDon);
        }

        // GET: admin/Bills/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTenKH", hoaDon.MaKH);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV", hoaDon.MaNV);
            return View(hoaDon);
        }

        // POST: admin/Bills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHD,NgayDH,NgayGH,MaKH,MaNV,TinhTrangDuyet,TinhTrangDonHang")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoaDon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTenKH", hoaDon.MaKH);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "HoTenNV", hoaDon.MaNV);
            return View(hoaDon);
        }

        // GET: admin/Bills/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // POST: admin/Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HoaDon hoaDon = db.HoaDons.Find(id);
            db.HoaDons.Remove(hoaDon);
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
