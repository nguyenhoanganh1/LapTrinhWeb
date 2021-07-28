using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Model.EL;


namespace Web.Areas.Admin.Controllers
{
    public class DirectoryNotFoundException : System.IO.IOException { };
    public class CustomersAController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Admin/Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Admin/Customers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Admin/Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Password,Fullname,Email,Photo,Activated,Admin,ResetPasswordCode")] Customer customer, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Customer b = new Customer();
                    b.Fullname = customer.Fullname;
                    b.Email = customer.Email;
                    b.Id = customer.Id;
                    if (file.ContentLength > 0)
                    {
                        DateTime today = DateTime.Now;
                        string time = today.Millisecond.ToString();

                        string _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Images"), time + "_" + _FileName);

                        b.Photo = time + "_" + _FileName;
                        file.SaveAs(_path);
                    }
                    b.Activated = customer.Activated;
                    customer.Admin = false;
                    db.Customers.Add(b);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(customer);
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }

        // GET: Admin/Customers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Admin/Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]



        

        public ActionResult Edit([Bind(Include = "Id,Password,Fullname,Email,Photo,Activated,Admin")] Customer customer, HttpPostedFileBase file)
        {
            /*var custo = new CustomerDAO().GetCustomerById(customer.Id);*/
            var custo = db.Customers.FirstOrDefault(p => p.Id == customer.Id);
            if (file != null)
            {

                var location = Server.MapPath(Url.Content("~/Content/images/users"));

                


                if (!string.IsNullOrEmpty(custo.Photo))
                {
                    var existingFile = Path.Combine(location, custo.Photo);
                    if (System.IO.File.Exists(existingFile))
                    {
                        System.IO.File.Delete(existingFile);
                    }
                }
                DateTime today = DateTime.Now;
                string time = today.Millisecond.ToString();
                string _FileName = Path.GetFileName(file.FileName);
                string _path = Path.Combine(location, time + "_" + _FileName);
                custo.Photo = time + "_" + _FileName;
                file.SaveAs(filename: _path);
            }

            custo.Fullname = customer.Fullname;
            custo.Email = customer.Email;
            custo.Password = customer.Password;
            custo.Admin = customer.Admin;
            custo.Activated = customer.Activated;
            custo.ResetPasswordCode = "";
            db.SaveChanges();


            return RedirectToAction("Index","CustomersA"); 

        }

        // GET: Admin/Customers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Admin/Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
    }
}