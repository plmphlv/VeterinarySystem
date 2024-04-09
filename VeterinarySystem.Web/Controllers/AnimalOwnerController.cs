using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeterinarySystem.Common;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.AnimalOwner;

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
		public async Task<IActionResult> Add(AnimalOwnerFormModel fomr)
		{
			if (await animaOwnerService.AnimalOwnerExists(fomr))
			{
				ModelState.AddModelError(nameof(fomr), ErrorMessages.OwnerExistsError);
			}

			if (!ModelState.IsValid)
			{
				return View(fomr);
			}

			int newEntityId = await animaOwnerService.AddAnimalOwner(fomr);

			return RedirectToAction(nameof(Details), new { Id = newEntityId });
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
	}
}
