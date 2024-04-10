using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.Appointment;
using VeterinarySystem.Web.Models;

namespace VeterinarySystem.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IAppointmentService appointmentService;

		public HomeController(ILogger<HomeController> logger, IAppointmentService _appointmentService)
		{
			_logger = logger;
			appointmentService = _appointmentService;
		}

		public async Task<IActionResult> Index()
		{
			if (this.User.Identity.IsAuthenticated)
			{
				ICollection<AppointmentServiceModel> schedule = await appointmentService.TodaysSchedule();

				return View(schedule);
			}
			return View();
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
