using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeterinarySystem.Common;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.User;

namespace VeterinarySystem.Web.Controllers
{
	public class UserController : Controller
	{
		private readonly IUserService userService;

		public UserController(IUserService _userService)
		{
			userService = _userService;
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
