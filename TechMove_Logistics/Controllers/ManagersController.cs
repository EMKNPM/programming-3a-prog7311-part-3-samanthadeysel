using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using TechMove_Logistics.ViewModels;

namespace TechMove_Logistics.Controllers
{
    public class ManagersController : Controller
    {
        private readonly HttpClient _httpClient;

        public ManagersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Managers
        public async Task<IActionResult> Index()
        {
            var managers = await _httpClient.GetFromJsonAsync<List<ManagerViewModel>>("https://localhost:5001/api/managers");
            return View(managers);
        }

        // GET: Managers/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var manager = await _httpClient.GetFromJsonAsync<ManagerViewModel>($"https://localhost:5001/api/managers/{id}");
            if (manager == null) return NotFound();
            return View(manager);
        }

        // POST: Managers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ManagerViewModel manager)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:5001/api/managers", manager);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error creating manager");
            return View(manager);
        }

        // POST: Managers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ManagerViewModel manager)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:5001/api/managers/{id}", manager);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error editing manager");
            return View(manager);
        }

        // POST: Managers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:5001/api/managers/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return NotFound();
        }
    }
}
