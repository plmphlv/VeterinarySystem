using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeterinarySystem.Common;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Infrastructure;
using VeterinarySystem.Core.Models.AnimalOwner;
using VeterinarySystem.Core.Models.Common;

namespace VeterinarySystem.Web.Controllers
{
	[Authorize]
	public class AnimalOwnerController : Controller
	{
		private readonly IAnimalOwnerService animaOwnerService;

		public AnimalOwnerController(IAnimalOwnerService _animaOwnerService)
		{
			animaOwnerService = _animaOwnerService;
		}

		[HttpGet]
		public async Task<IActionResult> Search([FromQuery] AllOwnersQuery query)
		{
			OwnerQueryModel ownerQueryResult = await animaOwnerService
				.Search(query.SearchTerm,
				query.Parameter,
				AllOwnersQuery.OwnersPerPAge,
				query.CurrentPage);

			query.TotalOwnersCount = ownerQueryResult.TotalOwnersFound;
			query.Owners = ownerQueryResult.OwnersFound;
			query.TotalPages = ownerQueryResult.TotalPages;

			return View(query);
		}

		[HttpGet]
		public IActionResult Add()
		{
			AnimalOwnerFormModel newOwner = new AnimalOwnerFormModel();

			return View(newOwner);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AnimalOwnerFormModel form)
		{
			if (await animaOwnerService.AnimalOwnerExists(form))
			{
				ModelState.AddModelError(nameof(form), ErrorMessages.OwnerExistsError);
			}

			if (!ModelState.IsValid)
			{
				return View(form);
			}

			int newEntityId = await animaOwnerService.AddAnimalOwner(form);

			return RedirectToAction(nameof(Details), new { Id = newEntityId });
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id, string information)
		{
			if (!await animaOwnerService.AnimalOwnerExists(id))
			{
				return BadRequest();
			}

			OwnerServiceModel ownerDetails = await animaOwnerService.GetOwnerDetails(id);

			if (information != ownerDetails.GetOwnerInformation())
			{
				return BadRequest();
			}

			return View(ownerDetails);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			AnimalOwnerFormModel ownerForm = await animaOwnerService.GetEditingForm(id);

			return View(ownerForm);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, AnimalOwnerFormModel model)
		{
			if (!await animaOwnerService.AnimalOwnerExists(id))
			{
				return BadRequest();
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			await animaOwnerService.EditAnimalOwner(id, model);

			return RedirectToAction(nameof(Details), new { Id = id });
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			string controllerName = this.GetType().Name.Replace("Controller", "");
			DeleteViewModel ownerForm = await animaOwnerService.GetDeleteViewModel(id, controllerName);

			return View(ownerForm);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id, AnimalOwnerFormModel model)
		{
			if (!await animaOwnerService.AnimalOwnerExists(id))
			{
				return BadRequest();
			}

			string controllerName = this.GetType().Name.Replace("Controller", "");
			await animaOwnerService.DeleteAnimalOwner(id);

			return RedirectToAction(nameof(Search));
		}
	}
}
