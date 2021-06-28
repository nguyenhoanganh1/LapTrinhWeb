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
                    return RedirectToAction("Index", "Home");
                } 
                  
            
            return View();
        }

    }

}