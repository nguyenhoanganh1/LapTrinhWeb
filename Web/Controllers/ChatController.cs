using Model.EL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class ChatController : Controller
    {
        DataContext db = new DataContext();
        // GET: Chat
        [HttpPost]
        public ActionResult SendMessage(ChatGroupModel model)
        {
            Customer cus = (Customer)Session["user"];
            Chat chat = new Chat();
            if (cus == null)
            {
                
                return RedirectToAction("Login", "User");
            }
            else
            {
                chat.CustomerId = cus.Id;
                chat.Message = model.Message;
                chat.GroupName = model.GroupName;
                chat.DateTime = Convert.ToDateTime(model.Time);
                db.Chats.Add(chat);
                db.SaveChanges();
                return Json(new
                {
                    data = chat
                }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}