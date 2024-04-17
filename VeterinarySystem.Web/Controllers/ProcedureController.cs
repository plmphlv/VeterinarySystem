﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeterinarySystem.Common;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Infrastructure;
using VeterinarySystem.Core.Models.Common;
using VeterinarySystem.Core.Models.Prescription;
using VeterinarySystem.Core.Models.Procedure;
using VeterinarySystem.Core.Services;
using VeterinarySystem.Core.Tools.ExtenshionMethods;
using VeterinarySystem.Data.DataSeeding.Admin;

namespace VeterinarySystem.Web.Controllers
{
	[Authorize]
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
				Date = DateTimeQuickTools.GetDateAndTime(),
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
				form.Staff = await procedureService.GetStaffMembers();
				return View(form);
			}

			int newEntityId = await procedureService.CreateNewProcedude(id, form);

			return RedirectToAction(nameof(Details), new { Id = newEntityId });
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id, string information)
		{
			if (!await procedureService.ProcedureExists(id))
			{
				return BadRequest();
			}

			ProcedureServiceModel model = await procedureService.GetProcedudeDetails(id);

			if (information != model.GetInformationWithDescription())
			{
				return BadRequest();
			}

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
				form.Staff = await procedureService.GetStaffMembers();
				return View(form);
			}

			await procedureService.EditProcedude(form, id);

			return RedirectToAction(nameof(Details), new { Id = id });
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			string controllerName = this.GetType().Name.Replace("Controller", "");
			DeleteViewModel model = await procedureService.GetDeleteViewModel(id, controllerName);

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id, DeleteViewModel model)
		{
			if (!await procedureService.ProcedureExists(id))
			{
				return BadRequest();
			}

			int entityId = await procedureService.DeleteProcedude(id);

			return RedirectToAction(nameof(Details), "Animal", new { Id = entityId });
		}

		[HttpGet]
		public async Task<IActionResult> ProcedureHistory([FromQuery] ProcedureHistoryQueryModel query)
		{
			if (!await animalService.AnimalExists(query.AnimalId))
			{
				return BadRequest();
			}

			ProcedureQueryServiceModel serviceQuery = await procedureService.GetProcedureHistory(query.AnimalId,
				query.Order,
				query.CurrentPage,
				ProcedureHistoryQueryModel.ProceduresPerPage);

			query.TotalProcedures = serviceQuery.TotalProcedures;
			query.TotalPages = serviceQuery.TotalPages;
			query.Procedures = serviceQuery.Procedures;

			return View(query);
		}
	}
}
