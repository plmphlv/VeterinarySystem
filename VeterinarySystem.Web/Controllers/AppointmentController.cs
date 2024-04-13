using Microsoft.AspNetCore.Mvc;
using VeterinarySystem.Common;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.Appointment;
using VeterinarySystem.Core.Models.Common;
using VeterinarySystem.Core.Tools.ExtenshionMethods;

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
				Date = DateTimeQuickTools.GetDateAndTime(),
				Staff = await appointmentService.GetStaffMembers()
			};

			return View(appointmentFromModel);
		}

		[HttpPost]
		public async Task<IActionResult> Add(int id, AppointmentFromModel form)
		{
			if (!await ownerService.AnimalOwnerExists(id))
			{
				return BadRequest();
			}

			if (!form.Date.CompareDate())
			{
				ModelState
				   .AddModelError(nameof(form.Date), ErrorMessages.EarlierThatTodayDateError);

			}

			if (!ModelState.IsValid)
			{
				form.Staff = await appointmentService.GetStaffMembers();
				return View(form);
			}

			int newEntityId = await appointmentService.AddAppointment(form, id);

			return RedirectToAction(nameof(Details), new { Id = newEntityId });
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

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			if (!await appointmentService.AppointmenExists(id))
			{
				return BadRequest();
			}

			AppointmentFromModel form = await appointmentService.GetFormForEditing(id);

			form.Staff = await appointmentService.GetStaffMembers();

			return View(form);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, AppointmentFromModel form)
		{
			if (!await appointmentService.AppointmenExists(id))
			{
				return BadRequest();
			}

			if (!form.Date.CompareDate())
			{
				ModelState
				   .AddModelError(nameof(form.Date), ErrorMessages.EarlierThatTodayDateError);

			}

			if (!ModelState.IsValid)
			{
				form.Staff = await appointmentService.GetStaffMembers();

				return View(form);
			}

			await appointmentService.EditAppointment(id, form);

			return RedirectToAction("Details", new { Id = id });
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

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			string controllerName = this.GetType().Name.Replace("Controller", "");
			DeleteViewModel model = await appointmentService.GetDeleteViewModel(id, controllerName);

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id, DeleteViewModel model)
		{
			if (!await appointmentService.AppointmenExists(id))
			{
				return BadRequest();
			}

			int ownerId = await appointmentService.DeleteAppointment(id);

			return RedirectToAction(nameof(Details), "AnimalOwner", new { Id = ownerId });
		}

		[HttpGet]
		public async Task<IActionResult> Search([FromQuery] AllAppointmentsQueryModel query)
		{
			if (query.StartDate is null)
			{
				query.StartDate = DateTime.Today;
			}

			if (query.EndDate is null)
			{
				query.EndDate = DateTime.Today;
			}

			AppointmenQueryServiceModel queryResult = await appointmentService.GetAppointmensForPeriod(
				query.StartDate,
				query.EndDate,
				query.CurrentPage, // Pass currentPage parameter
				AllAppointmentsQueryModel.AppointmensPerPage // Pass appointmentsPerPage parameter
			);

			query.Appointments = queryResult.Appointmens;
			query.TotalAppointmensCount = queryResult.TotalAppointmens;
			query.CurrentPage = query.CurrentPage; // Update the CurrentPage property
			query.TotalPages = queryResult.TotalPages;

			return View(query);
		}
	}
}
