using Animal.Animal;
using Animal.Contracts;
using AnimalOwner.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeterinarySystem.Data.DataSeeding.Admin;

namespace VeterinarySystem.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = AdminUser.AdminRoleName)]
	[Area(AdminUser.AdminArea)]
	public class AnimalController : Controller
	{
		private readonly IAnimalOwnerService ownerService;
		private readonly IAnimalService animalService;

		public AnimalController(IAnimalOwnerService _ownerService, IAnimalService _animalService)
		{
			ownerService = _ownerService;
			animalService = _animalService;
		}

		public IActionResult AddNewAnimalType()
		{
			AnimalTypeFormModel model = new AnimalTypeFormModel();


			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> AddNewAnimalType(AnimalTypeFormModel model)
		{
			await animalService.AddNewAnimalType(model);

			return RedirectToAction("AdminMenu");
		}

		public async Task<IActionResult> DeleteAnimalType()
		{
			AnimalTypeDeleteFormModel model = new AnimalTypeDeleteFormModel();
			model.AnimalTypes = await animalService.AllAnimalTypes();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteAnimalType(AnimalTypeDeleteFormModel model)
		{
			await animalService.DeleteAnimalType(model);

			return RedirectToAction("AdminMenu");
		}
	}
}
