using Microsoft.AspNetCore.Mvc;
using VeterinarySystem.Common;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.Animal;

namespace VeterinarySystem.Web.Controllers
{
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
		//[Route("Animal/Add/{ownerId:int}")]
		public async Task<IActionResult> Add(int id)
		{
			if (!await ownerService.AnimalOwnerExists(id))
			{
				return BadRequest();
			}

			AnimalFormModel model = new AnimalFormModel()
			{
				OwnerId = id,
				AnimalTypes = await animalService.AllAnimalTypes()
			};

			return View(model);
		}

		[HttpPost]
		//[Route("Animal/Add/{ownerId:int}")]
		public async Task<IActionResult> Add(int id, AnimalFormModel model)
		{
			if (!await ownerService.AnimalOwnerExists(id))
			{
				return BadRequest();
			}

			if (await animalService.AnimalExists(model, id))
			{
				ModelState.AddModelError(nameof(model), ErrorMessages.AnimalExistsError);
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			int newId = await animalService.AddNewAnimal(model, id);

			return RedirectToAction("Details", new { id = newId });
		}

		[HttpGet]
		//[Route("Animal/Details/{animalId:int}")]
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

			AnimalFormModel model = await animalService.GetAnimalForm(id);

			model.AnimalTypeId = await animalService.GetAnimalTypeId(id);

			model.AnimalTypes = await animalService.AllAnimalTypes();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, AnimalFormModel model)
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
				return View(model);
			}

			await animalService.EditAnimal(id, model);

			return RedirectToAction("Details", new { id });
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
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

			return RedirectToAction(nameof(Details), nameof(AnimalOwnerController), new { Id = ownerId });
		}
	}
}
