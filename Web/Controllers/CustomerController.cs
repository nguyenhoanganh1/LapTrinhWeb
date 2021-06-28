using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using Model.EL;
using System.Security.Cryptography;
using System.Text;

namespace Web.Controllers
{
    public class CustomerController : Controller
    {
        DataContext db = new DataContext();
        // public ActionResult Index()
        //  {
        //      return View(db.Customers.ToList());
        //  }
        
        // GET: Home
        public ActionResult Index()
        {
            if (Session["Id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //GET: Register

        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var check = db.Customers.FirstOrDefault(s => s.Email == customer.Email);
                if (check == null)
                {
                    customer.Password = GetMD5(customer.Password);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            return View();
         }

            public ActionResult Login()
             {
            return View();
             }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string Email, string Password)
        {
            if (ModelState.IsValid)
            {
                var pass = GetMD5(Password);
                var data = db.Customers.Where(s => s.Email.Equals(Email) && s.Password.Equals(pass)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                  //  Session["FullName"] = data.FirstOrDefault().Fullname;
                    Session["Email"] = data.FirstOrDefault().Email.ToString();
                    Session["Password"] = data.FirstOrDefault().Password;
                  //  Session["ID"] = data.FirstOrDefault().Photo;
                  //  Session["ID"] = data.FirstOrDefault().Admin;
                  //  Session["ID"] = data.FirstOrDefault().Activated;
                    
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Email and password is not correct");
                    return View();
                }
            }
            return View();
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }



        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

    }

}


   
