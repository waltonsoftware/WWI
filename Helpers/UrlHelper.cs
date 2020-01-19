using System;
using System.Collections.Generic;

namespace WWI.Helpers
{
	using System.Linq.Expressions;
	using System.Web.Mvc;
	using System.Web.Mvc.Html;

	/// <summary>
	/// Custom Url Extension Methods
	/// </summary>
	public static class UrlHelperExtension
	{

		#region Application Routes
		public static string SiteRoot(this UrlHelper helper)
		{
			return helper.Content("~/");
		}

		public static string Image(this UrlHelper helper, string fileName)
		{
			return helper.Content(string.Format("~/Images/{0}", fileName));
		}

		public static string Stylesheet(this UrlHelper helper, string fileName)
		{
			return helper.Content(string.Format("~/Content/{0}", fileName));
		}

		//public static string ThemeStylesheet(this UrlHelper helper, string fileName)
		//{
		//	string _theme = string.IsNullOrWhiteSpace(ApplicationManager.SiteConfig.site_ThemeName) ? "default" : ApplicationManager.SiteConfig.site_ThemeName;
		//	return helper.Stylesheet(string.Format("/themes/{0}/{1}", _theme, fileName));
		//}

		public static string Script(this UrlHelper helper, string fileName)
		{
			return helper.Content(string.Format("~/Scripts/{0}", fileName));
		}
		#endregion //Application Routes


		#region Application Objects/Pages
		public static string Home(this UrlHelper helper)
		{
			return helper.Action("Index", "Home");
		}
		#endregion //Application Objects/Pages


		#region Action Methods
		public static string PingRemoteServer(this UrlHelper helper)
		{
			return helper.Action("PingRemoteServer", "Admin");
		}

		public static string RegisterClient(this UrlHelper helper)
		{
			return helper.Action("RegisterClient", "Admin");
		}

		public static string SaveSettings(this UrlHelper helper)
		{
			return helper.Action("SaveSettings", "Admin");
		}
		#endregion //Action Methods
	}
}