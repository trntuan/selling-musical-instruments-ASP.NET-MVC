using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ch_nhac_cu.Models;

namespace ch_nhac_cu.Areas.admin.Controllers
{
    public class NhanViensController : Controller
    {
        private QLCHNCEntities db = new QLCHNCEntities();
        string LayMaNV()
        {
            var maMax = db.NhanViens.ToList().Select(n => n.MaNV).Max();
            int maNV = int.Parse(maMax.Substring(2)) + 1;
            string NCC = String.Concat("0", maNV.ToString());
            return "NV" + NCC.Substring(maNV.ToString().Length - 1);
        }

        // GET: admin/NhanViens
        [HttpGet]
        public ActionResult Index(string maNV = "", string hoTenNV = "", string SDT = "", string Email = "", string DiaChi = "", string tenDN = "", string maCV = "")
        {

            ViewBag.MaNV = maNV;
            ViewBag.HoTenNV = hoTenNV;
            ViewBag.SDT = SDT;
            ViewBag.Email = Email;
            ViewBag.DiaChi = DiaChi;
            ViewBag.TenDN = tenDN;
            ViewBag.MaCV = new SelectList(db.ChucVus, "MaCV", "TenCV");
            var nhanViens = db.NhanViens.SqlQuery("NhanVien_TimKiem'" + maNV + "',N'" + hoTenNV + "',N'" + SDT + "',N'" + Email + "',N'" + DiaChi + "',N'" + tenDN + "',N'" + maCV + "'");

            if (nhanViens.Count() == 0)
            {
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            }
            return View(nhanViens.ToList());
        }

        // GET: admin/NhanViens/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }
        // Action đăng ký
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(NhanVien model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem mã chức vụ của nhân viên có phải là 'QL' hay không

                // Kiểm tra xem tên đăng nhập nhân viên đã tồn tại trong CSDL chưa
                bool isTenDNExists = db.NhanViens.Any(nv => nv.TenDN == model.TenDN);

                if (!isTenDNExists)
                {
                    var nhanVien = new NhanVien
                    {
                        MaNV = model.MaNV,
                        HoTenNV = model.HoTenNV,
                        SDT = model.SDT,
                        Email = model.Email,
                        DiaChi = model.DiaChi,
                        TenDN = model.TenDN,
                        MatKhau = HashPassword(model.MatKhau), // Mã hóa mật khẩu
                        MaCV = model.MaCV // Thiết lập mã chức vụ
                    };
                    // Lưu đối tượng NhanVien vào CSDL
                    db.NhanViens.Add(nhanVien);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã của nhân viên đã tồn tại");
                    ModelState.AddModelError("", "Vui lòng nhập tên khác");

                }
            }

            return View(model);
        }

        // GET: admin/NhanViens/Create
        public ActionResult Create()
        {
            ViewBag.MaNV = LayMaNV();
            ViewBag.MaCV = new SelectList(db.ChucVus, "MaCV", "TenCV");
            return View();
        }

        // POST: admin/NhanViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNV,HoTenNV,SDT,Email,DiaChi,TenDN,MatKhau,MaCV")] NhanVien nhanVien)
        {
            string matKhau = Request["MatKhau"];
            string tenDN = Request["TenDN"];
            if (ModelState.IsValid)
            {
                // Kiểm tra xem tên đăng nhập nhân viên đã tồn tại trong CSDL chưa
                bool isTenDNExists = db.NhanViens.Any(nv => nv.TenDN == tenDN);
                if (!isTenDNExists)
                {
                    nhanVien.MatKhau = HashPassword(matKhau);
                    nhanVien.MaNV = LayMaNV();
                    db.NhanViens.Add(nhanVien);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã của nhân viên đã tồn tại");
                    ModelState.AddModelError("", "Vui lòng nhập tên khác");

                }
            }

            ViewBag.MaCV = new SelectList(db.ChucVus, "MaCV", "TenCV", nhanVien.MaCV);
            return View(nhanVien);
        }

        // GET: admin/NhanViens/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaCV = new SelectList(db.ChucVus, "MaCV", "TenCV", nhanVien.MaCV);
            return View(nhanVien);
        }

        // POST: admin/NhanViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNV,HoTenNV,SDT,Email,DiaChi,TenDN,MatKhau,MaCV")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaCV = new SelectList(db.ChucVus, "MaCV", "TenCV", nhanVien.MaCV);
            return View(nhanVien);
        }

        // GET: admin/NhanViens/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: admin/NhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NhanVien nhanVien = db.NhanViens.Find(id);
            db.NhanViens.Remove(nhanVien);
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
        // Mã hóa mật khẩu sử dụng SHA256
        //private string HashPassword(string password)
        //{
        //    using (SHA256 sha256 = SHA256.Create())
        //    {
        //        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        //        StringBuilder builder = new StringBuilder();
        //        for (int i = 0; i < bytes.Length; i++)
        //        {
        //            builder.Append(bytes[i].ToString("x2"));
        //        }
        //        return builder.ToString();
        //    }
        //}

        public static string HashPassword(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}
