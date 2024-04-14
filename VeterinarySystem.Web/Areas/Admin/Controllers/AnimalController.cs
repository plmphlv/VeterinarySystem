using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeterinarySystem.Core.Contracts;
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
	}
}
