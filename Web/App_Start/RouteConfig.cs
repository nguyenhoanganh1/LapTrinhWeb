﻿using System;
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
                url: "chi-tiet-hoa-don",
                defaults: new { controller = "Orderdetail", action = "Index", id = UrlParameter.Optional }
            );
           /* routes.MapRoute(
                name: "DangKy",
                url: "chi-tiet-DangKY",
                defaults: new { controller = "User", action = "DangKy", id = UrlParameter.Optional }
            );*/
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Catagory", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
