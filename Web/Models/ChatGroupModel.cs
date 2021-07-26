using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ChatGroupModel
    {
        public string UserName { get; set; }
        public string GroupName { get; set; }
        public string Message { get; set; }

        public string Time
        {
            get { return DateTime.Now.ToString("dd/MM/yyyy HH:mm"); }
        }
    }
}