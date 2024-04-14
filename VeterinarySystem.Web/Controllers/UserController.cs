using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using VeterinarySystem.Common;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.User;
using VeterinarySystem.Data.DataSeeding.Admin;

namespace VeterinarySystem.Web.Controllers
{
	[Authorize]
	public class UserController : Controller
	{
		private readonly IUserService userService;
		private readonly RoleManager<IdentityRole> roleManager;

		public UserController(RoleManager<IdentityRole> _roleManager, IUserService _userService)
		{
			userService = _userService;
			roleManager = _roleManager;
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginUserModel model)
		{
			try
			{
				await userService.Login(model);
			}
			catch (ArgumentException ae)
			{
				return View("UserError", ae.Message);
			}
			catch (Exception)
			{
				return View("UserError", ErrorMessages.ErrorLoggingIn);
			}


			return RedirectToAction("Index", "Home");
		}
	}

}
