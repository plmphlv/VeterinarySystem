using Microsoft.AspNetCore.Mvc;

namespace VeterinarySystem.Web.Controllers
{
	public class ProcedureController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
