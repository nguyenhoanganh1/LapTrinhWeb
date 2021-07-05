using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "CTHD",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "OrderDetail", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Forgot PassWord",
                url: "ForgotPassWord",
                defaults: new { controller = "User", action = "ForgotPassWord", id = UrlParameter.Optional }
            );
            /* routes.MapRoute(
                 name: "DangKy",
                 url: "chi-tiet-DangKY",
                 defaults: new { controller = "User", action = "DangKy", id = UrlParameter.Optional }
             );*/

           /*routes.MapRoute(
                name: "DangKy",
                url: "chi-tiet-DangKY",
                defaults: new { controller = "User", action = "DangKy", id = UrlParameter.Optional }
            );*/
            routes.MapRoute(
              name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
            routes.MapRoute(
              name: "Loginn",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional }
                );


        }
    }
}