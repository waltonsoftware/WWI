using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Web.Routing;
using System.ComponentModel.DataAnnotations;

namespace WWI.Helpers
{
	using System.Linq.Expressions;
	using System.Web.Mvc;
	using System.Web.Mvc.Html;

	/// <summary>
	/// Custom Html Extention Helper Methods
	/// </summary>
	public static class HtmlHelperExtension
	{
		public static MvcHtmlString DescriptionFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
		{
			var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
			var description = metadata.Description;

			RouteValueDictionary anonymousObjectToHtmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

			TagBuilder tagBuilder = new TagBuilder("span");
			tagBuilder.MergeAttributes<string, object>(anonymousObjectToHtmlAttributes);
			tagBuilder.SetInnerText(description);

			return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
		}

		public static MvcHtmlString DisplayTheNameFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression)
		{
			var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
			var description = metadata.DisplayName;

			//RouteValueDictionary anonymousObjectToHtmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

			TagBuilder tagBuilder = new TagBuilder("span");
			//tagBuilder.MergeAttributes<string, object>(anonymousObjectToHtmlAttributes);
			tagBuilder.SetInnerText(description);

			return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
		}

		public static MvcHtmlString LogonLink(this HtmlHelper helper)
		{
			return helper.ActionLink("Log On", "LogOn", "Security");
		}

		public static MvcHtmlString LogoffLink(this HtmlHelper helper)
		{
			return helper.ActionLink("Log Off", "LogOff", "Security");
		}

		public static MvcHtmlString RegisterLink(this HtmlHelper helper)
		{
			return helper.ActionLink("Register", "Register", "Security");
		}

		public static MvcHtmlString ContractPayment(this HtmlHelper helper)
		{
			return helper.ActionLink("Contract Payment", "Index", "PayBill");
		}

		public static string Images(this HtmlHelper htmlHelper, string id, string alt)
		{
			var urlHelper = ((Controller)htmlHelper.ViewContext.Controller).Url;
			var photoUrl = urlHelper.Action("GetPhoto", "Clinical", new { imageId = id });
			var img = new TagBuilder("img");
			img.MergeAttribute("src", photoUrl);
			img.MergeAttribute("alt", alt);
			return img.ToString(TagRenderMode.SelfClosing);
		}

		public static HtmlTable BuildTable<T>(List<T> Data)
		{
			HtmlTable ht = new HtmlTable();
			//Get the columns
			HtmlTableRow htColumnsRow = new HtmlTableRow();
			typeof(T).GetProperties().Select(prop =>
			{
				HtmlTableCell htCell = new HtmlTableCell();
				htCell.InnerText = prop.Name;
				return htCell;
			}).ToList().ForEach(cell => htColumnsRow.Cells.Add(cell));
			ht.Rows.Add(htColumnsRow);
			//Get the remaining rows
			Data.ForEach(delegate (T obj)
			{
				HtmlTableRow htRow = new HtmlTableRow();
				obj.GetType().GetProperties().ToList().ForEach(delegate (PropertyInfo prop)
				{
					HtmlTableCell htCell = new HtmlTableCell();
					htCell.InnerText = prop.GetValue(obj, null).ToString();
					htRow.Cells.Add(htCell);
				});
				ht.Rows.Add(htRow);
			});
			return ht;
		}

		public static MvcHtmlString ValueLabel<TModel>(this HtmlHelper<TModel> htmlHelper, string fieldName)
		{
			var label = new TagBuilder("span");
			label.MergeAttribute("id", "lbl" + fieldName);
			TModel model = (TModel)htmlHelper.ViewData.Model;
			if (model != null)
				label.SetInnerText(model.GetType().GetProperty(fieldName).GetValue(model).ToString());
				//label.SetInnerText(model.GetType().GetMember(fieldName).GetValue(0).ToString());
			return MvcHtmlString.Create(label.ToString());
		}

		public static MvcHtmlString DisplayNameLabel<TModel>(this HtmlHelper<TModel> htmlHelper, string fieldName)
		{
			var label = new TagBuilder("span");
			label.MergeAttribute("id", "lbl" + fieldName);
			TModel model = (TModel)htmlHelper.ViewData.Model;
			string str = string.Empty;
			if (model != null)
			{
				MemberInfo mi = ((System.Reflection.MemberInfo)(model.GetType().GetMember(fieldName).GetValue(0)));
				if (mi != null)
				{
					Attribute[] di = mi.GetCustomAttributes(typeof(DisplayAttribute)).ToArray();
					if (di != null && di.Length > 0)
						str = ((DisplayAttribute)di[0]).Name;
				}
			}
			label.SetInnerText(str);
			return MvcHtmlString.Create(label.ToString());
		}

		/// <summary>
		/// Creates a span tag for the property name and a span tag for the property value.
		/// </summary>
		/// <typeparam name="TModel"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="htmlHelper"></param>
		/// <param name="expression"></param>
		/// <param name="autoBreak">Automatically adds a line-break after the value span.</param>
		/// <returns></returns>
		public static MvcHtmlString LabelValueFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, bool autoBreak = true)
		{
			var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
			var description = metadata.DisplayName;

			RouteValueDictionary anonymousObjectToHtmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(new { @style = "font-weight:bold;" });

			TagBuilder tagBuilder = new TagBuilder("span");
			tagBuilder.MergeAttributes<string, object>(anonymousObjectToHtmlAttributes);
			tagBuilder.SetInnerText(description + ":");

			TagBuilder valueBuilder = new TagBuilder("span");
			valueBuilder.SetInnerText(metadata.SimpleDisplayText);

			if (autoBreak)
				return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal) + "&nbsp;" + valueBuilder.ToString(TagRenderMode.Normal) + "<br />");
			else
				return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal) + "&nbsp;" + valueBuilder.ToString(TagRenderMode.Normal));
		}

		public static MvcHtmlString LabelLinkValueFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression,
																		string actionName, string controller, bool autoBreak = true)
		{
			return LabelLinkValueFor(htmlHelper, expression, actionName, controller, null, autoBreak);
		}

		/// <summary>
		/// Creates a span tag for the property name and a link tag for the property value.
		/// </summary>
		/// <typeparam name="TModel"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="htmlHelper"></param>
		/// <param name="expression"></param>
		/// <param name="autoBreak">Automatically adds a line-break after the value span.</param>
		/// <returns></returns>
		public static MvcHtmlString LabelLinkValueFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, 
																		string actionName, string controller, RouteValueDictionary routeValues, bool autoBreak = true)
		{
			var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
			var description = metadata.DisplayName;

			RouteValueDictionary anonymousObjectToHtmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(new { @style = "font-weight:bold;" });

			TagBuilder tagBuilder = new TagBuilder("span");
			tagBuilder.MergeAttributes<string, object>(anonymousObjectToHtmlAttributes);
			tagBuilder.SetInnerText(description + ":");

			MvcHtmlString mvcString = MvcHtmlString.Empty;
			if (routeValues == null)
				mvcString = htmlHelper.ActionLink(metadata.SimpleDisplayText, actionName, controller);
			else
				mvcString = htmlHelper.ActionLink(metadata.SimpleDisplayText, actionName, controller, routeValues , null);

			//TagBuilder valueBuilder = new TagBuilder("span");
			//valueBuilder.SetInnerText(metadata.SimpleDisplayText);

			if (autoBreak)
				return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal) + "&nbsp;" + mvcString.ToString() + "<br />");
			else
				return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal) + "&nbsp;" + mvcString.ToString());
		}

		public static MvcHtmlString ALink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, RouteValueDictionary routeValues)
		{
			return htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, null);
		}
	}
}