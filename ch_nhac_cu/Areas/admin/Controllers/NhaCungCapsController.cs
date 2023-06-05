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
    public class NhaCungCapsController : Controller
    {
        private QLCHNCEntities db = new QLCHNCEntities();
        string LayMaNCC()
        {
            var maMax = db.NhaCungCaps.ToList().Select(n => n.MaNCC).Max();
            int maNCC = int.Parse(maMax.Substring(3)) + 1;
            string NCC = String.Concat("0", maNCC.ToString());
            return "NCC" + NCC.Substring(maNCC.ToString().Length - 1);
        }

        // GET: admin/NhaCungCaps
        [HttpGet]
        public ActionResult Index(string maNCC = "", string TenNCC = "", string SDT = "", string Email = "", string DiaChi = "")
        {

            ViewBag.MaNCC = maNCC;
            ViewBag.TenNCC = TenNCC;
            ViewBag.SDT = SDT;
            ViewBag.Email = Email;
            ViewBag.DiaChi = DiaChi;
            var nhaCungCaps = db.NhaCungCaps.SqlQuery("NhaCungCap_TimKiem'" + maNCC + "',N'" + TenNCC + "',N'" + SDT + "',N'" + Email + "',N'" + DiaChi + "'");

            if (nhaCungCaps.Count() == 0)
            {
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            }
            return View(nhaCungCaps.ToList());
        }

        // GET: admin/NhaCungCaps/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCungCap nhaCungCap = db.NhaCungCaps.Find(id);
            if (nhaCungCap == null)
            {
                return HttpNotFound();
            }
            return View(nhaCungCap);
        }

        // GET: admin/NhaCungCaps/Create
        public ActionResult Create()
        {
            ViewBag.MaNCC = LayMaNCC();
            return View();
        }

        // POST: admin/NhaCungCaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNCC,TenNCC,SDT,DiaChi,Email")] NhaCungCap nhaCungCap)
        {
            if (ModelState.IsValid)
            {
                nhaCungCap.MaNCC=LayMaNCC();
                db.NhaCungCaps.Add(nhaCungCap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nhaCungCap);
        }

        // GET: admin/NhaCungCaps/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCungCap nhaCungCap = db.NhaCungCaps.Find(id);
            if (nhaCungCap == null)
            {
                return HttpNotFound();
            }
            return View(nhaCungCap);
        }

        // POST: admin/NhaCungCaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNCC,TenNCC,SDT,DiaChi,Email")] NhaCungCap nhaCungCap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhaCungCap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhaCungCap);
        }

        // GET: admin/NhaCungCaps/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCungCap nhaCungCap = db.NhaCungCaps.Find(id);
            if (nhaCungCap == null)
            {
                return HttpNotFound();
            }
            return View(nhaCungCap);
        }

        // POST: admin/NhaCungCaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NhaCungCap nhaCungCap = db.NhaCungCaps.Find(id);
            db.NhaCungCaps.Remove(nhaCungCap);
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
