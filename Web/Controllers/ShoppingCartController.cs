using Model.Dao;
using Model.EL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using System.Web.Script.Serialization;

namespace Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        DataContext db = new DataContext();

        public ActionResult ShowCart()
        {
            var cart = Session["cart"];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        // Thêm sản phẩm
        public ActionResult AddItem(int id)
        {
            var product = new ProductDAO().GetProductById(id);
            if (product == null)
            {
                return RedirectToAction("ShowCart", "ShoppingCart");
            }
            var cart = Session["cart"];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.product.Id == id))
                {
                    foreach (var item in list)
                    {
                        if (item.product.Id == id)
                        {
                            item.quantity += 1;
                        }
                    }
                }
                else
                {
                    CartItem item = new CartItem();
                    item.product = product;
                    item.quantity = 1;
                    list.Add(item);
                    Session["cart"] = list;
                }
            }
            else
            {
                CartItem item = new CartItem();
                item.product = product;
                item.quantity = 1;
                var list = new List<CartItem>();
                Session["cart"] = list;
                list.Add(item);
            }

            return RedirectToAction("ShowCart", "ShoppingCart");
        }

        [Route("RemoveCart")]
        [HttpPost]
        public JsonResult RemoveCart(int id)
        {
            var list = (List<CartItem>)Session["cart"];
            var cart = list.Find(x => x.product.Id == id);
            if (cart != null)
            {
                list.Remove(cart);
            }
            Session["cart"] = list;
            return Json(new
            {
                status = true,
                message = "success"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateCart(int id, int quantity)
        {
            var sessionCart = (List<CartItem>)Session["cart"];

            foreach (var item in sessionCart)
            {
                var cartItem = sessionCart.Find(x => x.product.Id == id);
                if (cartItem != null)
                {
                    cartItem.quantity = quantity;
                }
            }
            Session["cart"] = sessionCart;
            return Json(new
            {
                status = true,
                message = "success"
            }, JsonRequestBehavior.AllowGet);
        }

        [Route("ClearCart")]
        [HttpGet]
        public JsonResult ClearCart()
        {
            Session["cart"] = null;
            return Json(new
            {
                status = true,
                message = "success"
            }, JsonRequestBehavior.AllowGet);
        }

    }
}