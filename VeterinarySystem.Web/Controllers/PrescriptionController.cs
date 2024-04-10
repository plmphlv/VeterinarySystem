using Microsoft.AspNetCore.Mvc;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.Prescription;
using VeterinarySystem.Core.Tools.ExtenshionMethods;

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
	}
}
