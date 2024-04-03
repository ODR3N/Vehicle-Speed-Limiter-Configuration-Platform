using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using PModuloLimitadorV.Models;

namespace PModuloLimitadorV.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
	{
		private ModuloLimitadorVelocidadDBEntities db = new ModuloLimitadorVelocidadDBEntities();
		private ApplicationRoleManager _roleManager;
		private ApplicationSignInManager _signInManager;
		private ApplicationUserManager _userManager;

		public AdminController()
		{
		}

		public AdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager, ApplicationSignInManager signInManager)
		{
			UserManager = userManager;
			RoleManager = roleManager;
			SignInManager = signInManager;
		}

		// GET: Admin
		public ActionResult Index()
		{
			return View(db.AspNetUsers.ToList());
		}

		// GET: Admin/Details/5
		public ActionResult Details(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			AspNetUser aspNetUser = db.AspNetUsers.Find(id);
			if (aspNetUser == null)
			{
				return HttpNotFound();
			}
			return View(aspNetUser);
		}

		// GET: Admin/Create
		public ActionResult Create()
		{
			List<Role> rolesDB = RoleManager.Roles.ToList();
			if (rolesDB.Count == 0)
			{
				Role admin = new Role() { Id = Guid.NewGuid().ToString(), Name = "Admin", Description = "Rol de super usuario", Activo = true };
				Role profesor = new Role() { Id = Guid.NewGuid().ToString(), Name = "Profesor", Description = "Rol de usuario profesor", Activo = true };
				Role estudiante = new Role() { Id = Guid.NewGuid().ToString(), Name = "Estudiante", Description = "Rol de usuario estudiante", Activo = true };
				RoleManager.Create(admin);
				RoleManager.Create(profesor);
				RoleManager.Create(estudiante);
				rolesDB = RoleManager.Roles.ToList();
			}

			ViewBag.Roles = rolesDB;
			return View();
		}

		// POST: Admin/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser
				{
					UserName = model.Correo,
					Email = model.Correo,
					Nombre = model.Nombre.Trim(),
					Apellidos = model.Apellidos.Trim(),
					UltimaConexion = DateTime.Now,
					Activo = true

				};
				var result = await UserManager.CreateAsync(user, model.Contraseña);
				if (result.Succeeded)
				{
					IdentityResult resultRole = UserManager.AddToRoles(user.Id, RoleManager.Roles.Where(r => r.Id == model.Rol).FirstOrDefault().Name);
					//await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

					// For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
					// Send an email with this link
					// string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
					// var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
					// await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

					return RedirectToAction("Index", "Admin");
				}
				AddErrors(result);
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		// GET: Admin/Edit/5
		public ActionResult Edit(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			AspNetUser aspNetUser = db.AspNetUsers.Find(id);
			if (aspNetUser == null)
			{
				return HttpNotFound();
			}
			return View(aspNetUser);
		}

		// POST: Admin/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Nombre,Apellidos,UltimaConexion,Activo,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] AspNetUser aspNetUser)
		{
			if (ModelState.IsValid)
			{
				aspNetUser.UltimaConexion = DateTime.Now;
				db.Entry(aspNetUser).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(aspNetUser);
		}

		// GET: Admin/Delete/5
		public ActionResult Delete(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			AspNetUser aspNetUser = db.AspNetUsers.Find(id);
			if (aspNetUser == null)
			{
				return HttpNotFound();
			}
			return View(aspNetUser);
		}

		// POST: Admin/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(string id)
		{
			AspNetUser aspNetUser = db.AspNetUsers.Find(id);
			db.AspNetUsers.Remove(aspNetUser);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		public ApplicationRoleManager RoleManager
		{
			get
			{
				return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
			}
			private set
			{
				_roleManager = value;
			}
		}

		public ApplicationSignInManager SignInManager
		{
			get
			{
				return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
			}
			private set
			{
				_signInManager = value;
			}
		}

		public ApplicationUserManager UserManager
		{
			get
			{
				return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			private set
			{
				_userManager = value;
			}
		}

		private void AddErrors(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError("", error);
			}
		}
	}
}
