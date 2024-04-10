using Microsoft.AspNetCore.Mvc;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.Common;
using VeterinarySystem.Core.Models.Prescription;
using VeterinarySystem.Core.Tools.ExtenshionMethods;
using VeterinarySystem.Data.Migrations;

namespace VeterinarySystem.Web.Controllers
{
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
				newForm.Number = number;
				return View(newForm);
			}

			DateTime date = DateTimeQuickTools.GetDateAndTime().Date;

			if (newForm.IssueDate != date)
			{
				newForm.IssueDate = date;
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

			return View(editForm);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, PrescriptionFormModel editForm)
		{
			if (!await prescriptionService.PrescriptionExists(id))
			{
				return BadRequest();
			}

			if (editForm.IssueDate != await prescriptionService.CheckPrescriptionDate(id))
			{
				return BadRequest();
			}

			if (editForm.Number != await prescriptionService.CheckPrescriptionNumber(id))
			{
				return BadRequest();
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
