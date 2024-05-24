using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Users.Contracts;
using Users.User;
using VeterinarySystem.Common;
using VeterinarySystem.Data.DataSeeding.Admin;

namespace VeterinarySystem.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = AdminUser.AdminRoleName)]
	[Area(AdminUser.AdminArea)]
	public class UserController : Controller
	{
		private readonly IUserService userService;
		private readonly RoleManager<IdentityRole> roleManager;

		public UserController(RoleManager<IdentityRole> _roleManager, IUserService _userService)
		{
			this.userService = _userService;
			this.roleManager = _roleManager;
		}

		public IActionResult AdminMenu()
		{
			return View();
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterUserModel model)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var result = await userService.Register(model);

			if (!result.Succeeded)
			{
				return View("UserError", ErrorMessages.ErrorRegistering);
			}

			return RedirectToAction("Index", "Home");
		}

		public async Task<IActionResult> DisableUser()
		{
			DisableUserFormModel model = new DisableUserFormModel();
			model.Staff = await userService.GetStaffMembers();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> DisableUser(DisableUserFormModel model)
		{
			await userService.Delete(model.StaffMemberId);

			return RedirectToAction("AdminMenu");
		}
	}
}
