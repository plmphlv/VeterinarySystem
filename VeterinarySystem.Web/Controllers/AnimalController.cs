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
				//AnimalOwnerId = ownerId,
				AnimalTypes = await animalService.AllAnimalTypes()
			};

			return View(model);
		}

		[HttpPost]
		[Route("Animal/Add/{ownerId:int}")]
		public async Task<IActionResult> Add(int ownerId, AnimalFormModel model)
		{
			if (!await ownerService.AnimalOwnerExists(ownerId))
			{
				return BadRequest();
			}

			if (await animalService.AnimalExists(model, ownerId))
			{
				ModelState.AddModelError(nameof(model), ErrorMessages.AnimalExistsError);
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			int id = await animalService.AddNewAnimal(model, ownerId);

			return RedirectToAction("Details", new { id });
		}

		[HttpGet]
		[Route("Animal/Details/{animalId:int}")]
		public async Task<IActionResult> Details(int animalId)
		{
			if (!await animalService.AnimalExists(animalId))
			{
				return BadRequest();
			}

			AnimalServiceModel model = await animalService.GetAnimalDetails(animalId);

			return View(model);
		}
	}
}
