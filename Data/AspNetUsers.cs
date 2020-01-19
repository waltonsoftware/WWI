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
	public class AspNetUsers : IListTableBase<AspNetUsersModel>
	{
		public AspNetUsers()
			: base() { }

		public AspNetUsers(QueryModel queryModel)
			: base(queryModel) { }

		public void Fetch()
		{
			SQLData db = new SQLData();
			List<AspNetUsersModel> list = new List<AspNetUsersModel>();
			string sql = "SELECT * FROM dbo.AspNetUsers";

			try
			{
				if (queryModel == null)
				{
					queryModel = new QueryModel();
				}

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
						AspNetUsersModel model = new AspNetUsersModel();

						if (!row["Id"].ToString().Equals(string.Empty))
							model.Id = row["Id"].ToString();

						if (!row["Email"].ToString().Equals(string.Empty))
							model.Email = row["Email"].ToString();

						if (!row["EmailConfirmed"].ToString().Equals(string.Empty))
							model.EmailConfirmed = (Boolean)row["EmailConfirmed"];

						if (!row["PasswordHash"].ToString().Equals(string.Empty))
							model.PasswordHash = row["PasswordHash"].ToString();

						if (!row["SecurityStamp"].ToString().Equals(string.Empty))
							model.SecurityStamp = row["SecurityStamp"].ToString();

						if (!row["PhoneNumber"].ToString().Equals(string.Empty))
							model.PhoneNumber = row["PhoneNumber"].ToString();

						if (!row["PhoneNumberConfirmed"].ToString().Equals(string.Empty))
							model.PhoneNumberConfirmed = (Boolean)row["PhoneNumberConfirmed"];

						if (!row["TwoFactorEnabled"].ToString().Equals(string.Empty))
							model.TwoFactorEnabled = (Boolean)row["TwoFactorEnabled"];

						if (!row["LockoutEndDateUtc"].ToString().Equals(string.Empty))
							model.LockoutEndDateUtc = (DateTime)row["LockoutEndDateUtc"];

						if (!row["LockoutEnabled"].ToString().Equals(string.Empty))
							model.LockoutEnabled = (Boolean)row["LockoutEnabled"];

						if (!row["AccessFailedCount"].ToString().Equals(string.Empty))
							model.AccessFailedCount = (Int32)row["AccessFailedCount"];

						if (!row["UserName"].ToString().Equals(string.Empty))
							model.UserName = row["UserName"].ToString();

						// Get Roles
						AspNetUserRoles dbRoles = new AspNetUserRoles();
						dbRoles.queryModel = new QueryModel("UserId=@UserId");
						dbRoles.queryModel.Parameters.Add(new SqlParameter("@UserId", model.Id));
						dbRoles.Fetch();
						model.Roles = dbRoles.Items;

						Add(model);
					}

					db.Close();
				}
			}
			catch (Exception ex)
			{
				Logger.LogError("AspNetUsers Fetch exception: ", ex);
			}
		}
	}
}