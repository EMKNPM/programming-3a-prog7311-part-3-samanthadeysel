using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechMove_Logistics.ViewModels;

namespace TechMove_Logistics.Controllers
{
    [Authorize] 
    public class ClientsController : Controller
    {
        private readonly HttpClient _httpClient;

        public ClientsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            // Automatically maps to https://localhost:7292/api/clients via relative pathing
            var clients = await _httpClient.GetFromJsonAsync<List<ClientViewModel>>("api/clients");
            return View(clients);
        }

        // GET: Clients/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientViewModel client)
        {
            // Automatically maps to https://localhost:7292/api/clients via relative pathing
            var response = await _httpClient.PostAsJsonAsync("api/clients", client);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error creating client");
            return View(client);
        }
    }
}
