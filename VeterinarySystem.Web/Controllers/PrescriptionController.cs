using Microsoft.AspNetCore.Mvc;
using VeterinarySystem.Core.Contracts;

namespace VeterinarySystem.Web.Controllers
{
	public class PrescriptionController : Controller
	{
		private readonly IAnimalService animalService;

		private readonly IPrescriptionService prescriptionService;

		public PrescriptionController(IPrescriptionService _prescriptionService, IAnimalService _animalService)
		{
			prescriptionService = _prescriptionService;
			animalService = _animalService;
		}

		[HttpGet]
		public IActionResult Add(int id)
		{

			return View();
		}
	}
}
