using Microsoft.AspNetCore.Mvc;
using VeterinarySystem.Core.Contracts;

namespace VeterinarySystem.Web.Controllers
{
	public class AnimalOwnerController : Controller
	{
		private readonly IAnimalOwnerService animaOwnerService;

		public AnimalOwnerController(IAnimalOwnerService _animaOwnerService) 
		{ 
			animaOwnerService = _animaOwnerService;
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
