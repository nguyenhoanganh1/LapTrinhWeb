using Model.Dao;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class OrderAController : Controller
    {
        private OrderDAO odao = new OrderDAO();
        public ActionResult Index(int? page)
        {
            var list = odao.GetAllOrders();
            if (page == null) page = 1;

            int pageSize = 9;

            int pageNumber = (page ?? 1);

            return View(list.ToPagedList(pageNumber, pageSize));
        }
    }
}