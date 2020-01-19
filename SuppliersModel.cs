using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WWI.Models
{
	public class SuppliersModel
	{
		[Display(Name = "ID")]
		public Int32 SupplierID { get; set; }

		[Display(Name = "Supplier")]
		public String SupplierName { get; set; }

		public Int32 SupplierCategoryID { get; set; }

		public Int32 PrimaryContactPersonID { get; set; }

		public Int32 AlternateContactPersonID { get; set; }

		public Int32 DeliveryMethodID { get; set; }

		public Int32 DeliveryCityID { get; set; }

		public Int32 PostalCityID { get; set; }

		public String SupplierReference { get; set; }

		public String BankAccountName { get; set; }

		public String BankAccountBranch { get; set; }

		public String BankAccountCode { get; set; }

		public String BankAccountNumber { get; set; }

		public String BankInternationalCode { get; set; }

		public Int32 PaymentDays { get; set; }

		public String InternalComments { get; set; }

		public String PhoneNumber { get; set; }

		public String FaxNumber { get; set; }

		public String WebsiteURL { get; set; }

		public String DeliveryAddressLine1 { get; set; }

		public String DeliveryAddressLine2 { get; set; }

		public String DeliveryPostalCode { get; set; }

		public String DeliveryLocation { get; set; }

		public String PostalAddressLine1 { get; set; }

		public String PostalAddressLine2 { get; set; }

		public String PostalPostalCode { get; set; }

		public Int32 LastEditedBy { get; set; }

		public DateTime ValidFrom { get; set; }

		public DateTime ValidTo { get; set; }
	}
}