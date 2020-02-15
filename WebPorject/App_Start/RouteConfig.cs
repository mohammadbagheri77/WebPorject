using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebPorject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}",
                defaults: new { controller = "ms", action = "Index" }
            );

            routes.MapRoute(
               name: "post",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "ms", action = "post", id = UrlParameter.Optional }
           );

            /////////////////////////////////////////////////////////////////////
            
            routes.MapRoute(
               name: "User",
               url: "{controller}/{action}",
               defaults: new { controller = "log", action = "User" }
           );

            routes.MapRoute(
               name: "hellow",
               url: "{controller}/{action}",
               defaults: new { controller = "log", action = "hellow" }
           );

            routes.MapRoute(
              name: "newpost",
              url: "{controller}/{action}",
              defaults: new { controller = "log", action = "newpost" }
          );
            routes.MapRoute(
              name: "showpost",
              url: "{controller}/{action}",
              defaults: new { controller = "log", action = "showpost" }
          );
        }
    }
}
