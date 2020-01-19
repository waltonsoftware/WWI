using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using WWI.Log4NetExtension;
using WWI.Managers;
using WWI.Models;
using WWI.Data;

namespace WWI.Data
{
	public class WWIDal
	{
		// Users
		public PeopleModel GetPerson(int personId)
		{
			SQLData db = new SQLData();
			string sql = "SELECT * FROM Application.People WHERE PersonId=@PersonId";
			List<SqlParameter> parms = new List<SqlParameter>();
			PeopleModel model = new PeopleModel();

			try
			{
				db.Open(ConfigurationManager.ConnectionStrings["WWI"].ConnectionString);
				parms.Add(new SqlParameter("@PersonId", personId));
				DataTable dt = db.Execute(sql, parms);
				DataRow row = dt.Rows[0];

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
			}
			catch (Exception ex)
			{
				Logger.LogError("GetPerson exception: ", ex);
			}
			finally
			{
				db.Close();
			}

			return model;
		}

		public AspNetEditUserModel GetUser(string id)
		{
			AspNetEditUserModel model = new AspNetEditUserModel();

			SQLData db = new SQLData();
			string sql = "SELECT * FROM dbo.AspNetUsers WHERE Id=@Id";
			List<SqlParameter> parms = new List<SqlParameter>();

			try
			{
				db.Open(ConfigurationManager.ConnectionStrings["WWI"].ConnectionString);
				parms.Add(new SqlParameter("@Id", id));
				DataTable dt = db.Execute(sql, parms);
				DataRow row = dt.Rows[0];

				if (!row["Id"].ToString().Equals(string.Empty))
					model.Id = row["Id"].ToString();

				if (!row["UserName"].ToString().Equals(string.Empty))
					model.UserName = row["UserName"].ToString();

				if (!row["Email"].ToString().Equals(string.Empty))
					model.Email = row["Email"].ToString();

				if (!row["EmailConfirmed"].ToString().Equals(string.Empty))
					model.EmailConfirmed = (Boolean)row["EmailConfirmed"];

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
				else
					model.LockoutEndDateUtc = DateTime.Now;

				if (!row["LockoutEnabled"].ToString().Equals(string.Empty))
					model.LockoutEnabled = (Boolean)row["LockoutEnabled"];

				if (!row["AccessFailedCount"].ToString().Equals(string.Empty))
					model.AccessFailedCount = (Int32)row["AccessFailedCount"];
			}
			catch (Exception ex)
			{
				Logger.LogError("GetUser exception: ", ex);
			}

			return model;
		}

		public List<AspNetUsersModel> GetUsers()
		{
			return GetUsers(1);
		}

		public List<AspNetUsersModel> GetUsers(int order)
		{
			return GetUsers(order, "asc");
		}

		public List<AspNetUsersModel> GetUsers(int order, string direction)
		{
			AspNetUsers db = new AspNetUsers(new QueryModel(new Order(order, direction)));
			db.Fetch();
			return db.Items;
		}

		public int AddUser(AspNetNewUserModel model)
		{
			int _ret = 0;
			string userId = Guid.NewGuid().ToString();
			SQLData db = new SQLData();
			string sql = @"INSERT INTO dbo.AspNetUsers (Id, Email, EmailConfirmed, PasswordHash, PhoneNumber, PhoneNumberConfirmed, 
														TwoFactorEnabled, LockoutEnabled, UserName, AccessFailedCount)
												VALUES(@Id, @Email, @EmailConfirmed, @PasswordHash, @PhoneNumber, @PhoneNumberConfirmed,
														@TwoFactorEnabled, @LockoutEnabled, @UserName, 0)";

			try
			{
				// Insert AspNetUsers table information.
				List<SqlParameter> parms = new List<SqlParameter>();
				parms.Add(new SqlParameter("@Id", userId));
				parms.Add(new SqlParameter("@Email", model.Email));
				parms.Add(new SqlParameter("@EmailConfirmed", model.EmailConfirmed));
				parms.Add(new SqlParameter("@PasswordHash", model.PasswordHash));
				parms.Add(new SqlParameter("@PhoneNumber", model.PhoneNumber));
				parms.Add(new SqlParameter("@PhoneNumberConfirmed", model.PhoneNumberConfirmed));
				parms.Add(new SqlParameter("@TwoFactorEnabled", model.TwoFactorEnabled));
				parms.Add(new SqlParameter("@LockoutEnabled", model.LockoutEnabled));
				parms.Add(new SqlParameter("@UserName", model.UserName));
				db.Open(ConfigurationManager.ConnectionStrings["WWI"].ConnectionString);
				db.ExecuteNonQuery(sql, parms);
				db.Close();

				// Insert User roles.
				if (model.cbAdministrator)
					InsertUserRole(userId, "Administrator");
				if (model.cbContractor)
					InsertUserRole(userId, "Contractor");
				if (model.cbExecutive)
					InsertUserRole(userId, "Executive");
				if (model.cbInventory)
					InsertUserRole(userId, "Inventory");
				if (model.cbSales)
					InsertUserRole(userId, "Sales");
				if (model.cbSupplier)
					InsertUserRole(userId, "Supplier");
				if (model.cbUser)
					InsertUserRole(userId, "User");
				if (model.cbVendor)
					InsertUserRole(userId, "Vendor");

			}
			catch (Exception ex)
			{
				Logger.LogError("WWIDal.AddUser Exception", ex);
			}

			return _ret;
		}

		public void InsertUserRole(string userId, string roleName)
		{
			SQLData db = new SQLData();
			List<SqlParameter> roleParms = new List<SqlParameter>();

			roleParms.Add(new SqlParameter("@Name", roleName));
			db.Open(ConfigurationManager.ConnectionStrings["WWI"].ConnectionString);
			DataTable dt = db.Execute("SELECT Id FROM dbo.AspNetRoles WHERE Name = @Name", roleParms);
			if (dt.Rows.Count > 0)
			{
				List<SqlParameter> userRoleParms = new List<SqlParameter>();
				userRoleParms.Add(new SqlParameter("@UserId", userId));
				userRoleParms.Add(new SqlParameter("@RoleId", dt.Rows[0]["Id"].ToString()));
				db.ExecuteNonQuery("INSERT INTO dbo.AspNetUserRoles (UserId, RoleId) VALUES(@UserId, @RoleId)", userRoleParms);
			}
			db.Close();
		}

		public void DeleteUserRole(string userId, string roleName)
		{
			SQLData db = new SQLData();
			List<SqlParameter> roleParms = new List<SqlParameter>();

			roleParms.Add(new SqlParameter("@Name", roleName));
			db.Open(ConfigurationManager.ConnectionStrings["WWI"].ConnectionString);
			DataTable dt = db.Execute("SELECT Id FROM dbo.AspNetRoles WHERE Name = @Name", roleParms);
			if (dt.Rows.Count > 0)
			{
				List<SqlParameter> userRoleParms = new List<SqlParameter>();
				userRoleParms.Add(new SqlParameter("@UserId", userId));
				userRoleParms.Add(new SqlParameter("@RoleId", dt.Rows[0]["Id"].ToString()));
				db.ExecuteNonQuery("DELETE FROM dbo.AspNetUserRoles WHERE UserId=@UserId AND RoleId=@RoleId", userRoleParms);
			}
			db.Close();
		}

		public int UpdateUser(AspNetEditUserModel model)
		{
			int _ret = 0;
			SQLData db = new SQLData();
			StringBuilder sb = new StringBuilder(@"UPDATE dbo.AspNetUsers SET Email=@Email, EmailConfirmed=@EmailConfirmed,
							PhoneNumber=@PhoneNumber, PhoneNumberConfirmed=@PhoneNumberConfirmed, TwoFactorEnabled=@TwoFactorEnabled, 
							LockoutEnabled=@LockoutEnabled, LockoutEndDateUtc=@LockoutEndDateUtc, AccessFailedCount=@AccessFailedCount");

			sb.Append(" WHERE Id=@Id");

			try
			{
				List<SqlParameter> parms = new List<SqlParameter>();
				parms.Add(new SqlParameter("@Email", model.Email));
				parms.Add(new SqlParameter("@EmailConfirmed", model.EmailConfirmed));
				parms.Add(new SqlParameter("@PhoneNumber", (string.IsNullOrEmpty(model.PhoneNumber)) ? "" : model.PhoneNumber));
				parms.Add(new SqlParameter("@PhoneNumberConfirmed", model.PhoneNumberConfirmed));
				parms.Add(new SqlParameter("@TwoFactorEnabled", model.TwoFactorEnabled));
				parms.Add(new SqlParameter("@LockoutEnabled", model.LockoutEnabled));
				parms.Add(new SqlParameter("@LockoutEndDateUtc", model.LockoutEndDateUtc));
				parms.Add(new SqlParameter("@AccessFailedCount", model.AccessFailedCount));
				parms.Add(new SqlParameter("@Id", model.Id));

				db.Open(ConfigurationManager.ConnectionStrings["WWI"].ConnectionString);
				_ret = db.ExecuteNonQuery(sb.ToString(), parms);

				string roleName = "Administrator";
				bool userHasRoleName = WWIDal.UserHasRoleName(model.Id, roleName);
				if (model.cbAdministrator && !userHasRoleName)
					InsertUserRole(model.Id, roleName);
				else if (!model.cbAdministrator && userHasRoleName)
					DeleteUserRole(model.Id, roleName);

				roleName = "Contractor";
				userHasRoleName = WWIDal.UserHasRoleName(model.Id, roleName);
				if (model.cbContractor && !userHasRoleName)
					InsertUserRole(model.Id, roleName);
				else if (!model.cbAdministrator && userHasRoleName)
					DeleteUserRole(model.Id, roleName);

				roleName = "Executive";
				userHasRoleName = WWIDal.UserHasRoleName(model.Id, roleName);
				if (model.cbExecutive && !userHasRoleName)
					InsertUserRole(model.Id, roleName);
				else if (!model.cbExecutive && userHasRoleName)
					DeleteUserRole(model.Id, roleName);

				roleName = "Inventory";
				userHasRoleName = WWIDal.UserHasRoleName(model.Id, roleName);
				if (model.cbInventory && !userHasRoleName)
					InsertUserRole(model.Id, roleName);
				else if (!model.cbInventory && userHasRoleName)
					DeleteUserRole(model.Id, roleName);

				roleName = "Sales";
				userHasRoleName = WWIDal.UserHasRoleName(model.Id, roleName);
				if (model.cbSales && !userHasRoleName)
					InsertUserRole(model.Id, roleName);
				else if (!model.cbSales && userHasRoleName)
					DeleteUserRole(model.Id, roleName);

				roleName = "Supplier";
				userHasRoleName = WWIDal.UserHasRoleName(model.Id, roleName);
				if (model.cbSupplier && !userHasRoleName)
					InsertUserRole(model.Id, roleName);
				else if (!model.cbSupplier && userHasRoleName)
					DeleteUserRole(model.Id, roleName);

				roleName = "User";
				userHasRoleName = WWIDal.UserHasRoleName(model.Id, roleName);
				if (model.cbUser && !userHasRoleName)
					InsertUserRole(model.Id, roleName);
				else if (!model.cbUser && userHasRoleName)
					DeleteUserRole(model.Id, roleName);

				roleName = "Vendor";
				userHasRoleName = WWIDal.UserHasRoleName(model.Id, roleName);
				if (model.cbVendor && !userHasRoleName)
					InsertUserRole(model.Id, roleName);
				else if (!model.cbVendor && userHasRoleName)
					DeleteUserRole(model.Id, roleName);
			}
			catch (Exception ex)
			{
				Logger.LogError("UpdateUser exception: ", ex);
			}
			finally
			{
				db.Close();
			}

			return _ret;
		}

		public void DeleteUser(string id)
		{
			SQLData db = new SQLData();
			string sql = @"dbo.DeleteUser";

			try
			{
				List<SqlParameter> parms = new List<SqlParameter>();
				parms.Add(new SqlParameter("@UserId", id));

				db.Open(ConfigurationManager.ConnectionStrings["WWI"].ConnectionString);
				db.ExecuteNonQueryStoredProcedure(sql, parms);
			}
			catch(Exception ex)
			{
				Logger.LogError("DeleteUser exception: ", ex);
			}
			finally
			{
				db.Close();
			}
		}

		public static bool UserNameExists(string userName)
		{
			bool _ret = false;
			List<SqlParameter> parms = new List<SqlParameter>();
			SQLData db = new SQLData();

			try
			{
				parms.Add(new SqlParameter("@UserName", userName));
				db.Open(ConfigurationManager.ConnectionStrings["WWI"].ConnectionString);
				DataTable dt = db.Execute("SELECT * FROM dbo.AspNetUsers WHERE UserName=@UserName");
				_ret = (dt.Rows.Count > 0);
			}
			catch(Exception ex)
			{
				Logger.LogError("UserNameExists Exception", ex);
			}
			finally
			{
				db.Close();
			}

			return _ret;
		}

		public static string GetUserEmail(string id)
		{
			string _ret = string.Empty;

			List<SqlParameter> parms = new List<SqlParameter>();
			SQLData db = new SQLData();

			try
			{
				parms.Add(new SqlParameter("@Id", id));
				db.Open(ConfigurationManager.ConnectionStrings["WWI"].ConnectionString);
				DataTable dt = db.Execute("SELECT [Email] FROM dbo.AspNetUsers WHERE [Id]=@Id", parms);
				if (dt.Rows.Count > 0)
				{
					_ret = dt.Rows[0]["Email"].ToString();
				}
			}
			catch (Exception ex)
			{
				Logger.LogError("GetUserEmail Exception", ex);
			}
			finally
			{
				db.Close();
			}
			
			return _ret;
		}

		public static bool UpdateSecurityStamp(string id, string securityStamp)
		{
			bool _ret = false;

			List<SqlParameter> parms = new List<SqlParameter>();
			SQLData db = new SQLData();

			try
			{
				parms.Add(new SqlParameter("@Id", id));
				parms.Add(new SqlParameter("@SecurityStamp", securityStamp));
				db.Open(ConfigurationManager.ConnectionStrings["WWI"].ConnectionString);
				int i = db.ExecuteNonQuery("UPDATE dbo.AspNetUsers SET [SecurityStamp]=@SecurityStamp WHERE [Id]=@Id", parms);
				_ret = (i == 1);
			}
			catch (Exception ex)
			{
				Logger.LogError("UpdateSecurityStamp Exception", ex);
			}
			finally
			{
				db.Close();
			}

			return _ret;
		}

		public static string GetSecurityStamp(string id)
		{
			string _ret = string.Empty;

			List<SqlParameter> parms = new List<SqlParameter>();
			SQLData db = new SQLData();

			try
			{
				parms.Add(new SqlParameter("@Id", id));
				db.Open(ConfigurationManager.ConnectionStrings["WWI"].ConnectionString);
				DataTable dt = db.Execute("SELECT [SecurityStamp] FROM dbo.AspNetUsers WHERE [Id]=@Id", parms);
				if (dt.Rows.Count > 0)
				{
					_ret = dt.Rows[0]["SecurityStamp"].ToString();
				}
			}
			catch (Exception ex)
			{
				Logger.LogError("GetSecurityStamp Exception", ex);
			}
			finally
			{
				db.Close();
			}

			return _ret;

		}

		public static bool UpdateEmailConfirmed(string id, bool emailConfirmed)
		{
			bool _ret = false;

			List<SqlParameter> parms = new List<SqlParameter>();
			SQLData db = new SQLData();

			try
			{
				parms.Add(new SqlParameter("@Id", id));
				parms.Add(new SqlParameter("@EmailConfirmed", emailConfirmed));
				db.Open(ConfigurationManager.ConnectionStrings["WWI"].ConnectionString);
				int i = db.ExecuteNonQuery("UPDATE dbo.AspNetUsers SET [EmailConfirmed]=@EmailConfirmed WHERE [Id]=@Id", parms);
				_ret = (i == 1);
			}
			catch (Exception ex)
			{
				Logger.LogError("UpdateEmailConfirmed Exception", ex);
			}
			finally
			{
				db.Close();
			}

			return _ret;
		}



		// Roles
		public static List<AspNetRolesModel> GetRoles()
		{
			return GetRoles(2);
		}

		public static List<AspNetRolesModel> GetRoles(int order)
		{
			AspNetRoles db = new AspNetRoles(new QueryModel(new Order(order)));
			db.Fetch();
			return db.Items;
		}

		public static bool UserHasRole(string userId, string roleId)
		{
			bool _ret = false;
			string sql = @"SELECT * FROM dbo.AspNetUserRoles WHERE UserId=@UserId AND RoleId=@RoleId";
			List<SqlParameter> parms = new List<SqlParameter>();
			SQLData db = new SQLData();

			try
			{
				parms.Add(new SqlParameter("@UserId", userId));
				parms.Add(new SqlParameter("@RoleId", roleId));
				db.Open(ConfigurationManager.ConnectionStrings["WWI"].ConnectionString);
				DataTable dt = db.Execute(sql, parms);
				_ret = (dt.Rows.Count > 0);
			}
			catch (Exception ex)
			{
				Logger.LogError("UserHasRole Exception", ex);
			}
			finally
			{
				db.Close();
			}

			return _ret;
		}

		public static bool UserHasRoleName(string userId, string roleName)
		{
			bool _ret = false;

			string sql = @"SELECT ur.*, r.[Name] FROM dbo.AspNetUserRoles ur, dbo.AspNetRoles r
							WHERE ur.RoleId = r.Id AND r.Name = @RoleName AND ur.UserId = @UserId";
			List<SqlParameter> parms = new List<SqlParameter>();
			SQLData db = new SQLData();

			try
			{
				parms.Add(new SqlParameter("@UserId", userId));
				parms.Add(new SqlParameter("@RoleName", roleName));
				db.Open(ConfigurationManager.ConnectionStrings["WWI"].ConnectionString);
				DataTable dt = db.Execute(sql, parms);
				_ret = (dt.Rows.Count > 0);
			}
			catch (Exception ex)
			{
				Logger.LogError("UserHasRoleName Exception", ex);
			}
			finally
			{
				db.Close();
			}

			return _ret;
		}

		public AspNetRolesModel AddRole(string name)
		{
			SQLData db = new SQLData();
			string sql = "INSERT INTO dbo.AspNetRoles (Id, Name, AccessLevel) VALUES(@Id, @Name, @AccessLevel)";
			List<SqlParameter> parms = new List<SqlParameter>();
			AspNetRolesModel model = new AspNetRolesModel();

			try
			{
				model.Id = Guid.NewGuid().ToString();
				model.Name = name;
				parms.Add(new SqlParameter("@Id", model.Id));
				parms.Add(new SqlParameter("@Name", model.Name));
				db.Open(ConfigurationManager.ConnectionStrings["WWI"].ConnectionString);
				db.ExecuteNonQuery(sql, parms);
			}
			catch(Exception ex)
			{
				Logger.LogError("AddRole Exception: ", ex);
			}
			finally
			{
				db.Close();
			}

			return model;
		}

		public AspNetRolesModel GetRole(string id)
		{
			SQLData db = new SQLData();
			string sql = "SELECT * FROM dbo.AspNetRoles WHERE Id=@Id";
			List<SqlParameter> parms = new List<SqlParameter>();
			AspNetRolesModel model = new AspNetRolesModel();

			try
			{
				db.Open(ConfigurationManager.ConnectionStrings["WWI"].ConnectionString);
				parms.Add(new SqlParameter("@Id", id));
				DataTable dt = db.Execute(sql, parms);
				DataRow row = dt.Rows[0];

				if (!row["Id"].ToString().Equals(string.Empty))
					model.Id = row["Id"].ToString();

				if (!row["Name"].ToString().Equals(string.Empty))
					model.Name = row["Name"].ToString();

			}
			catch (Exception ex)
			{
				Logger.LogError("GetRole exception: ", ex);
			}
			finally
			{
				db.Close();
			}

			return model;
		}

		public int UpdateRole(AspNetRolesModel model)
		{
			SQLData db = new SQLData();
			string sql = "UPDATE dbo.AspNetRoles SET Name=@Name, AccessLevel=@AccessLevel WHERE Id=@Id";
			List<SqlParameter> parms = new List<SqlParameter>();
			int _ret = 0;

			try
			{
				parms.Add(new SqlParameter("@Id", model.Id));
				parms.Add(new SqlParameter("@Name", model.Name));
				db.Open(ConfigurationManager.ConnectionStrings["WWI"].ConnectionString);
				_ret = db.ExecuteNonQuery(sql, parms);
			}
			catch (Exception ex)
			{
				Logger.LogError("UpdateRole Exception: ", ex);
			}
			finally
			{
				db.Close();
			}

			return _ret;
		}

		public void DeleteRole(string id)
		{
			SQLData db = new SQLData();
			string sql = @"dbo.DeleteRole";

			try
			{
				List<SqlParameter> parms = new List<SqlParameter>();
				parms.Add(new SqlParameter("@RoleId", id));

				db.Open(ConfigurationManager.ConnectionStrings["WWI"].ConnectionString);
				db.ExecuteNonQueryStoredProcedure(sql, parms);
			}
			catch (Exception ex)
			{
				Logger.LogError("DeleteRole exception: ", ex);
			}
			finally
			{
				db.Close();
			}
		}

		public List<AspNetRolesModel> GetUserRoles(string id)
		{
			List<AspNetRolesModel> _ret = new List<AspNetRolesModel>();
			AspNetRoles db = new AspNetRoles();
			QueryModel qModel = new QueryModel();

			try
			{
				qModel.search = "UserId=" + id;
				db.queryModel = qModel;
				db.Fetch();
				_ret = db.Items;
			}
			catch (Exception ex)
			{
				Logger.LogError("GetUserRoles: ", ex);
			}

			return _ret;
		}


		// Access Levels
		public List<AspNetAccessLevelsModel> GetAccessLevels()
		{
			return GetAccessLevels(1);
		}

		public List<AspNetAccessLevelsModel> GetAccessLevels(int order)
		{
			AspNetAccessLevels db = new AspNetAccessLevels(new QueryModel(new Order(order)));
			db.Fetch();
			return db.Items;
		}

		public AspNetAccessLevelsModel AddAccessLevel(AspNetAccessLevelsModel model)
		{
			SQLData db = new SQLData();
			string sql = "INSERT INTO dbo.AspNetAccessLevels (Id, Name, Description, AccessLevel) VALUES(@Id, @Name, @Description, @AccessLevel)";
			List<SqlParameter> parms = new List<SqlParameter>();

			try
			{
				if (string.IsNullOrEmpty(model.Description))
					model.Description = "";
				model.Id = Guid.NewGuid().ToString();
				parms.Add(new SqlParameter("@Id", model.Id));
				parms.Add(new SqlParameter("@Name", model.Name));
				parms.Add(new SqlParameter("@Description", model.Description));
				parms.Add(new SqlParameter("@AccessLevel", model.AccessLevel));
				db.Open(ConfigurationManager.ConnectionStrings["WWI"].ConnectionString);
				db.ExecuteNonQuery(sql, parms);
			}
			catch (Exception ex)
			{
				Logger.LogError("AddAccessLevel Exception: ", ex);
			}
			finally
			{
				db.Close();
			}

			return model;
		}

		public AspNetAccessLevelsModel GetAccessLevel(string id)
		{
			SQLData db = new SQLData();
			string sql = "SELECT * FROM dbo.AspNetAccessLevels WHERE Id=@Id";
			List<SqlParameter> parms = new List<SqlParameter>();
			AspNetAccessLevelsModel model = new AspNetAccessLevelsModel();

			try
			{
				db.Open(ConfigurationManager.ConnectionStrings["WWI"].ConnectionString);
				parms.Add(new SqlParameter("@Id", id.ToString()));
				DataTable dt = db.Execute(sql, parms);
				DataRow row = dt.Rows[0];

				if (!row["Id"].ToString().Equals(string.Empty))
					model.Id = row["Id"].ToString();

				if (!row["Name"].ToString().Equals(string.Empty))
					model.Name = row["Name"].ToString();

				if (!row["Description"].ToString().Equals(string.Empty))
					model.Description = row["Description"].ToString();

				if (!row["AccessLevel"].ToString().Equals(string.Empty))
					model.AccessLevel = (int)row["AccessLevel"];
			}
			catch (Exception ex)
			{
				Logger.LogError("GetAccessLevel exception: ", ex);
			}
			finally
			{
				db.Close();
			}

			return model;
		}

		public int UpdateAccessLevel(AspNetAccessLevelsModel model)
		{
			SQLData db = new SQLData();
			string sql = "UPDATE dbo.AspNetAccessLevels SET Name=@Name, Description=@Description, AccessLevel=@AccessLevel WHERE Id=@Id";
			List<SqlParameter> parms = new List<SqlParameter>();
			int _ret = 0;

			try
			{
				parms.Add(new SqlParameter("@Id", model.Id));
				parms.Add(new SqlParameter("@Name", model.Name));
				parms.Add(new SqlParameter("@Description", model.Description));
				parms.Add(new SqlParameter("@AccessLevel", model.AccessLevel));
				db.Open(ConfigurationManager.ConnectionStrings["WWI"].ConnectionString);
				_ret = db.ExecuteNonQuery(sql, parms);
			}
			catch (Exception ex)
			{
				Logger.LogError("UpdateAccessLevel Exception: ", ex);
			}
			finally
			{
				db.Close();
			}

			return _ret;
		}

		public int DeleteAccessLevel(string id)
		{
			SQLData db = new SQLData();
			string sql = "DELETE FROM dbo.AspNetAccessLevels WHERE Id=@Id";
			List<SqlParameter> parms = new List<SqlParameter>();
			int _ret = 0;

			try
			{
				parms.Add(new SqlParameter("@Id", id));
				db.Open(ConfigurationManager.ConnectionStrings["WWI"].ConnectionString);
				_ret = db.ExecuteNonQuery(sql, parms);
			}
			catch (Exception ex)
			{
				Logger.LogError("DeleteAccessLevel Exception: ", ex);
			}
			finally
			{
				db.Close();
			}

			return _ret;
		}

	}
}