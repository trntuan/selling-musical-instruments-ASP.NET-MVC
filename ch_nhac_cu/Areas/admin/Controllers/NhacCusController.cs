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
    public class NhacCusController : Controller
    {
        private QLCHNCEntities db = new QLCHNCEntities();
        public bool IsQuantityValid(int soLuong)
        {
            // Lấy giá trị số lượng tối đa từ bảng ThamSo
            var soLuongToiDa = db.ThamSoes.FirstOrDefault(ts => ts.MaTS == "TS001")?.GiaTri;

            if (!string.IsNullOrEmpty(soLuongToiDa))
            {
                var soLuongToiDaInt = int.Parse(soLuongToiDa);
                // Kiểm tra số lượng nhạc cụ
                if (soLuong > soLuongToiDaInt)
                {
                    return false;
                }
            }

            return true;
        }

        // GET: Admin/NhacCus
        public string getNhacCu(string LoaiNC)
        {
            var maxMaNhacCu = db.NhacCus.Where(n => n.MaLoaiNC == LoaiNC).Max(n => n.MaNC);
            int maNC = int.Parse(maxMaNhacCu.Substring(LoaiNC.Length)) + 1;
            string NC = String.Concat("00", maNC.ToString());
            return LoaiNC + NC.ToString();
        }
        [HttpPost]
        public JsonResult GenerateMaNhacCu(string LoaiNC)
        {
            string maNhacCu = getNhacCu(LoaiNC); // Gọi hàm getNhacCu để tạo mã nhạc cụ
            return Json(maNhacCu);
        }


        [HttpGet]
        public ActionResult Index(string maNC = "", string TenNC = "", string ThoiGianBaoHanh = "", string DonGiaMin = "", string DonGiaMax = "", string maLoaiNC = "", string maNCC = "", string maHSX = "", string SoLuong = "")
        {
            string min = DonGiaMin, max = DonGiaMax, soLuong = SoLuong;
            ViewBag.MaNC = maNC;
            ViewBag.TenNC = TenNC;
            ViewBag.ThoiGianBaoHanh = ThoiGianBaoHanh;
            if (DonGiaMin == "")
            {
                ViewBag.DonGiaMin = "";
                min = "0";
            }
            else
            {
                ViewBag.DonGiaMin = DonGiaMin;
                min = DonGiaMin;
            }
            if (max == "")
            {
                max = Int32.MaxValue.ToString();
                ViewBag.DonGiaMax = "";// Int32.MaxValue.ToString(); 
            }
            else
            {
                ViewBag.DonGiaMax = DonGiaMax;
                max = DonGiaMax;
            }
            ViewBag.SoLuong = SoLuong;
            ViewBag.MaLoaiNC = new SelectList(db.LoaiNhacCus, "MaLoaiNC", "TenLoaiNC");
            ViewBag.MaHSX = new SelectList(db.HangSanXuats, "MaHSX", "TenHSX");
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC");
            var nhacCus = db.NhacCus.SqlQuery("NhacCu_TimKiem'" + maNC + "',N'" + TenNC + "',N'" + ThoiGianBaoHanh + "','" + min + "','" + max + "','" + SoLuong + "','" + maNCC + "'");

            if (SoLuong == "")
            {
                nhacCus = db.NhacCus.SqlQuery("NhacCu_TimKiem'" + maNC + "',N'" + TenNC + "',N'" + ThoiGianBaoHanh + "','" + min + "','" + max + "','" + maLoaiNC + "','" + maNCC + "','" + maHSX + "'");
            }

            if (nhacCus.Count() == 0)
            {
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            }
            return View(nhacCus.ToList());
        }




        // GET: Admin/NhacCus/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhacCu nhacCu = db.NhacCus.Find(id);
            if (nhacCu == null)
            {
                return HttpNotFound();
            }
            return View(nhacCu);
        }

        // GET: Admin/NhacCus/Create
        public ActionResult Create()
        {
            ViewBag.MaHSX = new SelectList(db.HangSanXuats, "MaHSX", "TenHSX");
            ViewBag.MaLoaiNC = new SelectList(db.LoaiNhacCus, "MaLoaiNC", "TenLoaiNC");
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC");// Lấy giá trị đã chọn từ dropdown list
            return View();
        }

        // POST: Admin/NhacCus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNC,TenNC,MoTa,AnhNC,SoLuong,ThoiGianBaoHanh,DonGia,MaLoaiNC,MaNCC,MaHSX")] NhacCu nhacCu)
        {
            var imgNC = Request.Files["AnhNC"];
         
            string postedFileName = System.IO.Path.GetFileName(imgNC.FileName);
            //Lưu hình đại diện về Server
            var path = Server.MapPath("/Content/assets/img/NhacCu/" + postedFileName);
            imgNC.SaveAs(path);
            if (ModelState.IsValid)
            {
                if (!IsQuantityValid(nhacCu.SoLuong))
                {
                    ModelState.AddModelError("", "Số lượng nhạc cụ vượt quá giới hạn cho phép.");
                    ViewBag.MaHSX = new SelectList(db.HangSanXuats, "MaHSX", "TenHSX", nhacCu.MaHSX);
                    ViewBag.MaLoaiNC = new SelectList(db.LoaiNhacCus, "MaLoaiNC", "TenLoaiNC", nhacCu.MaLoaiNC);
                    ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", nhacCu.MaNCC);
                    return View(nhacCu);
                }

                // Gán mã nhạc cụ tự động cho thuộc tính của đối tượng nhạc cụ
                nhacCu.MaNC = Request.Form["MaNC"];

                nhacCu.AnhNC = postedFileName;
                db.NhacCus.Add(nhacCu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHSX = new SelectList(db.HangSanXuats, "MaHSX", "TenHSX", nhacCu.MaHSX);
            ViewBag.MaLoaiNC = new SelectList(db.LoaiNhacCus, "MaLoaiNC", "TenLoaiNC", nhacCu.MaLoaiNC);
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", nhacCu.MaNCC);
           
            return View(nhacCu);
        }

        // GET: Admin/NhacCus/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhacCu nhacCu = db.NhacCus.Find(id);
            if (nhacCu == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHSX = new SelectList(db.HangSanXuats, "MaHSX", "TenHSX", nhacCu.MaHSX);
            ViewBag.MaLoaiNC = new SelectList(db.LoaiNhacCus, "MaLoaiNC", "TenLoaiNC", nhacCu.MaLoaiNC);
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", nhacCu.MaNCC);
            return View(nhacCu);
        }

        // POST: Admin/NhacCus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNC,TenNC,MoTa,AnhNC,SoLuong,ThoiGianBaoHanh,DonGia,MaLoaiNC,MaNCC,MaHSX")] NhacCu nhacCu)
        {
            var imgNC = Request.Files["AnhNC"];
            try
            {
                //Lấy thông tin từ input type=file có tên Avatar
                string postedFileName = System.IO.Path.GetFileName(imgNC.FileName);
                //Lưu hình đại diện về Server
                var path = Server.MapPath("/Content/assets/img/NhacCu/" + postedFileName);
                imgNC.SaveAs(path);
            }
            catch
            { }
            if (ModelState.IsValid)
            {
                db.Entry(nhacCu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHSX = new SelectList(db.HangSanXuats, "MaHSX", "TenHSX", nhacCu.MaHSX);
            ViewBag.MaLoaiNC = new SelectList(db.LoaiNhacCus, "MaLoaiNC", "TenLoaiNC", nhacCu.MaLoaiNC);
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", nhacCu.MaNCC);
            return View(nhacCu);
        }

        // GET: Admin/NhacCus/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhacCu nhacCu = db.NhacCus.Find(id);
            if (nhacCu == null)
            {
                return HttpNotFound();
            }
            return View(nhacCu);
        }

        // POST: Admin/NhacCus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NhacCu nhacCu = db.NhacCus.Find(id);
            db.NhacCus.Remove(nhacCu);
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
