using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeterinarySystem.Common;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.Common;
using VeterinarySystem.Core.Models.Prescription;
using VeterinarySystem.Core.Tools.ExtenshionMethods;

namespace VeterinarySystem.Web.Controllers
{
	[Authorize]
	public class PrescriptionController : Controller
	{
		private readonly IAnimalService animalService;

		private readonly IPrescriptionService prescriptionService;

		public PrescriptionController(IPrescriptionService _prescriptionService, IAnimalService _animalService)
		{
			prescriptionService = _prescriptionService;
			animalService = _animalService;
		}

		[HttpGet]
		public async Task<IActionResult> Add(int id)
		{
			if (!await animalService.AnimalExists(id))
			{
				return BadRequest();
			}

			PrescriptionFormModel newForm = await prescriptionService.GetNewPrescriptionForm();
			newForm.Staff = await prescriptionService.GetStaffMembers();

			return View(newForm);
		}

		[HttpPost]
		public async Task<IActionResult> Add(int id, PrescriptionFormModel newForm)
		{
			if (!await animalService.AnimalExists(id))
			{
				return BadRequest();
			}

			string number = await prescriptionService.GetPrescriptionNumber();

			if (newForm.Number != number)
			{
				ModelState.AddModelError(nameof(newForm.Number), ErrorMessages.PrescriptionNumberError);
			}

			DateTime date = DateTimeQuickTools.GetDateAndTime().Date;

			if (newForm.IssueDate != date)
			{
				ModelState.AddModelError(nameof(newForm.IssueDate), ErrorMessages.IssueDateError);
			}

			if (!ModelState.IsValid)
			{
				newForm.Number = number;
				newForm.IssueDate = date;
				newForm.Staff = await prescriptionService.GetStaffMembers();
				return View(newForm);
			}

			int newId = await prescriptionService.AddPrescription(newForm, id);

			return RedirectToAction(nameof(Details), new { Id = newId });
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
	}
}
