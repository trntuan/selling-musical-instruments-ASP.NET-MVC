using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ch_nhac_cu.Models;
using Microsoft.SqlServer.Server;

namespace ch_nhac_cu.Controllers
{
    public class HomeController : Controller
    {
        private QLCHNCEntities db = new QLCHNCEntities();


        string LayMaKH()
        {
            var maMax = db.KhachHangs.ToList().Select(n => n.MaKH).Max();
            int maKH = int.Parse(maMax.Substring(2)) + 1;
            string KH = String.Concat("000", maKH.ToString());
            return "KH" + KH.Substring(maKH.ToString().Length - 1);
        }
        //create a string MD5
        public static string GetMD5(string str)
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
        // GET: Home
        public ActionResult Index()
        {
            var model = new HomeModel
            {
                NhacCus = db.NhacCus.Include(n => n.HangSanXuat).Include(n => n.LoaiNhacCu).Include(n => n.NhaCungCap).ToList(),
                LoaiNhacCus = db.LoaiNhacCus.ToList(),
                CTHoaDons = db.CTHoaDons.ToList()
            };
            Session["DSLoaiNhac"] = model.LoaiNhacCus;
            Session["SPdeXuat"] = model.NhacCus;


            return View(model);

        }

        // GET: Home/Details/5
        public ActionResult Register()
        {


            return View();

        }
        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(KhachHang _user)
        {
            if (ModelState.IsValid)
            {
                var check = db.KhachHangs.FirstOrDefault(s => s.Email == _user.Email );

                _user.MaKH = LayMaKH();
                if (check == null)
                {
                    _user.MatKhau =  GetMD5(_user.MatKhau);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.KhachHangs.Add(_user);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Đăng ký thành công!";
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "Email đã tồn tại!";
                    return View(_user);
                }


            }
            return RedirectToAction("Register");


        }

       

        
        public ActionResult ForgotPassword()
        {

            return View();

        }

        public ActionResult Login()
        {

            if (TempData.ContainsKey("SuccessMessage"))
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
                TempData.Remove("SuccessMessage");
            }
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(password);
                var data = db.KhachHangs.Where(s => s.Email == email && s.MatKhau == f_password).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["idMaKH"] = data.FirstOrDefault().MaKH;
                    Session["HoTenKH"] = data.FirstOrDefault().HoTenKH;
                    Session["DiaChi"] = data.FirstOrDefault().DiaChi;

                
                    return RedirectToAction("Index");
                }
                else
                {
                   TempData["ErrorMessage"] = "Đăng nhập thất bại! tài khoản hoặc mật khẩu không chính xác";

                    ViewBag.ErrorMessage = TempData["ErrorMessage"];
                    TempData.Remove("ErrorMessage");
                return View("Login");
                }
                
            }
            return RedirectToAction("Index");
        }
        public ActionResult Info()
        {
            if (Session["idMaKH"] != null)
            {
                KhachHang khachHang = db.KhachHangs.Find(Session["idMaKH"]);
                if (khachHang == null)
                {
                    return HttpNotFound();
                }
                return View(khachHang);

            }
            else
            {

                return View("Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Info ([Bind(Include = "HoTenKH, SDT, Email, DiaChi")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                // Lấy thông tin khách hàng từ cơ sở dữ liệu dựa trên khóa chính MaKH
                KhachHang existingKhachHang = db.KhachHangs.Find(Session["idMaKH"]);
               
                    if (existingKhachHang != null)
                        {
                  
                        // Cập nhật các trường cụ thể
                        existingKhachHang.HoTenKH = khachHang.HoTenKH;
                        existingKhachHang.SDT = khachHang.SDT;
                        existingKhachHang.Email = khachHang.Email;
                        existingKhachHang.DiaChi = khachHang.DiaChi;
                        existingKhachHang.TaiKhoan = khachHang.Email;
                        Session["DiaChi"] = khachHang.DiaChi;
                        db.SaveChanges();
                  
                    
                }
                

                return RedirectToAction("Info");
            }

            return View(khachHang);
        }

        public ActionResult changePassword()
        {
            if (Session["idMaKH"] != null)
            {
                KhachHang khachHang = db.KhachHangs.Find(Session["idMaKH"]);
                if (khachHang == null)
                {
                    return HttpNotFound();
                }
                return View();

            }
            else
            {

                return View("Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult changePassword(string password, string changePassword)
        {
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(password);
                string makh = Session["idMaKH"].ToString();
                var data = db.KhachHangs.Where(s => s.MaKH == makh && s.MatKhau == f_password).ToList();
                if (data.Count() > 0)
                {
                    KhachHang existingKhachHang = db.KhachHangs.Find(makh);
                    var w_password = GetMD5(changePassword);
                    existingKhachHang.MatKhau = w_password;
                    db.SaveChanges();
                   
                    TempData["SuccessMessage"] = "đổi mật khẩu thành công";

                    ViewBag.ErrorMessage = TempData["SuccessMessage"];
                    TempData.Remove("SuccessMessage");
                    return View("changePassword");
                }
                else
                {
                    TempData["ErrorMessage"] = "mật khẩu không chính xác";

                    ViewBag.ErrorMessage = TempData["ErrorMessage"];
                    TempData.Remove("ErrorMessage");
                    return View("changePassword");
                }

            }
            return RedirectToAction("changePassword");
        }


        public ActionResult bill()
        {
            if (Session["idMaKH"] != null)
            {
                string makh = Session["idMaKH"].ToString();
                var hoaDons = db.HoaDons.Where(s => s.MaKH == makh).Include(h => h.KhachHang).Include(h => h.NhanVien).ToList();
                if (hoaDons.Count() == 0)
                {
                    return RedirectToAction("Index");
                }
                return View(hoaDons);

            }
            else
            {
                return View("Login");
            }
            
        }


        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
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
