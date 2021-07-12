using Model.EL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ReportController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Report

        public ActionResult Index()
        {
            var product = db.Products.GroupBy(p => p.Category)
                .OrderByDescending(p => p.Sum(c => c.UnitPrice * c.Quantity))
                .Select(p => new
                {
                    Group = p.Key.NameVN,
                    Sum = p.Sum(c => c.UnitPrice * c.Quantity),
                    Count = p.Sum(c => c.Quantity),
                    Min = p.Min(c => c.UnitPrice),
                    Max = p.Max(c => c.UnitPrice),
                    Avg = p.Average(c => c.UnitPrice)
                });
            return Json(new
            {
                message = "success",
                data = product
            }, JsonRequestBehavior.AllowGet);
        }
    }
}