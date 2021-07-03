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
        private object crypto;

        public ActionResult FogotPassWord()
        {
            return View();
        }
        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode,string emailfor = "VerifyAccount")
        {
            var verifyUrl = "/User/"+emailfor+ "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("drangon165@gmail.com", "Dotnet Awesome");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "Thanh0123"; // Replace with actual password
            string body = "";
            string subject = "";
            if (emailfor == "VerifyAccount")
            {
                subject = "Your account is successfully created!";
                body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
               " successfully created. Please click on the below link to verify your account" +
               " <br/><br/><a href='" + link + "'>" + link + "</a> ";
            }
            else
            {
                if (emailfor == "ResetPassword")
                {
                    subject = " Reset Password";
                    body = "Hi,<br/>br/>We got requet for reset your account password. please click on the below  link to reset your password" +
                        "<br/>br/><a href=" + link + ">Reset Password link</a>";
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
        public ActionResult FogotPassWord(String Email)
        {
            string message = "";
            bool status = false;

            var account = db.Customers.Where(a => a.Email == Email).FirstOrDefault();
            if (account != null)
            {
                string ressetcode = Guid.NewGuid().ToString();
                SendVerificationLinkEmail(account.Email, ressetcode, "ResetPassword");
                account.ResetPasswordCode = ressetcode;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                message = "ResetPassword link has been sent to your email ID";
            }
            else {
                message = "Account not found";
                ViewBag.Message = message;
            }
            return View();
        }
        public ActionResult ResetPassword(String Id)
        {
            var user = db.Customers.Where(a => a.ResetPasswordCode == Id).FirstOrDefault();
            if (user != null)
            {
                ResetPasswordModel model = new ResetPasswordModel();
                model.ResetCode = Id;
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
                var user = db.Customers.Where(a =>a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                if (user != null)
                {
                    user.Password = model.NewPassWord;
                    user.ResetPasswordCode = "";
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    message = "New Pass Word updata successfully";
                }
            }
            else
            {
                message = "Something invalid";

            }
            ViewBag.Message = message;
            return View(model);
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
                    return RedirectToAction("Index", "Home");
                } 
                  
            
            return View();
        }

    }

}