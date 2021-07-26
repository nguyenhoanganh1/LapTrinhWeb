using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using Web.Models;

namespace Web.Utils
{
    public class MailUtils
    {
        public void SendMail(MailerModel model)
        {
            MailMessage message = new MailMessage(model.From, model.To);
            message.Subject = model.Subject;
            message.Body = model.Content;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("hoanganh25022@gmail.com", "0362006314");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}