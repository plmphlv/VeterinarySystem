using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeterinarySystem.Common;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.Animal;
using VeterinarySystem.Core.Models.AnimalOwner;
using VeterinarySystem.Core.Services;

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
			OwnerQueryModel ownerQueryResult = await animaOwnerService.Search(query.SearchTerm, query.Parameter);

			query.TotalOwnersCount = ownerQueryResult.SearchResults;
			query.Owners = ownerQueryResult.OwnersFound;

			return View(query);
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			AnimalOwnerFormModel newOwner = new AnimalOwnerFormModel();

			return View(newOwner);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AnimalOwnerFormModel newOwner)
		{
			if (await animaOwnerService.AnimalOwnerExists(newOwner))
			{
				ModelState.AddModelError(nameof(newOwner), ErrorMessages.OwnerExistsError);
			}

			if (!ModelState.IsValid)
			{
				return View(newOwner);
			}

			int newId = await animaOwnerService.AddAnimalOwner(newOwner);

			return RedirectToAction(nameof(Details), new { Id = newId });
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			if (!await animaOwnerService.AnimalOwnerExists(id))
			{
				return BadRequest();
			}

			OwnerServiceModel ownerDetails = await animaOwnerService.GetOwnerDetails(id);

			return View(ownerDetails);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			AnimalOwnerFormModel ownerForm = await animaOwnerService.GetForm(id);

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

			return RedirectToAction(nameof(Details), new { id });
		}
	}
}
