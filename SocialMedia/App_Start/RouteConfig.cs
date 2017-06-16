using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SocialMedia
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Account", action = "Index"}
            );

            routes.MapRoute(
                name: "Login",
                url: "Account/Login",
                defaults: new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
                name: "CreateAccount",
                url: "Account/CreateAccount",
                defaults: new { controller = "Account", action = "CreateAccount" }
            );

            routes.MapRoute(
                name: "Dashboard",
                url: "{username}",
                defaults: new { controller = "Account", action = "Dashboard" }
            );

            routes.MapRoute(
               name: "SearchUsers",
               url: "Account/SearchUsers",
               defaults: new { controller = "Account", action = "SearchUsers" }
           );

            routes.MapRoute(
                name: "Logout",
                url: "Account/Logout",
                defaults: new { controller = "Account", action = "Logout" }
            );


        }
    }
}
