using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WWI.Models
{
	public class PeopleModel
	{
		[Key]
		public int PersonID { get; set; }

		[Display(Name = "Full Name")]
		public string FullName { get; set; }

		[Display(Name = "Preferred Name")]
		public string PreferredName { get; set; }

		[Display(Name = "Search Name")]
		public string SearchName { get; set; }

		[Display(Name = "Permitted to Logon")]
		public bool IsPermittedToLogon { get; set; }

		[Display(Name = "Logon Name")]
		public string LogonName { get; set; }

		[Display(Name = "External Logon Provider")]
		public bool IsExternalLogonProvider { get; set; }

		public byte[] HashedPassword { get; set; }

		[Display(Name = "System User")]
		public bool IsSystemUser { get; set; }

		[Display(Name = "Employee")]
		public bool IsEmployee { get; set; }

		[Display(Name = "Sales Person")]
		public bool IsSalesperson { get; set; }

		[Display(Name = "User Preferences")]
		public string UserPreferences { get; set; }

		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }

		[Display(Name = "Fax Number")]
		public string FaxNumber { get; set; }

		[Display(Name = "Email Address")]
		public string EmailAddress { get; set; }

		public byte[] Photo { get; set; }

		public string CustomFields { get; set; }

		public string OtherLanguages { get; set; }

		public int LastEditedBy { get; set; }

		public DateTime ValidFrom { get; set; }

		public DateTime ValidTo { get; set; }
	}
}