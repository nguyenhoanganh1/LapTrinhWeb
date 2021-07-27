using Model.Dao;
using Model.EL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        DataContext db = new DataContext();
        private string message = "";
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




        public ActionResult Logout()
        {
            if (Request.Cookies["link"] != null)
            {
                Response.Cookies["link"].Expires = DateTime.Now.AddDays(-1);
            }
            Session.Remove("User");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]

        public ActionResult FogotPassWord()
        {
            return View();
        }
        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode, string emailfor = "VerifyAccount")
        {
            var verifyUrl = "/User/" + emailfor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("drangon165@gmail.com", "Shop Bán Giầy");// Thay doi tai khoan giu
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "Thanh0123"; // Mat Khau cua tai khoan gmail
            string body = "";
            string subject = "";
            if (emailfor == "VerifyAccount")
            {
                subject = "Your account is successfully created!";
                body = "<br/>Chúng tôi vui mừng thông báo với bạn rằng tài khoản của bạn là" +
               "đăng nhập thành công. Vui lòng nhấp vào liên kết dưới đây để xác minh tài khoản của bạn" +
               " <br/><a href='" + link + "'>" + link + "</a> ";
            }
            else
            {
                if (emailfor == "ResetPassword")
                {
                    subject = " Reset Password";
                    body = "Xin Chào,<br/>Chúng tôi đã được yêu cầu để đặt lại mật khẩu tài khoản của bạn. vui lòng nhấp vào liên kết dưới đây để đặt lại mật khẩu của bạn" +
                        "<br/><a href=" + link + ">Reset Password link</a>";
                }
            }
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }

        [HttpPost]
        public ActionResult FogotPassWord(String Email, string username)
        {
            string message = "";
            bool status = false;

            var account = db.Customers.Where(a => a.Email == Email && a.Id == username).FirstOrDefault();
            if (account != null)
            {
                string ressetcode = Guid.NewGuid().ToString();
                SendVerificationLinkEmail(account.Email, ressetcode, "ResetPassword");
                account.ResetPasswordCode = ressetcode;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                message = "ResetPassword link has been sent to your email ID";
            }
            else
            {
                message = "Không tìm thấy tài khoản";
                ViewBag.Message = message;
            }
            return View();
        }


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
                if (Email == "nghiemn@fpt.edu.vn" || (islogin.Admin == true))
                {
                    Session["User"] = islogin;
                    return RedirectToAction("Index", "Admin/HomeA");
                }
                else
                {
                    string link = Request.Cookies["link"].Value;
                    if (link != null)
                    {
                        Session["User"] = islogin;
                        return Redirect(link);
                    }
                    else
                    {
                        Session["User"] = islogin;
                        return RedirectToAction("Index", "Home");
                    }

                }
            }
            else
            {
                ViewBag.Fail = "Đăng nhập thất bại";
                return View("Login");
            }

        }
        public ActionResult ResetPassword(string id)
        {
            var user = db.Customers.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
            if (user != null)
            {
                ResetPasswordModel model = new ResetPasswordModel();
                model.ResetCode = id;
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {

                var user = db.Customers.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                if (user != null)
                {
                    user.Password = model.NewPassWord;
                    user.ResetPasswordCode = "";
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    message = "New password updated successfully";
                }
            }
            else
            {
                message = "Something invalid";
            }
            ViewBag.Message = message;
            return RedirectToAction("Index", "Home");
        }
    }
}



