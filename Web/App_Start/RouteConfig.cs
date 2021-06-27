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
                 name: "Add Cart",
                 url: "them-gio-hang",
                 defaults: new { controller = "ShoppingCart", action = "AddItem", id = UrlParameter.Optional }
             );

            routes.MapRoute(
                name: "CTHD",
                url: "chi-tiet-hoa-don",
                defaults: new { controller = "Orderdetail", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Catagory", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
