using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WWI.Log4NetExtension;

namespace WWI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
			LoggerInitializer.Initialize();
			Logger.LogInfo("Site WWI starting");

			AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
