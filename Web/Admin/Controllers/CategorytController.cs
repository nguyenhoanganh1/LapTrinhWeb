using Model.EL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Admin.Controllers
{
    public class CategorytController : Controller
    {
        // GET: Categoryt
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }
    }
}