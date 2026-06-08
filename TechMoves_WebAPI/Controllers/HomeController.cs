using Microsoft.AspNetCore.Mvc;

namespace TechMoves_WebAPI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
