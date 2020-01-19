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
	public class SupplierCategories : IListTableBase<SupplierCategoriesModel>
	{
		public SupplierCategories()
			: base() { }

		public SupplierCategories(QueryModel queryModel)
			: base(queryModel) { }

		public void Fetch()
		{
			SQLData db = new SQLData();
			List<SupplierCategoriesModel> list = new List<SupplierCategoriesModel>();
			string sql = "select * from Purchasing.SupplierCategories";

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
						SupplierCategoriesModel model = new SupplierCategoriesModel();

						if (!row["SupplierCategoryID"].ToString().Equals(string.Empty))
							model.SupplierCategoryID = (Int32)row["SupplierCategoryID"];

						if (!row["SupplierCategoryName"].ToString().Equals(string.Empty))
							model.SupplierCategoryName = row["SupplierCategoryName"].ToString();

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
				Logger.LogError("SupplierCategories Fetch exception: ", ex);
			}
			finally
			{
				db.Close();
			}
		}
	}
}

