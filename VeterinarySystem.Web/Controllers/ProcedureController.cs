using Microsoft.AspNetCore.Mvc;
using VeterinarySystem.Common;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.Procedure;
using VeterinarySystem.Core.Services;
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
				Staff = await procedureService.GetStaffMembers(),
				AnimalId = id
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(int id, ProcedureFormModel model)
		{
			if (!await animalService.AnimalExists(id))
			{
				return BadRequest();
			}

			if (!model.Date.CompareDate())
			{
				ModelState
				   .AddModelError(nameof(model.Date), ErrorMessages.EarlierThatTodayDateError);
			}

			if (ModelState.IsValid)
			{
				return View(model);
			}

			int newId = await procedureService.CreateNewProcetude(id, model);

			return RedirectToAction(nameof(Details), new { Id = newId });
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

			ProcedureFormModel model = new ProcedureFormModel()
			{
				Date = DateTimeQuickTools.GetDate(),
				Staff = await procedureService.GetStaffMembers(),
				AnimalId = id
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, ProcedureFormModel model)
		{
			if (!await procedureService.ProcedureExists(id))
			{
				return BadRequest();
			}

			if (!await animalService.AnimalExists(model.AnimalId))
			{
				return BadRequest();
			}

			if (!model.Date.CompareDate())
			{
				ModelState
				   .AddModelError(nameof(model.Date), ErrorMessages.EarlierThatTodayDateError);
			}

			if (ModelState.IsValid)
			{
				return View(model);
			}

			await procedureService.EditProcetude(model, id);

			return RedirectToAction(nameof(Details), new { Id = id });
		}
	}
}
