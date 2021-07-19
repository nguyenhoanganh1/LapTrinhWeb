using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ReportServices
{
    public class Cookie
    {
        HttpRequest request;
        HttpResponse respon;

        public Cookie create (string name , string value, int han)
        {
            HttpCookie cookie = new HttpCookie(name, value);
            cookie.Expires.AddMinutes(han); // thời hạn của cookie theo phút
            


            return null;
        }
        public string getvalue (string name)
        {
            return null;

        }
        public void delete(string name)
        {
            this.create(name, "", 0);

        }
    }
}