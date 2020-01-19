using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WWI.Models
{
	public class SupplierCategoriesModel
	{
		public Int32 SupplierCategoryID { get; set; }

		public String SupplierCategoryName { get; set; }

		public Int32 LastEditedBy { get; set; }

		public DateTime ValidFrom { get; set; }

		public DateTime ValidTo { get; set; }
	}
}