using Model.EL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class OrderdetailController : Controller
    {
        DataContext db = new DataContext();
        // GET: Orderdetail
        public ActionResult Index()
        {
            var orderstail = db.OrderDetails.Include(q => q.Order);
            return View(orderstail.ToList());
        }
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderdetail = db.OrderDetails.Find(id);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            return View(orderdetail);
        }
        public ActionResult Create()
        {
            ViewBag.OrderId = new SelectList(db.OrderDetails, "Id");
            return View();
        }
    }
}