using Model.EL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class OrderController : Controller
    {
        DataContext db = new DataContext();
        // GET: Orderdetail
        [Route("OrderPurchase")]
        public ActionResult Purchase()
        {
            double amount = 0;
            var cart = (List<CartItem>)Session["cart"];

            Order order = new Order();

            // order.Customer =  cho user vào session

            foreach (var ca in cart.ToList())
            {
                amount += (ca.product.UnitPrice * ca.quantity * (1 - ca.product.Discount));

                OrderDetail orderDetail = new OrderDetail();
                orderDetail.Product = ca.product;
                orderDetail.Order = order;
                orderDetail.Discount = ca.product.Discount;
                orderDetail.Quantity = ca.quantity;
                orderDetail.UnitPrice = ca.product.UnitPrice;
                db.OrderDetails.Add(orderDetail);
            }
            order.Amount = amount;
            order.OrderDate = new DateTime();
            db.Orders.Add(order);

            db.SaveChanges();
            return View();
        }

    }
}