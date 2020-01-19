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
	public class AspNetRoles : IListTableBase<AspNetRolesModel>
	{
		public AspNetRoles()
			: base() { }

		public AspNetRoles(QueryModel queryModel)
			: base(queryModel) { }

		public void Fetch()
		{
			SQLData db = new SQLData();
			List<AspNetRolesModel> list = new List<AspNetRolesModel>();
			string sql = "SELECT * FROM dbo.AspNetRoles";

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
						AspNetRolesModel model = new AspNetRolesModel();

						if (!row["Id"].ToString().Equals(string.Empty))
							model.Id = row["Id"].ToString();

						if (!row["Name"].ToString().Equals(string.Empty))
							model.Name = row["Name"].ToString();

						Add(model);
					}

					db.Close();
				}
			}
			catch (Exception ex)
			{
				Logger.LogError("AspNetRoles Fetch exception: ", ex);
			}
		}
	}
}