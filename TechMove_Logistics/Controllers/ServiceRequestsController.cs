using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using TechMove_Logistics.ViewModels;

namespace TechMove_Logistics.Controllers
{
    public class ServiceRequestsController : Controller
    {
        private readonly HttpClient _httpClient;

        public ServiceRequestsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: ServiceRequests
        public async Task<IActionResult> Index()
        {
            var requests = await _httpClient.GetFromJsonAsync<List<ServiceRequestViewModel>>("https://localhost:5001/api/servicerequests");
            return View(requests);
        }

        // GET: ServiceRequests/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var request = await _httpClient.GetFromJsonAsync<ServiceRequestViewModel>($"https://localhost:5001/api/servicerequests/{id}");
            if (request == null) return NotFound();
            return View(request);
        }

        // POST: ServiceRequests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceRequestViewModel request)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:5001/api/servicerequests", request);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error creating service request");
            return View(request);
        }

        // POST: ServiceRequests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ServiceRequestViewModel request)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:5001/api/servicerequests/{id}", request);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error editing service request");
            return View(request);
        }

        // POST: ServiceRequests/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:5001/api/servicerequests/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return NotFound();
        }

        // GET: ServiceRequests/GetRate
        public async Task<IActionResult> GetRate()
        {
            var rate = await _httpClient.GetFromJsonAsync<decimal>("https://localhost:5001/api/servicerequests/rate");
            return Json(rate);
        }
    }
}
