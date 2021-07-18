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

        [HttpGet]
        public ActionResult InfoCart()
        {
            double amount = 0;
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            int count = 0;
            if (cart != null)
            {
                foreach (var ca in cart)
                {
                    count += ca.quantity;
                    amount += (ca.product.UnitPrice * ca.quantity * (1 - ca.product.Discount));
                }
            }

            return Json(new
            {
                count = count,
                amount = amount
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ShowCart()
        {
            HttpCookie http = new HttpCookie("link");
            string url = Request.Url.LocalPath;
            http.Value = url;
            http.Expires = DateTime.Today.AddDays(1);
            Response.Cookies.Add(http);

            var cart = Session["cart"];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        // Thêm sản phẩm
        [HttpPost]
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

        [HttpPost]
        public ActionResult RemoveItem(int id)
        {
            var list = (List<CartItem>)Session["cart"];
            var cart = list.FirstOrDefault(x => x.product.Id == id);
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
        public ActionResult UpdateCart(int id, int quantity)
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


        public ActionResult ClearCart()
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