﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WWI.Models
{
	public class AspNetNewUserModel
	{
		public String Id { get; set; }

		[Display(Name = "User Name")]
		public String UserName { get; set; }

		[Display(Name = "Email")]
		public String Email { get; set; }

		[Display(Name = "Email Confirmed")]
		public Boolean EmailConfirmed { get; set; }

		[Display(Name = "Password")]
		public String Password { get; set; }

		[Display(Name = "Phone Number")]
		public String PhoneNumber { get; set; }

		[Display(Name = "Phone Number Confirmed")]
		public Boolean PhoneNumberConfirmed { get; set; }

		[Display(Name = "Two Factor Enabled")]
		public Boolean TwoFactorEnabled { get; set; }

		[Display(Name = "Lockout End Date UTC")]
		public DateTime LockoutEndDateUtc { get; set; }

		[Display(Name = "Password Hash")]
		public string PasswordHash { get; set; }

		[Display(Name = "Lockout Enabled")]
		public bool LockoutEnabled { get; set; }

		[Display(Name = "Access Failed Count")]
		public Int32 AccessFailedCount { get; set; }

		[Display(Name = "Administrator")]
		public Boolean cbAdministrator { get; set; }

		[Display(Name = "Contractor")]
		public Boolean cbContractor { get; set; }

		[Display(Name = "Executive")]
		public Boolean cbExecutive { get; set; }

		[Display(Name = "Inventory")]
		public Boolean cbInventory { get; set; }

		[Display(Name = "Sales")]
		public Boolean cbSales { get; set; }

		[Display(Name = "Supplier")]
		public Boolean cbSupplier { get; set; }

		[Display(Name = "User")]
		public Boolean cbUser { get; set; }

		[Display(Name = "Vendor")]
		public Boolean cbVendor { get; set; }
	}
}