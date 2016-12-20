using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CarApp.AppStart.Unity;
using CarApp.Web.Utility;

namespace CarApp.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = UnityContainerFactory.Create();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityResolver(container);
        }
    }
}
