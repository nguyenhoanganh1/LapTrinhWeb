using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EL;
using PagedList;
using Model.Dao;

namespace Web.Controllers

{
    public class ProductsController : Controller
    {
        private DataContext db = new DataContext();
        ProductDAO pdao = new ProductDAO();
        // GET: Products
        public ActionResult Index(int? page)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo Product ID mới có thể phân trang.
            var pro_page = (from l in db.Products
                            select l).OrderBy(x => x.Id);
            /*var pro_page2 = (from l in pro_page select l).OrderBy()*/

            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 9;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các Link được phân trang theo kích thước và số trang.

            return View(pro_page.ToPagedList(pageNumber, pageSize));

        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            // lấy id sản phẩm để hiện chi tiết sp và tằng số lượng người đã xem sp
            // ghi nhận số lần click
           
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.chat = db.Chats.Where(x => x.GroupName == id.ToString()).ToList();

            Product product = db.Products.Find(id);
           
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }



        public ActionResult FindByCategory(int id)
        {
            /* //1. khởi tạo 1 ds  tên là list( List<Product> list)
             //2. vào Sản phẩm tìm kiếm: nếu có cùng id thì bỏ vào list
             var list = db.Products.Where(x => x.CategoryId == id).ToList(); /*//*/ ToList : chuyển đổi tập hợp thành danh sách*/
            var list = pdao.FindByCategory(id);
            return View(list);


        }



        public ActionResult FindBySpecials(int id)
        {
            List<Product> list_special;
            switch (id)
            {
                case 0:
                    list_special = pdao.FindBySpecials();
                

                    break;

                case 1:
                    list_special = pdao.FindByMostView();
                    
                    break;
                case 2:
                    list_special = pdao.FindBySaleOff();
                   
                    break;
                case 3:
                    list_special = pdao.FindByLatest();
                  
                    break;
                case 4:
                    list_special = pdao.FindByBestSeller();
                  
                    break;
                default:
                    list_special = pdao.FindAll();
                    break;


            }
            return View(list_special);


        }



        public ActionResult FindByKeywords(String keywords)
        {
            //1. khởi tạo 1 ds  tên là list( List<Product> list)
            //2. vào Sản phẩm tìm kiếm: nếu có cùng id thì bỏ vào list
            /*var list = db.Products.Where(x => x.Name.Contains(keywords) 
                                         || x.Category.Name.Contains(keywords)
                                         ||x.Category.NameVN.Contains(keywords) ).ToList();*/
            // Contains(keywords) : chưa phần tử

            // ToList : chuyển đổi tập hợp thành danh sách
            /* ViewBag.keyword = keywords;
             return View(list);*/
            // BÊN TRÊN ĐÚNG RỒI NHƯNG VIẾT LẠI THEO DAO CHO GỌN
            List<Product> list = pdao.FindByKeywords(keywords);

            return View(list);
            //return RedirectToAction("Product2_User","list2", new { id = 1001}); // (tên action, tham số)
            // return RedirectToAction("list2", new RouteValueDictionary( new { Controller="Product2_User", Action="list2", new { List < Product > list2 = list } }));
            // return RedirectToAction("list2", list);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
