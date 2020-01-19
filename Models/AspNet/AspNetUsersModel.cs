using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WWI.Models
{
	public class AspNetUsersModel
	{
		public String Id { get; set; }

		public String UserName { get; set; }

		public String Email { get; set; }

		[Display(Name = "Email Confirmed")]
		public Boolean EmailConfirmed { get; set; }

		[Display(Name = "Password Hash")]
		public String PasswordHash { get; set; }

		[Display(Name = "Security Stamp")]
		public String SecurityStamp { get; set; }

		[Display(Name = "Phone Number")]
		public String PhoneNumber { get; set; }

		[Display(Name = "Phone Number Confirmed")]
		public Boolean PhoneNumberConfirmed { get; set; }

		[Display(Name = "Two Factor Enabled")]
		public Boolean TwoFactorEnabled { get; set; }

		[Display(Name = "Lockout End Date UTC")]
		public DateTime LockoutEndDateUtc { get; set; }

		[Display(Name = "Lockout Enabled")]
		public Boolean LockoutEnabled { get; set; }

		[Display(Name = "Access Failed Count")]
		public Int32 AccessFailedCount { get; set; }

		public List<AspNetUserRolesModel> Roles { get; set; }
	}
}