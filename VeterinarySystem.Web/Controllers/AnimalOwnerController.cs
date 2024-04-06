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
		public async Task<IActionResult> AddNewOwner()
		{
			AnimalOwnerFormModel newOwner = new AnimalOwnerFormModel();

			return View(newOwner);
		}

		[HttpPost]
		public async Task<IActionResult> AddNewOwner(AnimalOwnerFormModel newOwner)
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

			return RedirectToAction(nameof(AnimalOwnerDetails), new { newId });
		}

		[HttpGet]
		public async Task<IActionResult> AnimalOwnerDetails(int id)
		{
			if (!await animaOwnerService.AnimalOwnerExists(id))
			{
				return BadRequest();
			}

			AnimalOwnerDetailsModel ownerDetails = await animaOwnerService.GetOwnerDetails(id);

			return View(ownerDetails);
		}

		[HttpGet]
		public async Task<IActionResult> EditOwner(int id)
		{
			AnimalOwnerFormModel ownerForm = await animaOwnerService.GetForm(id);

			return View(ownerForm);
		}

		[HttpPost]
		public async Task<IActionResult> EditOwner(int id, AnimalOwnerFormModel model)
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

			return RedirectToAction(nameof(AnimalOwnerDetails), new { id });
		}
	}
}
