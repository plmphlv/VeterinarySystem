using Animal.Contracts;
using Common.Common;
using Common.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prescriptions.Contracts;
using Prescriptions.Prescription;
using VeterinarySystem.Common;
using VeterinarySystem.Data.DataSeeding.Admin;

namespace VeterinarySystem.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = AdminUser.AdminRoleName)]
	[Area(AdminUser.AdminArea)]
	public class PrescriptionController : Controller
	{

		private readonly IAnimalService animalService;

		private readonly IPrescriptionService prescriptionService;

		public PrescriptionController(IPrescriptionService _prescriptionService, IAnimalService _animalService)
		{
			this.prescriptionService = _prescriptionService;
			this.animalService = _animalService;
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			if (!await prescriptionService.PrescriptionExists(id))
			{
				return BadRequest();
			}

			PrescriptionFormModel editForm = await prescriptionService.GetFormForEditing(id);
			editForm.Staff = await prescriptionService.GetStaffMembers();

			return View(editForm);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, PrescriptionFormModel editForm)
		{
			if (!await prescriptionService.PrescriptionExists(id))
			{
				return BadRequest();
			}

			string number = await prescriptionService.GetPrescriptionNumber();

			if (editForm.Number != number)
			{
				ModelState.AddModelError(nameof(editForm.Number), ErrorMessages.PrescriptionNumberError);
			}

			DateTime date = DateTimeQuickTools.GetDateAndTime().Date;

			if (editForm.IssueDate != date)
			{
				ModelState.AddModelError(nameof(editForm.IssueDate), ErrorMessages.IssueDateError);
			}

			if (!ModelState.IsValid)
			{
				editForm.Number = number;
				editForm.IssueDate = date;
				editForm.Staff = await prescriptionService.GetStaffMembers();
				return View(editForm);
			}

			await prescriptionService.EditPrescription(id, editForm);

			return RedirectToAction(nameof(Details), new { Id = id });
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			if (!await prescriptionService.PrescriptionExists(id))
			{
				return BadRequest();
			}
			string controllerName = this.GetType().Name.Replace("Controller", "");
			DeleteViewModel editForm = await prescriptionService.GetDeleteViewModel(id, controllerName);

			return View(editForm);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id, DeleteViewModel model)
		{
			if (!await prescriptionService.PrescriptionExists(id))
			{
				return BadRequest();
			}

			await prescriptionService.DeletePrescription(id);

			return RedirectToAction(nameof(Details), new { Id = id });
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			if (!await prescriptionService.PrescriptionExists(id))
			{
				return BadRequest();
			}

			PrescriptionServiceModel model = await prescriptionService.GetPrescriptionDetails(id);

			return View(model);
		}
	}
}
