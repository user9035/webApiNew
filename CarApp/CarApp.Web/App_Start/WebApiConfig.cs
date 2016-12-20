using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CarApp.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { controller = "feeds", id = RouteParameter.Optional }
            );
        }
    }
}
