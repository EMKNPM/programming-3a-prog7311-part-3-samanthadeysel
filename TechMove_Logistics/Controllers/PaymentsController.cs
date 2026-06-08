using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using TechMove_Logistics.ViewModels;

namespace TechMove_Logistics.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly HttpClient _httpClient;

        public PaymentsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var payments = await _httpClient.GetFromJsonAsync<List<PaymentViewModel>>("api/payments");
            return View(payments);
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var payment = await _httpClient.GetFromJsonAsync<PaymentViewModel>($"api/payments/{id}");
            if (payment == null) return NotFound();
            return View(payment);
        }

        // POST: Payments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentViewModel payment)
        {
            var response = await _httpClient.PostAsJsonAsync("api/payments", payment);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error creating payment");
            return View(payment);
        }

        // POST: Payments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PaymentViewModel payment)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/payments/{id}", payment);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error editing payment");
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/payments/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return NotFound();
        }
    }
}
