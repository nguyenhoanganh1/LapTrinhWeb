using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Web.Utils
{
    public static class CookieSecurityProvider
    {
        private static MethodInfo _encode;
        private static MethodInfo _decode;
        // CookieProtection.All enables 'temper proffing' and 'encryption' for cookie  
        private static CookieProtection _cookieProtection = CookieProtection.All;

        //Static constructor to get reference of Encode and Decode methods of Class CookieProtectionHelper  
        //using Reflection.  
        static CookieSecurityProvider()
        {
            Assembly systemWeb = typeof(HttpContext).Assembly;
            Type cookieProtectionHelper = systemWeb.GetType("System.Web.Security.CookieProtectionHelper");


            _encode = cookieProtectionHelper.GetMethod("Encode", BindingFlags.NonPublic | BindingFlags.Static);
            _decode = cookieProtectionHelper.GetMethod("Decode", BindingFlags.NonPublic | BindingFlags.Static);
        }

        public static HttpCookie Encrypt(HttpCookie httpCookie)
        {
            byte[] buffer = Encoding.Default.GetBytes(httpCookie.Value);

            //Referencing the Encode mehod of CookieProtectionHelper class  
            httpCookie.Value = (string)_encode.Invoke(null, new object[] { _cookieProtection, buffer, buffer.Length });
            return httpCookie;
        }

        public static HttpCookie Decrypt(HttpCookie httpCookie)
        {
            //Referencing the Decode mehod of CookieProtectionHelper class  
            byte[] buffer = (byte[])_decode.Invoke(null, new object[] { _cookieProtection, httpCookie.Value });
            httpCookie.Value = Encoding.Default.GetString(buffer, 0, buffer.Length);
            return httpCookie;
        }
    }
}