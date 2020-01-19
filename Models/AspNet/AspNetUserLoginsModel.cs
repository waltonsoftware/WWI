using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WWI.Models
{
	public class AspNetUserLoginsModel
	{
		[Display(Name = "Login Provider")]
		public string LoginProvider { get; set; }

		[Display(Name = "Provider Key")]
		public string ProviderKey { get; set; }

		public string UserId { get; set; }
	}
}