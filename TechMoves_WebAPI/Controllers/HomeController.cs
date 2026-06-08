using Microsoft.AspNetCore.Mvc;

namespace TechMoves_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Routes this endpoint to https://localhost:7292/api/home
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("TechMoves Web API is running smoothly on local machine.");
        }
    }
}
