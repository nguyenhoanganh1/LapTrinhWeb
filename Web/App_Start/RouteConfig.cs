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

           /* routes.MapRoute(
                 name: "Add Cart",
                url: "{controller}/{action}/{id}",
                 defaults: new { controller = "ShoppingCart", action = "AddItem", id = UrlParameter.Optional }
             );

            routes.MapRoute(
                name: "CTHD",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "OrderDetail", action = "Index", id = UrlParameter.Optional }
            );*/

            routes.MapRoute(
              name: "Login",
                url: "{controller}/{action}/{id}",
               defaults: new { controller = "Customer", action = "Index", id = UrlParameter.Optional }
           );
        }
    }
}