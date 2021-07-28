using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EL;
using Model.Dao;
using PagedList; // chỉ sài PagedList  not PagedList.Mvc;
using System.IO;

namespace Web.Areas.Admin.Controllers
{
    public class ProductsAController : Controller
    {
        private DataContext db = new DataContext();
        private ProductDAO pdao = new ProductDAO();
        // GET: Admin/ProductsA
        public ActionResult Index( int ?page)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo Product ID mới có thể phân trang.
            var pro_page = (from l in db.Products
                            select l).OrderBy(x => x.Id);

            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 9;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các Link được phân trang theo kích thước và số trang.

            return View(pro_page.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/ProductsA/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            

            return View(product);
        }

        // GET: Admin/ProductsA/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "NameVN");
            return View();
        }

        // POST: Admin/ProductsA/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Image,UnitPrice,Discount,Quantity,ProductDate,Special,Latest,ClickCount,CategoryId,Description")] Product product, HttpPostedFileBase  file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/images/items"), _FileName);


                    product.Image = _FileName;
                    file.SaveAs(_path);

                }
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }



            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "NameVN", product.CategoryId);
            return View(product);
        }

        // GET: Admin/ProductsA/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "NameVN", product.CategoryId);
            return View(product);
        }

        // POST: Admin/ProductsA/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Image,UnitPrice,Discount,Quantity,ProductDate,Special,Latest,ClickCount,CategoryId,Description")] Product product, HttpPostedFileBase file )
        {
            if (file != null)
            {
                string _Filename = Path.GetFileName(file.FileName);
                string _path = Path.Combine(Server.MapPath("~/Content/images/items"), _Filename);
                product.Image = _Filename;
                file.SaveAs(_path); // lưu file

            }
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");




            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "NameVN", product.CategoryId);
            return View(product);
        }

        // GET: Admin/ProductsA/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/ProductsA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult FindByCategory(int id)
        {
            var list = pdao.FindByCategory(id);
            return View(list);

        }
        public ActionResult FindBySpecials(int id, int? page)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo Product ID mới có thể phân trang.
            /*var pro_page = (from l in db.Products
                            select l).OrderBy(x => x.Id);*/

            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 9;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

           
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
            var pro_page = (from l in list_special
                            select l).OrderBy(x => x.Id);
            // 5. Trả về các Link được phân trang theo kích thước và số trang.

            return View(pro_page.ToPagedList(pageNumber, pageSize));

          


        }



        public ActionResult FindByKeywords(String keywords)
        {
           
            List<Product> list = pdao.FindByKeywords(keywords);

            return View(list);
            
        }
    }
}
