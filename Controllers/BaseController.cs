using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WWI.Controllers
{
    public class BaseController : Controller
    {
		protected override void Initialize(RequestContext requestContext)
		{
			base.Initialize(requestContext);
		}

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			// Displays in footer.
			ViewBag.versionInfo = GetProductVersionInfo();
			ViewBag.versionCopyright = GetLegalCopyright();
			ViewBag.productName = GetProductName();

			base.OnActionExecuting(filterContext);
		}

		protected override void OnException(ExceptionContext exceptionContext)
		{
			if (!exceptionContext.ExceptionHandled)
			{
				string controllerName = (string)exceptionContext.RouteData.Values["controller"];
				string actionName = (string)exceptionContext.RouteData.Values["action"];

				var model = new HandleErrorInfo(exceptionContext.Exception, controllerName, actionName);

				exceptionContext.Result = new ViewResult
				{
					ViewName = "~/Views/Shared/Error.cshtml",
					ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
					TempData = exceptionContext.Controller.TempData
				};

				exceptionContext.ExceptionHandled = true;
			}
		}

		//protected bool IsUserAdmin()
		//{
		//	bool ret = false;

		//	var identity = (ClaimsIdentity)User.Identity;
		//	IEnumerable<Claim> claims = identity.Claims;
		//	foreach (Claim claim in claims)
		//	{
		//		if (claim.Type == ClaimTypes.Role && claim.Value == "admin")
		//			ret = true;
		//	}

		//	return ret;
		//}

		private string GetProductVersionInfo()
		{
			return Getfvi().ProductVersion;
		}

		private string GetLegalCopyright()
		{
			return Getfvi().LegalCopyright;
		}

		private string GetProductName()
		{
			return Getfvi().ProductName;
		}

		private int GetFileBuild()
		{
			return Getfvi().FileBuildPart;
		}

		private FileVersionInfo Getfvi()
		{
			System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
			FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
			return fvi;
		}

		protected string GetConnectionString()
		{
			return ConfigurationManager.ConnectionStrings["WWIConnection"].ConnectionString;
		}
	}
}