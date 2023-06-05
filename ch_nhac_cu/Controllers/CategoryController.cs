using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ch_nhac_cu.Models;

namespace ch_nhac_cu.Controllers
{
    public class CategoryController : Controller
    {
        private QLCHNCEntities db = new QLCHNCEntities();

        // GET: Category
       
        public ActionResult TimKiemNhacCu(string TenNC = "", string DonGiaMin = "", string DonGiaMax = "",string maLoaiNC = "")
        {
            string min, max;
           
            ViewBag.TenNC = TenNC;
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
            if (DonGiaMax == "")
            {
                max = Int32.MaxValue.ToString();
                ViewBag.DonGiaMax = "";// Int32.MaxValue.ToString(); 
            }
            else
            {
                ViewBag.DonGiaMax = DonGiaMax;
                max = DonGiaMax;
            }
   
            ViewBag.MaLoaiNC = new SelectList(db.LoaiNhacCus, "MaLoaiNC", "TenLoaiNC");
            ViewBag.MaHSX = new SelectList(db.HangSanXuats, "MaHSX", "TenHSX");
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC");
            var nhacCus = db.NhacCus.SqlQuery("NhacCu_TimKiem'" + "" + "',N'" + TenNC + "',N'" + "" + "','" + min + "','" + max + "','" + maLoaiNC + "'");

         

            if (nhacCus.Count() == 0)
            {
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            }
            return View(nhacCus.ToList());
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
