using Microsoft.AspNetCore.Mvc;
using TechMove_Logistics.ViewModels;

namespace TechMove_Logistics.Controllers
{
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
            var clients = await _httpClient.GetFromJsonAsync<List<ClientViewModel>>("https://localhost:5001/api/clients");
            return View(clients);
        }

        // POST: Clients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientViewModel client)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:5001/api/clients", client);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error creating client");
            return View(client);
        }
    }
}