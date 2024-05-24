using Microsoft.AspNetCore.Mvc;
using Users.Contracts;
using Users.User;

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
			catch (Exception)
			{
				return RedirectToAction(nameof(Login));
			}


			return RedirectToAction("Welcome", "Home");
		}
	}

}
