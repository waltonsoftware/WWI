using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using WWI.Data;
using WWI.Models;
using WWI.Log4NetExtension;
using WWI.Managers;
using WWI.Helpers;

namespace WWI.Controllers
{
    public partial class AdministrationController : BaseController
    {
        // GET: Administration
        public ActionResult Index()
        {
            return View();
        }


		//--------------------------------------------
		// USERS -------------------------------------
		public ActionResult Users()
		{
			WWIDal dal = new WWIDal();
			return View(dal.GetUsers(12));
		}

		public ActionResult AddUser()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddUser(AspNetNewUserModel model)
		{
			if (WWIDal.UserNameExists(model.UserName))
				return RedirectToAction("Users", "Administration");

			HashingManager hasher = new HashingManager();
			WWIDal dal = new WWIDal();
			AspNetNewUserModel userModel = new AspNetNewUserModel();

			try
			{
				userModel = new AspNetNewUserModel()
				{
					AccessFailedCount = 0,
					Email = model.Email,
					EmailConfirmed = model.EmailConfirmed,
					Id = Guid.NewGuid().ToString(),
					LockoutEnabled = false,
					LockoutEndDateUtc = DateTime.MinValue,
					PasswordHash = hasher.HashToString(model.Password),
					PhoneNumber = (string.IsNullOrEmpty(model.PhoneNumber)) ? "" : model.PhoneNumber,
					PhoneNumberConfirmed = model.PhoneNumberConfirmed,
					TwoFactorEnabled = model.TwoFactorEnabled,
					UserName = model.UserName,
					cbAdministrator = model.cbAdministrator,
					cbExecutive = model.cbExecutive,
					cbInventory = model.cbInventory,
					cbSales = model.cbSales,
					cbSupplier = model.cbSupplier,
					cbUser = model.cbUser,
					cbVendor = model.cbVendor
				};

				dal = new WWIDal();
				int ret = dal.AddUser(userModel);
			}
			catch (Exception ex)
			{
				Logger.LogError("AddUser Exception", ex);
			}

			return RedirectToAction("Users", "Administration");
		}

		public ActionResult EditUser(string id)
		{
			var principal = System.Security.Claims.ClaimsPrincipal.Current;
			var isAdmin = principal.HasClaim(ClaimTypes.Role, "admin");

			WWIDal dal = new WWIDal();
			return View(dal.GetUser(id));
		}

		[HttpPost]
		public ActionResult EditUser(AspNetEditUserModel model)
		{
			try
			{
				WWIDal dal = new WWIDal();
				int recordsEffected = dal.UpdateUser(model);
			}
			catch(Exception ex)
			{
				Logger.LogError("EditUser Post exception: ", ex);
			}

			return RedirectToAction("Users");
		}

		public ActionResult SendConfirmEmail(string id)
		{
			string stamp = Guid.NewGuid().ToString();
			string body = string.Format(@"http://localhost:62844/Administration/ConfirmEmail?Id={0}&stamp={1}", id, stamp);
			if (WWIDal.UpdateSecurityStamp(id, stamp))
			{
				WWIDal dal = new WWIDal();
				AspNetEditUserModel model = dal.GetUser(id);
				MailHelper.SendMail(WWIDal.GetUserEmail(id), body, "Email Confirmation for " + model.UserName);
			}
			return RedirectToAction("EditUser", new { Id = id });
		}

		public ActionResult ConfirmEmail(string id, string stamp)
		{
			string securityStamp = WWIDal.GetSecurityStamp(id);

			if (stamp == securityStamp)
			{
				WWIDal.UpdateEmailConfirmed(id, true);
				return RedirectToAction("EmailConfirmed", "Administration");
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		public ActionResult EmailConfirmed()
		{
			return View();
		}

		public ActionResult DeleteUser(string id)
		{
			WWIDal dal = new WWIDal();
			dal.DeleteUser(id);
			return RedirectToAction("Users");
		}



		//--------------------------------------------
		// ROLES -------------------------------------
		public ActionResult Roles()
		{
			return View(WWIDal.GetRoles());
		}

		public ActionResult AddRole()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddRole(AspNetRolesModel model)
		{
			try
			{
				WWIDal dal = new WWIDal();
				model = dal.AddRole(model.Name);
			}
			catch(Exception ex)
			{
				Logger.LogError("AddRole Post Exception: ", ex);
			}

			return RedirectToAction("Roles");
		}
		
		public ActionResult EditRole(string id)
		{
			WWIDal dal = new WWIDal();
			return View(dal.GetRole(id));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditRole(AspNetRolesModel model)
		{
			try
			{
				WWIDal dal = new WWIDal();
				int recordsEffected = dal.UpdateRole(model);
			}
			catch (Exception ex)
			{
				Logger.LogError("UpdateRole Post exception: ", ex);
			}

			return RedirectToAction("Roles");
		}

		public ActionResult DeleteRole(string id)
		{
			WWIDal dal = new WWIDal();
			dal.DeleteRole(id);

			return RedirectToAction("Roles");
		}


		//--------------------------------------------
		// LEVELS ------------------------------------
		public ActionResult AccessLevels()
		{
			WWIDal dal = new WWIDal();
			return View(dal.GetAccessLevels(2));
		}

		public ActionResult AddAccessLevel()
		{
			return View();
		}

		public ActionResult EditAccessLevel(string id)
		{
			WWIDal dal = new WWIDal();
			return View(dal.GetAccessLevel(id));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditAccessLevel(AspNetAccessLevelsModel model)
		{
			WWIDal dal = new WWIDal();
			dal.UpdateAccessLevel(model);

			return RedirectToAction("AccessLevels");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddAccessLevel(AspNetAccessLevelsModel model)
		{
			try
			{
				WWIDal dal = new WWIDal();
				model = dal.AddAccessLevel(model);
			}
			catch(Exception ex)
			{
				Logger.LogError("AddAccessLevel Exception", ex);
			}

			return RedirectToAction("AccessLevels");
		}

		public ActionResult DeleteAccessLevel(string id)
		{
			WWIDal dal = new WWIDal();
			dal.DeleteAccessLevel(id);
			return RedirectToAction("AccessLevels");
		}
	}
}