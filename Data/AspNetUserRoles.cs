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
	public class AspNetUserRoles : IListTableBase<AspNetUserRolesModel>
	{
		public AspNetUserRoles()
			: base() { }

		public AspNetUserRoles(QueryModel queryModel)
			: base(queryModel) { }

		public void Fetch()
		{
			SQLData db = new SQLData();
			List<AspNetUserRolesModel> list = new List<AspNetUserRolesModel>();
			string sql = "SELECT * FROM dbo.AspNetUserRoles";

			try
			{
				// If filtered, add WHERE clause to the sql string.
				if (queryModel.search != null)
				{
					sql = string.Format(@"{0} WHERE {1}", sql, queryModel.search);
				}

				// Open the connection.
				if (db.Open(ConfigurationManager.ConnectionStrings["WWI"].ConnectionString))
				{
					// Get the total record count.
					string sql2 = string.Format("SELECT COUNT(*) FROM ({0}) AS C1", sql);
					DataTable dt2 = db.Execute(sql2, queryModel.Parameters);
					TotalRecords = (int)dt2.Rows[0][0];

					// Build order by clause if specified.
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

					// If pagination values.
					if (queryModel.length > 0)
						sql = string.Format(@"{0} OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY", sql, queryModel.start, queryModel.length);

					// Execute query.
					db.sqlCommand.Parameters.Clear();
					DataTable dt = db.Execute(sql, queryModel.Parameters);

					// Populate model with values/rows.
					foreach (DataRow row in dt.Rows)
					{
						AspNetUserRolesModel model = new AspNetUserRolesModel();

						if (!row["UserId"].ToString().Equals(string.Empty))
							model.UserId = row["UserId"].ToString();

						if (!row["RoleId"].ToString().Equals(string.Empty))
							model.RoleId = row["RoleId"].ToString();

						Add(model);
					}

					db.Close();
				}
			}
			catch (Exception ex)
			{
				Logger.LogError("AspNetUserRoles Fetch exception: ", ex);
			}
		}
	}
}