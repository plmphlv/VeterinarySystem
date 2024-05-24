using Animal.Contracts;
using Common.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prescriptions.Contracts;
using Prescriptions.Prescription;
using VeterinarySystem.Common;

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

			//if (!ModelState.IsValid)
			//{
			//	newForm.Number = number;
			//	newForm.IssueDate = date;
			//	newForm.Staff = await prescriptionService.GetStaffMembers();
			//	return View(newForm);
			//}

			int newId = await prescriptionService.AddPrescription(newForm, id);

			return RedirectToAction(nameof(Details), new { Id = newId });
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id, string information)
		{
			if (!await prescriptionService.PrescriptionExists(id))
			{
				return BadRequest();
			}

			PrescriptionServiceModel model = await prescriptionService.GetPrescriptionDetails(id);

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> PrescriptionHistory([FromQuery] PrescriptionsHistoryQueryModel query)
		{
			if (!await animalService.AnimalExists(query.AnimalId))
			{
				return BadRequest();
			}

			PrescriptionsQueryServiceModel serviceQuery = await prescriptionService.GetPrescriptionHistory(query.AnimalId,
				query.Order,
				query.CurrentPage,
				PrescriptionsHistoryQueryModel.PrescriptionsPerPage);

			query.TotalPrescriptions = serviceQuery.TotalPrescriptionsCount;
			query.TotalPages = serviceQuery.TotalPages;
			query.Prescriptions = serviceQuery.Prescriptions;

			return View(query);
		}
	}
}
