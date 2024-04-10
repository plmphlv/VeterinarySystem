using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeterinarySystem.Common;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.Animal;
using VeterinarySystem.Core.Models.Common;
using VeterinarySystem.Core.Services;

namespace VeterinarySystem.Web.Controllers
{
	[Authorize]
	public class AnimalController : Controller
	{
		private readonly IAnimalOwnerService ownerService;
		private readonly IAnimalService animalService;

		public AnimalController(IAnimalOwnerService _ownerService, IAnimalService _animalService)
		{
			ownerService = _ownerService;
			animalService = _animalService;
		}

		[HttpGet]
		public async Task<IActionResult> Add(int id)
		{
			if (!await ownerService.AnimalOwnerExists(id))
			{
				return BadRequest();
			}

			AnimalFormModel model = new AnimalFormModel()
			{
				AnimalTypes = await animalService.AllAnimalTypes()
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(int id, AnimalFormModel form)
		{
			if (!await ownerService.AnimalOwnerExists(id))
			{
				return BadRequest();
			}

			if (await animalService.AnimalExists(form, id))
			{
				ModelState.AddModelError(nameof(form), ErrorMessages.AnimalExistsError);
			}

			if (!ModelState.IsValid)
			{
				return View(form);
			}

			int newEntityId = await animalService.AddNewAnimal(form, id);

			return RedirectToAction("Details", new { id = newEntityId });
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			if (!await animalService.AnimalExists(id))
			{
				return BadRequest();
			}

			AnimalServiceModel model = await animalService.GetAnimalDetails(id);

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			if (!await animalService.AnimalExists(id))
			{
				return BadRequest();
			}

			AnimalFormModel model = await animalService.GetAnimalEditingForm(id);

			model.AnimalTypes = await animalService.AllAnimalTypes();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, AnimalFormModel form)
		{
			if (!await animalService.AnimalExists(id))
			{
				return BadRequest();
			}

			int ownerId = await animalService.GetOwnerByPetId(id);


			if (!await ownerService.AnimalOwnerExists(ownerId))
			{
				return BadRequest();
			}

			if (!ModelState.IsValid)
			{
				return View(form);
			}

			await animalService.EditAnimal(id, form);

			return RedirectToAction("Details", new { Id = id });
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			string controllerName = this.GetType().Name.Replace("Controller", "");
			DeleteViewModel model = await animalService.GetDeleteViewModel(id, controllerName);

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id, DeleteViewModel model)
		{
			if (!await animalService.AnimalExists(id))
			{
				return BadRequest();
			}

			int ownerId = await animalService.GetOwnerByPetId(id);

			if (await animalService.AnimalExists(ownerId))
			{
				return BadRequest();
			}

			await animalService.DeleteAnimal(id);

			return RedirectToAction(nameof(Details), "AnimalOwner", new { Id = ownerId });
		}
	}
}
