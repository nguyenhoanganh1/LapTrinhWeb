using Model.Dao;
using Model.EL;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Admin.Controllers
{
    public class OrderAdminController : Controller
    {
        private OrderDAO odao = new OrderDAO();

        public ActionResult Index(int? page)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo Product ID mới có thể phân trang.
            var list = odao.GetAllOrders();

            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 9;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }


        [HttpPost]
        public ActionResult Update(Order order)
        {

            odao.UpdateStatus(order);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(long id)
        {
            return View(odao.GetOrderById(id));
        }
    }
}