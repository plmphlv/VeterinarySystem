using Microsoft.AspNetCore.Mvc;
using VeterinarySystem.Core.Contracts;

namespace VeterinarySystem.Web.Controllers
{
	public class AppointmentController : Controller
	{
		private readonly IAnimalOwnerService ownerService;

		private readonly IAppointmentService appointmentService;

		public AppointmentController(IAnimalOwnerService _animalOwner, IAppointmentService _appointmentService)
		{
			ownerService = _animalOwner;
			appointmentService = _appointmentService;
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
