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
    public class CartController : Controller
    {
        private QLCHNCEntities db = new QLCHNCEntities();

        // GET: Cart
        public ActionResult Index()
        {
            
            return View((List<CartModel>)Session["cart"]);
        }
       

        public ActionResult AddToCart(string id, int quantity)
        {
            if (Session["cart"] == null)
            {
                List<CartModel> cart = new List<CartModel>();
                cart.Add(new CartModel { Product = db.NhacCus.Find(id), Quantity = quantity });
                Session["cart"] = cart;
                Session["count"] = 1;
            }
            else
            {
                List<CartModel> cart = (List<CartModel>)Session["cart"];
                //kiểm tra sản phẩm có tồn tại trong giỏ hàng chưa???
                int index = isExist(id);
                if (index != -1)
                {
                    //nếu sp tồn tại trong giỏ hàng thì cộng thêm số lượng
                    cart[index].Quantity += quantity;
                }
                else
                {
                    //nếu không tồn tại thì thêm sản phẩm vào giỏ hàng
                    cart.Add(new CartModel { Product = db.NhacCus.Find(id), Quantity = quantity });
                    //Tính lại số sản phẩm trong giỏ hàng
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                }
                Session["cart"] = cart;
            }
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }

        public ActionResult UpdateCart(string id, int quantity)
        {
            if (Session["cart"] != null)
            {
                List<CartModel> cart = (List<CartModel>)Session["cart"];
                int index = isExist(id);
                if (index != -1)
                {
                    // Cập nhật số lượng sản phẩm trong giỏ hàng
                    cart[index].Quantity = quantity;
                    Session["cart"] = cart;
                    return Json(new { Message = "Cập nhật thành công", JsonRequestBehavior.AllowGet });
                }
            }
            return Json(new { Message = "Cập nhật thất bại", JsonRequestBehavior.AllowGet });
        }

        private int isExist(string id)
        {
            List<CartModel> cart = (List<CartModel>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.MaNC.Equals(id))
                    return i;
            return -1;
        }

        //xóa sản phẩm khỏi giỏ hàng theo id
        public ActionResult Remove(string Id)
        {
            List<CartModel> li = (List<CartModel>)Session["cart"];
            li.RemoveAll(x => x.Product.MaNC == Id);
            Session["cart"] = li;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }
        // lấy số lượng sản phẩm trong giỏ    
        public ActionResult GetCartItemCount()
        {
            List<CartModel> cart = (List<CartModel>)Session["cart"];
            if (cart != null)
            {
                return Json(cart.Count, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
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
