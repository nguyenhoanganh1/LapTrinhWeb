using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class ShoppingCartController : Controller
    {

        private const string CartSession = "CartSession";

        // GET: ShoppingCart
        public ActionResult Index()
        {
            var session = Session[CartSession];
            var list = new List<CartItem>();
            if (session != null)
            {
                list = (List<CartItem>)session;
            }
            return View(list);
        }

        public ActionResult AddItem(long productId, int quantity, double price, double disc, string name)
        {
            var session = Session[CartSession];
            if (session != null)
            {
                var list = (List<CartItem>)session;
                if (list.Exists(x => x.ProductId.Equals(productId)))
                {
                    foreach (var item in list)
                    {
                        if (item.ProductId == productId)
                        {
                            item.Qty += quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.ProductId = productId;
                    item.Qty = 1;
                    item.name = name;
                    item.Price = price;
                    item.Disc = item.Disc;
                    list.Add(item);
                }
            }
            else
            {
                // tạo item
                var item = new CartItem();
                item.ProductId = productId;
                item.Qty = 1;
                item.Price = price;
                item.name = name;
                item.Disc = item.Disc;
                var list = new List<CartItem>();

                // lưu vào session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }

        public ActionResult UpdateItem(long productId, int quantity)
        {
            return View();
        }
    }
}