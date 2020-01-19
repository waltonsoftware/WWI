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
	public class IListPeopleModel : IListTableBase<PeopleModel>
	{
		public IListPeopleModel()
			: base() { }

		public IListPeopleModel(QueryModel queryModel)
			: base(queryModel) { }

		public void Fetch()
		{
			SQLData db = new SQLData();
			List<PeopleModel> list = new List<PeopleModel>();
			string sql = "SELECT * FROM Application.People";

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
						PeopleModel model = new PeopleModel();

						if (!row["PersonID"].ToString().Equals(string.Empty))
							model.PersonID = (int)row["PersonID"];

						if (!row["FullName"].ToString().Equals(string.Empty))
							model.FullName = row["FullName"].ToString();

						if (!row["PreferredName"].ToString().Equals(string.Empty))
							model.PreferredName = row["PreferredName"].ToString();

						if (!row["SearchName"].ToString().Equals(string.Empty))
							model.SearchName = row["SearchName"].ToString();

						if (!row["IsPermittedToLogon"].ToString().Equals(string.Empty))
							model.IsPermittedToLogon = (bool)row["IsPermittedToLogon"];

						if (!row["LogonName"].ToString().Equals(string.Empty))
							model.LogonName = row["LogonName"].ToString();

						if (!row["IsExternalLogonProvider"].ToString().Equals(string.Empty))
							model.IsExternalLogonProvider = (bool)row["IsExternalLogonProvider"];

						if (!row["HashedPassword"].ToString().Equals(string.Empty))
							model.HashedPassword = (byte[])row["HashedPassword"];

						if (!row["IsSystemUser"].ToString().Equals(string.Empty))
							model.IsSystemUser = (bool)row["IsSystemUser"];

						if (!row["IsEmployee"].ToString().Equals(string.Empty))
							model.IsEmployee = (bool)row["IsEmployee"];

						if (!row["IsSalesperson"].ToString().Equals(string.Empty))
							model.IsSalesperson = (bool)row["IsSalesperson"];

						if (!row["UserPreferences"].ToString().Equals(string.Empty))
							model.UserPreferences = row["UserPreferences"].ToString();

						if (!row["PhoneNumber"].ToString().Equals(string.Empty))
							model.PhoneNumber = row["PhoneNumber"].ToString();

						if (!row["FaxNumber"].ToString().Equals(string.Empty))
							model.FaxNumber = row["FaxNumber"].ToString();

						if (!row["EmailAddress"].ToString().Equals(string.Empty))
							model.EmailAddress = row["EmailAddress"].ToString();

						if (!row["Photo"].ToString().Equals(string.Empty))
							model.Photo = (byte[])row["Photo"];

						if (!row["CustomFields"].ToString().Equals(string.Empty))
							model.CustomFields = row["CustomFields"].ToString();

						if (!row["OtherLanguages"].ToString().Equals(string.Empty))
							model.OtherLanguages = row["OtherLanguages"].ToString();

						if (!row["LastEditedBy"].ToString().Equals(string.Empty))
							model.LastEditedBy = (int)row["LastEditedBy"];

						if (!row["ValidFrom"].ToString().Equals(string.Empty))
							model.ValidFrom = (DateTime)row["ValidFrom"];

						if (!row["ValidTo"].ToString().Equals(string.Empty))
							model.ValidTo = (DateTime)row["ValidTo"];

						Add(model);
					}

					db.Close();
				}
			}
			catch (Exception ex)
			{
				Logger.LogError("Fetch exception: ", ex);
			}
		}
	}
}