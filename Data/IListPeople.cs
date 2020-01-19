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
	public class IListPeople : IListTableBase<People>
	{
		public IListPeople()
			: base() { }

		public IListPeople(QueryModel queryModel)
			: base(queryModel) { }

		public void Fetch()
		{
			SQLData db = new SQLData();
			List<People> list = new List<People>();
			string sql = "SELECT * FROM Application.People";

			try
			{
				if (queryModel.search != null)
				{
					sql = string.Format(@"{0} WHERE {1}", sql, queryModel.search);
				}

				if (db.Open(ConfigurationManager.ConnectionStrings["Model1"].ConnectionString))
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
						Add(new People(row));
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

	public class People
	{
		[Key]
		public int PersonID { get; set; }

		public string FullName { get; set; }

		public string PreferredName { get; set; }

		public string SearchName { get; set; }

		[Display(Name = "Permitted to Logon")]
		public bool IsPermittedToLogon { get; set; }

		[Display(Name = "Logon Name")]
		public string LogonName { get; set; }

		public bool IsExternalLogonProvider { get; set; }

		public byte[] HashedPassword { get; set; }

		[Display(Name = "System User")]
		public bool IsSystemUser { get; set; }

		[Display(Name = "Employee")]
		public bool IsEmployee { get; set; }

		[Display(Name = "Sales Person")]
		public bool IsSalesperson { get; set; }

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


		public People() { }

		public People(DataRow row)
		{
			try
			{
				Fetch(row);
			}
			catch(Exception ex)
			{
				Logger.LogError("People Constructor exception: ", ex);
			}
		}

		public void Fetch(DataRow row)
		{
			try
			{
				if (!row["PersonID"].ToString().Equals(string.Empty))
					this.PersonID = (int)row["PersonID"];

				if (!row["FullName"].ToString().Equals(string.Empty))
					this.FullName = row["FullName"].ToString();

				if (!row["PreferredName"].ToString().Equals(string.Empty))
					this.PreferredName = row["PreferredName"].ToString();

				if (!row["SearchName"].ToString().Equals(string.Empty))
					this.SearchName = row["SearchName"].ToString();

				if (!row["IsPermittedToLogon"].ToString().Equals(string.Empty))
					this.IsPermittedToLogon = (bool)row["IsPermittedToLogon"];

				if (!row["LogonName"].ToString().Equals(string.Empty))
					this.LogonName = row["LogonName"].ToString();

				if (!row["IsExternalLogonProvider"].ToString().Equals(string.Empty))
					this.IsExternalLogonProvider = (bool)row["IsExternalLogonProvider"];

				if (!row["HashedPassword"].ToString().Equals(string.Empty))
					this.HashedPassword = (byte[])row["HashedPassword"];

				if (!row["IsSystemUser"].ToString().Equals(string.Empty))
					this.IsSystemUser = (bool)row["IsSystemUser"];

				if (!row["IsEmployee"].ToString().Equals(string.Empty))
					this.IsEmployee = (bool)row["IsEmployee"];

				if (!row["IsSalesperson"].ToString().Equals(string.Empty))
					this.IsSalesperson = (bool)row["IsSalesperson"];

				if (!row["UserPreferences"].ToString().Equals(string.Empty))
					this.UserPreferences = row["UserPreferences"].ToString();

				if (!row["PhoneNumber"].ToString().Equals(string.Empty))
					this.PhoneNumber = row["PhoneNumber"].ToString();

				if (!row["FaxNumber"].ToString().Equals(string.Empty))
					this.FaxNumber = row["FaxNumber"].ToString();

				if (!row["EmailAddress"].ToString().Equals(string.Empty))
					this.EmailAddress = row["EmailAddress"].ToString();

				if (!row["Photo"].ToString().Equals(string.Empty))
					this.Photo = (byte[])row["Photo"];

				if (!row["CustomFields"].ToString().Equals(string.Empty))
					this.CustomFields = row["CustomFields"].ToString();

				if (!row["OtherLanguages"].ToString().Equals(string.Empty))
					this.OtherLanguages = row["OtherLanguages"].ToString();

				if (!row["LastEditedBy"].ToString().Equals(string.Empty))
					this.LastEditedBy = (int)row["LastEditedBy"];

				if (!row["ValidFrom"].ToString().Equals(string.Empty))
					this.ValidFrom = (DateTime)row["ValidFrom"];

				if (!row["ValidTo"].ToString().Equals(string.Empty))
					this.ValidTo = (DateTime)row["ValidTo"];
			}
			catch(Exception ex)
			{
				Logger.LogError("People.Fetch exception: ", ex);
			}
		}
	}

}