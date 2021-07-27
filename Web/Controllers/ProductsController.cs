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
using Web.Models;
using Web.Utils;

namespace Web.Controllers

{
    public class ProductsController : Controller
    {
        MailUtils mailUtils = new MailUtils();

        private DataContext db = new DataContext();
        ProductDAO pdao = new ProductDAO();
        // tạo cái list để bỏ cookie
        public static List<int> yourlist = new List<int>();
        // PHẢI CÓ STATIC // nếu không list sẽ không được lưu lại, 

        // tạo list sp yêu thích
        public static List<string> likelist = new List<string>();



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

            HttpCookie http = new HttpCookie("link"); // Tên Cookie Là link, để sử dụng cookie thì gọi tên là ( link )
            string url = Request.Url.LocalPath; //Lấy Địa chỉ uri trang hiện tại là: /ShoppingCart/ShowCart
            http.Value = WebUtility.HtmlEncode(url);
            http.Expires = DateTime.Today.AddDays(1); // Thời gian Cookie: 1 ngày
            Response.Cookies.Add(http); // thêm vào cookie

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.chat = db.Chats.Where(x => x.GroupName == id.ToString()).ToList();

            Product product = db.Products.Find(id);

            // Thêm id vào list product

            //kiểm tra id có bị trùng hay không nếu trùng thì k thêm vào
            if (!yourlist.Contains(product.Id))
            {
                yourlist.Add(product.Id);

            }



            // tạo 1 list ngăn cách nhau bởi dấu phẩy
            var yourlist_String = String.Join(",", yourlist);

            // tạo cookie  
            // 
            HttpCookie yourlist_cookie = new HttpCookie("YourList", yourlist_String);


            //  tạo thời hạn tồn tại của cookie
            yourlist_cookie.Expires = DateTime.Now.AddMinutes(10);// hạn 10p

            // đẩy cookie tới View Details  của controller;
            Response.Cookies.Add(yourlist_cookie);
            //Đẩy cookie tới trang khác
            //Response.Redirect("_Aside.cshtml");


            if (product == null)
            {
                return HttpNotFound();
            }









            // mỗi lần click vào detail sẽ tăng lượt xem lên
            product.ClickCount += 1;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();

            Console.WriteLine(yourlist_cookie);



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

        public ActionResult postcookie()
        {
            /*  // tạo 1 list đã 

              yourlist.Add(1);
              yourlist.Add(2);
              yourlist.Add(3);
              yourlist.Add(4);
              //yourlist.Add(5);


              //
              var yourlist_String = String.Join(",", yourlist);

              // tạo cookie  
              // 
              HttpCookie yourlist_cookie = new HttpCookie("YourList", yourlist_String);


              //  tạo thời hạn tồn tại của cookie
              yourlist_cookie.Expires = DateTime.Now.AddMinutes(10);// hạn 10p

              // đẩy cookie tới View của controller;
              Response.Cookies.Add(yourlist_cookie);*/

            return View();


        }

       
        public ActionResult SendMail(MailerModel model)
        {
            string link = Request.Url.AbsoluteUri.ToString().Replace("SendMail", "Details");
            model.Subject = "Bạn của bạn gửi cho bạn một sản phẩm";
            model.Content += "<hr/> Mời bạn vào click vào " + link + " để xem chi tiết sản phẩm";
            mailUtils.SendMail(model);
            return Json(new
            {
                data = model
            }, JsonRequestBehavior.AllowGet);
        }


        /*[Route("GetAllLike")]
        [HttpGet]
        public ActionResult GetAllLike()
        {
            var danhsach = Request.Cookies["Like"].Value.Split(',').Select(x => x).ToList();
            *//*var list = new List<Product>();
            danhsach.ForEach(x => {
                list.Add(db.Products.Where(a => a.Id.ToString() == x).FirstOrDefault());
            });*//*

            return Json(new
            {
                data = danhsach

            }, JsonRequestBehavior.AllowGet);
        }*/



       /* public ActionResult Cong(int a, int b)
        {
            return Content((a + b).ToString()); 
        }*/
        [HttpPost]
        public ActionResult LikeProduct(string id)
        {
            // Thêm id vào list  yêu thích
            //kiểm tra id có bị trùng hay không nếu trùng thì k thêm vào
            if (!likelist.Contains(id))
            {
                likelist.Add(id);
            }


            // tạo 1 list ngăn cách nhau bởi dấu phẩy
            var likelist_String = String.Join(",", likelist);

            // tạo cookie  
            // 
            HttpCookie likelist2 = new HttpCookie("Like");

            likelist2.Value = likelist_String;

            //  tạo thời hạn tồn tại của cookie
            likelist2.Expires = DateTime.Now.AddDays(10);// hạn 10 ngay


            // đẩy cookie tới View Details  của controller;
            Response.Cookies.Add(likelist2);
            // Đẩy cookie tới trang khác
            // Response.Redirect("Details.cshtml");
            return Json(new
            {
                data = likelist2
            }, JsonRequestBehavior.AllowGet);


        }
        [Route("GetAllLike")]
        [HttpGet]
        public ActionResult GetAllLike()
        {
            var danhsach = Request.Cookies["Like"].Value.Split(',').Select(x => x).ToList();
            var list = new List<Product>();
            danhsach.ForEach(x =>
            {
                list.Add(db.Products.Where(a => a.Id.ToString() == x).FirstOrDefault());
            });

            List<LikeModel> like = new List<LikeModel>();
            for (int i = 0; i < list.Count(); i++)
            {
                like.Add(new LikeModel()
                {
                    id = list[i].Id.ToString(),
                    image = list[i].Image
                });
            }


            return Json(new
            {
                data = like

            }, JsonRequestBehavior.AllowGet);
        }
    }
}
