using Model.Dao;
using Model.EL;
using PagedList;
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

        public ActionResult checkout()
        {
            return RedirectToAction("login", "user");
        }

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

        public ActionResult ProductBought(int? page)
        {
            try
            {
                if (page == null) page = 1;

                int pageSize = 10;

                int pageNumber = (page ?? 1);

                OrderDAO dao = new OrderDAO();
                Customer cus = (Customer)Session["User"];

                var list = dao.FindAllOrderById(cus.Id);
                if (list != null)
                {
                    return View(list.ToPagedList(pageNumber, pageSize));
                }
            }
            catch (Exception ex)
            {
                return View();
            }
            return View();

        }

        public ActionResult Detail(int? page)
        {
            if (page == null) page = 1;

            int pageSize = 10;

            int pageNumber = (page ?? 1);
            ProductDAO pdao = new ProductDAO();
            Customer customer = (Customer)Session["User"];

            var list = pdao.GetAllProductByCustomerId(customer.Id);
            if (list != null)
            {
                return View(list.ToPagedList(pageNumber, pageSize));
            }
            return View();
        }

        public ActionResult Delete(long id)
        {
            var order = db.Orders.FirstOrDefault(x => x.Id == id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("productbought", "order");
        }

    }
}