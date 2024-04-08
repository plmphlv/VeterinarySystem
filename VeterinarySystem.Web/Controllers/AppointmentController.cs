using Microsoft.AspNetCore.Mvc;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.Appointment;

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

		[HttpGet]
		public async Task<IActionResult> Add(int id)
		{
			if (!await ownerService.AnimalOwnerExists(id))
			{
				return BadRequest();
			}

			AppointmentFromModel appointmentFromModel = new AppointmentFromModel()
			{
				Staff = await appointmentService.GetStaffMembers()
			};

			return View(appointmentFromModel);
		}

		[HttpPost]
		public async Task<IActionResult> Add(int id, AppointmentFromModel model)
		{
			if (!await ownerService.AnimalOwnerExists(id))
			{
				return BadRequest();
			}

			int appointmentId = await appointmentService.AddAppointment(model, id);

			return RedirectToAction(nameof(Details), new { Id = appointmentId });
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			if (!await appointmentService.AppointmenExists(id))
			{
				return BadRequest();
			}

			AppointmentServiceModel model = await appointmentService.GetAppointmentDetails(id);

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> ChangeStatus(int id)
		{
			if (!await appointmentService.AppointmenExists(id))
			{
				return BadRequest();
			}

			await appointmentService.ChangeAppointmentUpcomingStatus(id);

			return RedirectToAction(nameof(Details), new { Id = id });
		}

		[HttpPost]
		public async Task<IActionResult> DeleteAppointment(int id)
		{
			if (!await appointmentService.AppointmenExists(id))
			{
				return BadRequest();
			}

			int ownerId = await appointmentService.DeleteAppointment(id);

			return RedirectToAction(nameof(Details), nameof(AnimalOwnerController), new { Id = ownerId });
		}
	}
}
