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

			await animaOwnerService.AddAnimalOwner(newOwner);

			return RedirectToAction(nameof(HomeController.Privacy), "Privacy");
		}
	}
}
