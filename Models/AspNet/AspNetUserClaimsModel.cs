using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WWI.Models
{
	public class AspNetUserClaimsModel
	{
		public int Id { get; set; }

		public string UserId { get; set; }

		[Display(Name = "Claim Type")]
		public string ClaimType { get; set; }

		[Display(Name = "Claim Value")]
		public string ClaimValue { get; set; }
	}
}