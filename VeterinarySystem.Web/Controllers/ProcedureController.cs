using Microsoft.AspNetCore.Mvc;
using VeterinarySystem.Common;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.Procedure;
using VeterinarySystem.Core.Tools.ExtenshionMethods;

namespace VeterinarySystem.Web.Controllers
{
	public class ProcedureController : Controller
	{
		private readonly IAnimalService animalService;

		private readonly IProcedureService procedureService;

		public ProcedureController(IAnimalService _animalService, IProcedureService _procedureService)
		{
			animalService = _animalService;
			procedureService = _procedureService;
		}

		[HttpGet]
		public async Task<IActionResult> Add(int id)
		{
			if (!await animalService.AnimalExists(id))
			{
				return BadRequest();
			}

			ProcedureFormModel model = new ProcedureFormModel()
			{
				Date = DateTimeQuickTools.GetDate(),
				Staff = await procedureService.GetStaffMembers()
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(int id, ProcedureFormModel form)
		{
			if (!await animalService.AnimalExists(id))
			{
				return BadRequest();
			}

			if (!form.Date.CompareDate())
			{
				ModelState
				   .AddModelError(nameof(form.Date), ErrorMessages.EarlierThatTodayDateError);
			}

			if (ModelState.IsValid)
			{
				return View(form);
			}

			int newEntityId = await procedureService.CreateNewProcetude(id, form);

			return RedirectToAction(nameof(Details), new { Id = newEntityId });
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			if (!await procedureService.ProcedureExists(id))
			{
				return BadRequest();
			}

			ProcedureServiceModel model = await procedureService.GetProcetudeDetails(id);

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			if (!await procedureService.ProcedureExists(id))
			{
				return BadRequest();
			}

			ProcedureFormModel form = await procedureService.GetEditingForm(id);

			form.Staff = await procedureService.GetStaffMembers();

			return View(form);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, ProcedureFormModel form)
		{
			if (!await procedureService.ProcedureExists(id))
			{
				return BadRequest();
			}

			if (!await animalService.AnimalExists(form.AnimalId))
			{
				return BadRequest();
			}

			if (!form.Date.CompareDate())
			{
				ModelState
				   .AddModelError(nameof(form.Date), ErrorMessages.EarlierThatTodayDateError);
			}

			if (ModelState.IsValid)
			{
				return View(form);
			}

			await procedureService.EditProcetude(form, id);

			return RedirectToAction(nameof(Details), new { Id = id });
		}
	}
}
