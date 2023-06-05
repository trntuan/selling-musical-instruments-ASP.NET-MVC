using ch_nhac_cu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ch_nhac_cu.Controllers
{
    public class ProductController : Controller
    {
        private QLCHNCEntities db = new QLCHNCEntities();
        // GET: Product
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
    }
}