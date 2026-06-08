using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using TechMove_Logistics.ViewModels;

namespace TechMove_Logistics.Controllers
{
    [Authorize]
    public class ContractsController : Controller
    {
        private readonly HttpClient _httpClient;

        public ContractsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Contracts
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate, string status)
        {
            var url = "api/contracts";

            var queryParams = new List<string>();
            if (startDate.HasValue) queryParams.Add($"startDate={startDate.Value:O}");
            if (endDate.HasValue) queryParams.Add($"endDate={endDate.Value:O}");
            if (!string.IsNullOrEmpty(status)) queryParams.Add($"status={status}");

            if (queryParams.Any())
                url += "?" + string.Join("&", queryParams);

            var contracts = await _httpClient.GetFromJsonAsync<List<ContractViewModel>>(url);
            return View(contracts);
        }

        // GET: Contracts/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var contract = await _httpClient.GetFromJsonAsync<ContractViewModel>($"api/contracts/{id}");
            if (contract == null) return NotFound();
            return View(contract);
        }

        // GET: Contracts/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contracts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContractViewModel contract)
        {
            contract.Status = "Draft";
            var response = await _httpClient.PostAsJsonAsync("api/contracts", contract);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error creating contract");
            return View(contract);
        }

        // PATCH: Contracts/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ContractViewModel contract)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/contracts/{id}", contract);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error editing contract");
            return View(contract);
        }

        // DELETE: Contracts/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/contracts/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return NotFound();
        }
    }
}
