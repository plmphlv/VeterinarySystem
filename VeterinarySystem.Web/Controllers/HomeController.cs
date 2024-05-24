using Appointments.Appointment;
using Appointments.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Users.Contracts;
using VeterinarySystem.Web.Infrastructure;
using VeterinarySystem.Web.Models;

namespace VeterinarySystem.Web.Controllers
{
    public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IAppointmentService appointmentService;
		private readonly IUserService userService;

		public HomeController(ILogger<HomeController> logger, 
			IAppointmentService _appointmentService, 
			IUserService _userService)
		{
			_logger = logger;
			appointmentService = _appointmentService;
			userService = _userService;
		}

		public async Task<IActionResult> Index()
		{
			if (this.User.Identity.IsAuthenticated)
			{
				return RedirectToAction(nameof(Welcome));
			}

			return View();
		}

		[Authorize]
		public async Task<IActionResult> Welcome()
		{
			ICollection<AppointmentServiceModel> schedule = await appointmentService.TodaysSchedule();

			string lastName = await userService.GetUserLastName(this.User.Id());


			return View((lastName, schedule));
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
