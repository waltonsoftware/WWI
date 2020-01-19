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
	public class Suppliers : IListTableBase<SuppliersModel>
	{
		public Suppliers()
			: base() { }

		public Suppliers(QueryModel queryModel)
			: base(queryModel) { }

		public void Fetch()
		{
			SQLData db = new SQLData();
			List<SuppliersModel> list = new List<SuppliersModel>();
			string sql = "select * from Purchasing.Suppliers";

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
						SuppliersModel model = new SuppliersModel();

						if (!row["SupplierID"].ToString().Equals(string.Empty))
							model.SupplierID = (Int32)row["SupplierID"];

						if (!row["SupplierName"].ToString().Equals(string.Empty))
							model.SupplierName = row["SupplierName"].ToString();

						if (!row["SupplierCategoryID"].ToString().Equals(string.Empty))
							model.SupplierCategoryID = (Int32)row["SupplierCategoryID"];

						if (!row["PrimaryContactPersonID"].ToString().Equals(string.Empty))
							model.PrimaryContactPersonID = (Int32)row["PrimaryContactPersonID"];

						if (!row["AlternateContactPersonID"].ToString().Equals(string.Empty))
							model.AlternateContactPersonID = (Int32)row["AlternateContactPersonID"];

						if (!row["DeliveryMethodID"].ToString().Equals(string.Empty))
							model.DeliveryMethodID = (Int32)row["DeliveryMethodID"];

						if (!row["DeliveryCityID"].ToString().Equals(string.Empty))
							model.DeliveryCityID = (Int32)row["DeliveryCityID"];

						if (!row["PostalCityID"].ToString().Equals(string.Empty))
							model.PostalCityID = (Int32)row["PostalCityID"];

						if (!row["SupplierReference"].ToString().Equals(string.Empty))
							model.SupplierReference = row["SupplierReference"].ToString();

						if (!row["BankAccountName"].ToString().Equals(string.Empty))
							model.BankAccountName = row["BankAccountName"].ToString();

						if (!row["BankAccountBranch"].ToString().Equals(string.Empty))
							model.BankAccountBranch = row["BankAccountBranch"].ToString();

						if (!row["BankAccountCode"].ToString().Equals(string.Empty))
							model.BankAccountCode = row["BankAccountCode"].ToString();

						if (!row["BankAccountNumber"].ToString().Equals(string.Empty))
							model.BankAccountNumber = row["BankAccountNumber"].ToString();

						if (!row["BankInternationalCode"].ToString().Equals(string.Empty))
							model.BankInternationalCode = row["BankInternationalCode"].ToString();

						if (!row["PaymentDays"].ToString().Equals(string.Empty))
							model.PaymentDays = (Int32)row["PaymentDays"];

						if (!row["InternalComments"].ToString().Equals(string.Empty))
							model.InternalComments = row["InternalComments"].ToString();

						if (!row["PhoneNumber"].ToString().Equals(string.Empty))
							model.PhoneNumber = row["PhoneNumber"].ToString();

						if (!row["FaxNumber"].ToString().Equals(string.Empty))
							model.FaxNumber = row["FaxNumber"].ToString();

						if (!row["WebsiteURL"].ToString().Equals(string.Empty))
							model.WebsiteURL = row["WebsiteURL"].ToString();

						if (!row["DeliveryAddressLine1"].ToString().Equals(string.Empty))
							model.DeliveryAddressLine1 = row["DeliveryAddressLine1"].ToString();

						if (!row["DeliveryAddressLine2"].ToString().Equals(string.Empty))
							model.DeliveryAddressLine2 = row["DeliveryAddressLine2"].ToString();

						if (!row["DeliveryPostalCode"].ToString().Equals(string.Empty))
							model.DeliveryPostalCode = row["DeliveryPostalCode"].ToString();

						if (!row["DeliveryLocation"].ToString().Equals(string.Empty))
							model.DeliveryLocation = row["DeliveryLocation"].ToString();

						if (!row["PostalAddressLine1"].ToString().Equals(string.Empty))
							model.PostalAddressLine1 = row["PostalAddressLine1"].ToString();

						if (!row["PostalAddressLine2"].ToString().Equals(string.Empty))
							model.PostalAddressLine2 = row["PostalAddressLine2"].ToString();

						if (!row["PostalPostalCode"].ToString().Equals(string.Empty))
							model.PostalPostalCode = row["PostalPostalCode"].ToString();

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
				Logger.LogError("Suppliers Fetch exception: ", ex);
			}
			finally
			{
				db.Close();
			}
		}
	}

}

//columns: [
//	{ data: "SupplierID" },
//	{ data: "SupplierName" },
//	{ data: "SupplierCategoryID" },
//	{ data: "PrimaryContactPersonID" },
//	{ data: "AlternateContactPersonID" },
//	{ data: "DeliveryMethodID" },
//	{ data: "DeliveryCityID" },
//	{ data: "PostalCityID" },
//	{ data: "SupplierReference" },
//	{ data: "BankAccountName" },
//	{ data: "BankAccountBranch" },
//	{ data: "BankAccountCode" },
//	{ data: "BankAccountNumber" },
//	{ data: "BankInternationalCode" },
//	{ data: "PaymentDays" },
//	{ data: "InternalComments" },
//	{ data: "PhoneNumber" },
//	{ data: "FaxNumber" },
//	{ data: "WebsiteURL" },
//	{ data: "DeliveryAddressLine1" },
//	{ data: "DeliveryAddressLine2" },
//	{ data: "DeliveryPostalCode" },
//	{ data: "DeliveryLocation" },
//	{ data: "PostalAddressLine1" },
//	{ data: "PostalAddressLine2" },
//	{ data: "PostalPostalCode" },
//	{ data: "LastEditedBy" },
//	{ data: "ValidFrom" },
//	{ data: "ValidTo" },
//]


