using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;

namespace ch_nhac_cu.Areas.admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: admin/Home
        public ActionResult Index()
        {
            if (Session["HoTen"]!=null)
            {
                return View();
            }
            else return RedirectToAction("Login", "Account");

        }
    }
}