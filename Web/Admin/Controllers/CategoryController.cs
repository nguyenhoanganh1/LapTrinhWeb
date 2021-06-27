using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Admin.Controllers
{
    public class CategoryController : Controller
    {
        DataContext
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
    }
}