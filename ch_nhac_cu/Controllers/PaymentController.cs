using ch_nhac_cu.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ch_nhac_cu.Controllers
{
    public class PaymentController : Controller
    {
        private QLCHNCEntities db = new QLCHNCEntities();

        string LayMaHD()
        {
            var maMax = db.HoaDons.ToList().Select(n => n.MaHD).Max();
            int maHD = int.Parse(maMax.Substring(2)) + 1;
            string HD = String.Concat("0000", maHD.ToString());
            return "HD" + HD.Substring(maHD.ToString().Length - 1);
        }
        public ActionResult Index()
        {
            if (Session["idMaKH"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                //lấy thông giỏ hàng từ biến session
                var lstCart = (List<CartModel>)Session["cart"];
                //gán dữ liệu cho bảng Order
                string maHoaDon = LayMaHD();
                HoaDon hoaDon = new HoaDon();

                hoaDon.MaHD = maHoaDon;
                hoaDon.NgayDH = DateTime.Now;
                hoaDon.NgayGH = DateTime.Now.AddDays(3);
                hoaDon.MaKH = Session["idMaKH"].ToString();
                hoaDon.MaNV = null;
                hoaDon.TinhTrangDuyet = false;
                hoaDon.TinhTrangDonHang = false;
                int tong = 0;
                foreach (var item in lstCart)
                {
                    tong += (item.Product.DonGia * item.Quantity);
                }
                hoaDon.TongTien = tong;
                // lưu
                db.HoaDons.Add(hoaDon);
                db.SaveChanges();
                //Lấy hoaDon vừa mới tạo để lưu vào bảng CTHoaDon.
                List<CTHoaDon> dsctHD = new List<CTHoaDon>();

                foreach (var item in lstCart)
                {
                    CTHoaDon cthd = new CTHoaDon();
                    cthd.MaHD = maHoaDon;
                    cthd.MaNC = item.Product.MaNC;
                    cthd.SoLuongBan = item.Quantity;
                    cthd.DonGiaBan = item.Product.DonGia;

                    dsctHD.Add(cthd);

                }
                db.CTHoaDons.AddRange(dsctHD);
                db.SaveChanges();
                Session.Remove("cart");
                Session["count"] = null;

            }
            return View();
         }
            
    }
}