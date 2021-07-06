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



        [HttpPost]
        public ActionResult Checkout(OrderModel model)
        {
            double amount = 0;
            var cart = (List<CartItem>)Session["cart"];

            Customer cus = (Customer)Session["User"];

            Order order = new Order();

            order.CustomerId = cus.Id;
            order.Address = model.Address;
            order.Notes = model.Notes;
            OrderDetail orderDetail = new OrderDetail();

            foreach (var ca in cart.ToList())
            {
                amount += (ca.product.UnitPrice * ca.quantity * (1 - ca.product.Discount));
                orderDetail.ProductId = ca.product.Id;
                orderDetail.OrderId = order.Id;
                orderDetail.Discount = ca.product.Discount;
                orderDetail.Quantity = ca.quantity;
                orderDetail.UnitPrice = ca.product.UnitPrice;
            }

            db.OrderDetails.Add(orderDetail);
            order.Amount = amount;
            order.OrderDate = new DateTime();
            db.Orders.Add(order);

            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

    }
}