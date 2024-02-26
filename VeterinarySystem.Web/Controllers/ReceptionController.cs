using Microsoft.AspNetCore.Mvc;

namespace VeterinarySystem.Web.Controllers
{
    public class ReceptionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
