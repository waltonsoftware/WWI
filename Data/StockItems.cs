/// IListTableSql
/// A strongly typed list of SQL Model Item objects, using generic typed base class.
/// SQL
///
using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using WWI.Log4NetExtension;
using WWI.Models;
using WWI.Data;

namespace WWI.Data
{
	public class StockItems : IListTableBase<StockItemsModel>
	{
		public StockItems()
			: base() { }

		public StockItems(QueryModel queryModel)
			: base(queryModel) { }

		public void Fetch()
		{
			SQLData db = new SQLData();
			List<StockItemsModel> list = new List<StockItemsModel>();
			string sql = "select * from Warehouse.StockItems";

			try
			{
				if (queryModel.search != null)
				{
					sql = string.Format(@"{0} WHERE {1}", sql, queryModel.search);
				}

				if (db.Open(ConfigurationManager.ConnectionStrings["WWI"].ConnectionString))
				{
					string sql2 = string.Format("SELECT COUNT(*) FROM ({0}) AS C1", sql);
					DataTable dt2 = db.Execute(sql2);
					TotalRecords = (int)dt2.Rows[0][0];

					if (queryModel.orders != null)
					{
						StringBuilder sbOrders = new StringBuilder(" ORDER BY ");
						bool isFirst = true;
						foreach (Order order in queryModel.orders)
						{
							if (!isFirst)
								sbOrders.Append(",");
							sbOrders.AppendFormat("{0} {1}", (order.column).ToString(), order.dir);
							isFirst = false;
						}
						sql += sbOrders.ToString();
					}

					if (queryModel.length > 0)
						sql = string.Format(@"{0} OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY", sql, queryModel.start, queryModel.length);

					DataTable dt = db.Execute(sql);
					foreach (DataRow row in dt.Rows)
					{
						StockItemsModel model = new StockItemsModel();

						if (!row["StockItemID"].ToString().Equals(string.Empty))
							model.StockItemID = (Int32)row["StockItemID"];

						if (!row["StockItemName"].ToString().Equals(string.Empty))
							model.StockItemName = row["StockItemName"].ToString();

						if (!row["SupplierID"].ToString().Equals(string.Empty))
							model.SupplierID = (Int32)row["SupplierID"];

						if (!row["ColorID"].ToString().Equals(string.Empty))
							model.ColorID = (Int32)row["ColorID"];

						if (!row["UnitPackageID"].ToString().Equals(string.Empty))
							model.UnitPackageID = (Int32)row["UnitPackageID"];

						if (!row["OuterPackageID"].ToString().Equals(string.Empty))
							model.OuterPackageID = (Int32)row["OuterPackageID"];

						if (!row["Brand"].ToString().Equals(string.Empty))
							model.Brand = row["Brand"].ToString();

						if (!row["Size"].ToString().Equals(string.Empty))
							model.Size = row["Size"].ToString();

						if (!row["LeadTimeDays"].ToString().Equals(string.Empty))
							model.LeadTimeDays = (Int32)row["LeadTimeDays"];

						if (!row["QuantityPerOuter"].ToString().Equals(string.Empty))
							model.QuantityPerOuter = (Int32)row["QuantityPerOuter"];

						if (!row["IsChillerStock"].ToString().Equals(string.Empty))
							model.IsChillerStock = (Boolean)row["IsChillerStock"];

						if (!row["Barcode"].ToString().Equals(string.Empty))
							model.Barcode = row["Barcode"].ToString();

						if (!row["TaxRate"].ToString().Equals(string.Empty))
							model.TaxRate = (Decimal)row["TaxRate"];

						if (!row["UnitPrice"].ToString().Equals(string.Empty))
							model.UnitPrice = (Decimal)row["UnitPrice"];

						if (!row["RecommendedRetailPrice"].ToString().Equals(string.Empty))
							model.RecommendedRetailPrice = (Decimal)row["RecommendedRetailPrice"];

						if (!row["TypicalWeightPerUnit"].ToString().Equals(string.Empty))
							model.TypicalWeightPerUnit = (Decimal)row["TypicalWeightPerUnit"];

						if (!row["MarketingComments"].ToString().Equals(string.Empty))
							model.MarketingComments = row["MarketingComments"].ToString();

						if (!row["InternalComments"].ToString().Equals(string.Empty))
							model.InternalComments = row["InternalComments"].ToString();

						if (!row["Photo"].ToString().Equals(string.Empty))
							model.Photo = (byte[])row["Photo"];

						if (!row["CustomFields"].ToString().Equals(string.Empty))
							model.CustomFields = row["CustomFields"].ToString();

						if (!row["Tags"].ToString().Equals(string.Empty))
							model.Tags = row["Tags"].ToString();

						if (!row["SearchDetails"].ToString().Equals(string.Empty))
							model.SearchDetails = row["SearchDetails"].ToString();

						if (!row["LastEditedBy"].ToString().Equals(string.Empty))
							model.LastEditedBy = (Int32)row["LastEditedBy"];

						if (!row["ValidFrom"].ToString().Equals(string.Empty))
							model.ValidFrom = (DateTime)row["ValidFrom"];

						if (!row["ValidTo"].ToString().Equals(string.Empty))
							model.ValidTo = (DateTime)row["ValidTo"];
						Add(model);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.LogError("StockItems Fetch exception: ", ex);
			}
			finally
			{
				db.Close();
			}
		}
	}
}

//columns: [
//	{ data: "StockItemID" },
//	{ data: "StockItemName" },
//	{ data: "SupplierID" },
//	{ data: "ColorID" },
//	{ data: "UnitPackageID" },
//	{ data: "OuterPackageID" },
//	{ data: "Brand" },
//	{ data: "Size" },
//	{ data: "LeadTimeDays" },
//	{ data: "QuantityPerOuter" },
//	{ data: "IsChillerStock" },
//	{ data: "Barcode" },
//	{ data: "TaxRate" },
//	{ data: "UnitPrice" },
//	{ data: "RecommendedRetailPrice" },
//	{ data: "TypicalWeightPerUnit" },
//	{ data: "MarketingComments" },
//	{ data: "InternalComments" },
//	{ data: "Photo" },
//	{ data: "CustomFields" },
//	{ data: "Tags" },
//	{ data: "SearchDetails" },
//	{ data: "LastEditedBy" },
//	{ data: "ValidFrom" },
//	{ data: "ValidTo" },
//]


//<table id="tblData" class="dataTable">
//	<thead>
//		<tr>
//			<th style="width: 100px;">StockItemID</th>
//			<th style="width: 100px;">StockItemName</th>
//			<th style="width: 100px;">SupplierID</th>
//			<th style="width: 100px;">ColorID</th>
//			<th style="width: 100px;">UnitPackageID</th>
//			<th style="width: 100px;">OuterPackageID</th>
//			<th style="width: 100px;">Brand</th>
//			<th style="width: 100px;">Size</th>
//			<th style="width: 100px;">LeadTimeDays</th>
//			<th style="width: 100px;">QuantityPerOuter</th>
//			<th style="width: 100px;">IsChillerStock</th>
//			<th style="width: 100px;">Barcode</th>
//			<th style="width: 100px;">TaxRate</th>
//			<th style="width: 100px;">UnitPrice</th>
//			<th style="width: 100px;">RecommendedRetailPrice</th>
//			<th style="width: 100px;">TypicalWeightPerUnit</th>
//			<th style="width: 100px;">MarketingComments</th>
//			<th style="width: 100px;">InternalComments</th>
//			<th style="width: 100px;">Photo</th>
//			<th style="width: 100px;">CustomFields</th>
//			<th style="width: 100px;">Tags</th>
//			<th style="width: 100px;">SearchDetails</th>
//			<th style="width: 100px;">LastEditedBy</th>
//			<th style="width: 100px;">ValidFrom</th>
//			<th style="width: 100px;">ValidTo</th>
//		</tr>
//	</thead>
//</table>


