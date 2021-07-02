using Model.Dao;
using Model.EL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Login(FormCollection userlog)
        {
            string Email = userlog["Email"].ToString();
            string password = userlog["Password"].ToString();
            var islogin = db.Customers.SingleOrDefault(x => x.Email.Equals(Email) && x.Password.Equals(password));

            if (islogin != null)
            {
                if (Email == "Admin@gmail.com")
                {
                    Session["Customer"] = islogin;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Session["Customer"] = islogin;
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.Fail = "Đăng nhập thất bại";
                return View("Login");
            }

        }
        public ActionResult Dangky()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Dangky(CustomerModel model)
        {
            var dao = new CustomerDAO();

                if (dao.CheckUserName(model.Id))
                {
                    ModelState.AddModelError("Id", "Tên Đăng Nhập đã Tồn Tại");

                }
                else if (dao.Checkemail(model.Email))
                {
                    ModelState.AddModelError("Email", "Email đã Tồn Tại");
                }
                else
                {

                    var user = new Customer();
                    user.Id = model.Id;
                    user.Password = model.Password;
                    user.Fullname = model.Fullname;
                    user.Photo = "user.png";
                    user.Email = model.Email;
                    user.Activated = true;
                    user.Admin = false;
                    dao.InsertCustomer(user);
                dao.Save();
                    return RedirectToAction("Index", "Home");
                } 
                  
            
            return View();
        }

    }

}