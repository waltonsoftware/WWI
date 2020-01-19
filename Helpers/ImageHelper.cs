using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WWI.Helpers
{
	public static class ImageHelper
	{
		public static MvcHtmlString Image(this HtmlHelper htmlHelper, 
																		string src, string alt, int width, int height)
		{
			var imageTag = new TagBuilder("image");
			imageTag.MergeAttribute("src", src);
			imageTag.MergeAttribute("alt", alt);
			imageTag.MergeAttribute("width", width.ToString());
			imageTag.MergeAttribute("height", height.ToString());

			return MvcHtmlString.Create(imageTag.ToString(TagRenderMode.SelfClosing));
		}

		public static MvcHtmlString ImageLink(this HtmlHelper htmlHelper, string id, string methodLink, string src, string alt, int width, int height)
		{
			var anchor = new TagBuilder("a");
			anchor.MergeAttributes(new Dictionary<string, object>()
			{
				{ "id", id },
				{ "href", "javascript:" + methodLink },
				{ "alt", alt }
			});

			var imageTag = new TagBuilder("img");
			imageTag.MergeAttributes(new Dictionary<string, object>
			{
				{ "src", src },
				{ "width", width.ToString() },
				{ "height", height.ToString() }
			});

			anchor.InnerHtml = imageTag.ToString(TagRenderMode.SelfClosing);
			return MvcHtmlString.Create(anchor.ToString());
		}
	}
}