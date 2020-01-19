using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WWI.Models
{
	public class StockItemsModel
	{
		public Int32 StockItemID { get; set; }

		[Display(Name = "Name")]
		public String StockItemName { get; set; }

		public Int32 SupplierID { get; set; }

		public Int32 ColorID { get; set; }

		public Int32 UnitPackageID { get; set; }

		public Int32 OuterPackageID { get; set; }

		public String Brand { get; set; }

		public String Size { get; set; }

		public Int32 LeadTimeDays { get; set; }

		public Int32 QuantityPerOuter { get; set; }

		public Boolean IsChillerStock { get; set; }

		public String Barcode { get; set; }

		public Decimal TaxRate { get; set; }

		public Decimal UnitPrice { get; set; }

		public Decimal RecommendedRetailPrice { get; set; }

		public Decimal TypicalWeightPerUnit { get; set; }

		public String MarketingComments { get; set; }

		public String InternalComments { get; set; }

		public Byte[] Photo { get; set; }

		public String CustomFields { get; set; }

		public String Tags { get; set; }

		public String SearchDetails { get; set; }

		public Int32 LastEditedBy { get; set; }

		public DateTime ValidFrom { get; set; }

		public DateTime ValidTo { get; set; }
	}
}