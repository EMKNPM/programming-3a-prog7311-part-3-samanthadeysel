using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechMoves_WebAPI.Data;
using TechMoves_WebAPI.Models;
using TechMoves_WebAPI.Services;

namespace TechMoves_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceRequestsController : ControllerBase
    {
        private readonly TechMoves_WebAPIContext _context;
        private readonly CurrencyService _currencyService;

        public ServiceRequestsController(TechMoves_WebAPIContext context, CurrencyService currencyService)
        {
            _context = context;
            _currencyService = currencyService;
        }

        // GET: api/servicerequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceRequest>>> GetServiceRequests()
        {
            return await _context.ServiceRequests.Include(r => r.Contract).ToListAsync();
        }

        // GET: api/servicerequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceRequest>> GetServiceRequest(int id)
        {
            var request = await _context.ServiceRequests.Include(r => r.Contract)
                                                        .FirstOrDefaultAsync(r => r.ServiceRequestId == id);
            if (request == null) return NotFound();
            return request;
        }

        // POST: api/servicerequests
        [HttpPost]
        public async Task<ActionResult<ServiceRequest>> CreateServiceRequest(ServiceRequest request)
        {
            var contract = await _context.Contracts.FindAsync(request.ContractId);
            if (contract == null || contract.Status == "Expired" || contract.Status == "OnHold")
                return BadRequest("Cannot create ServiceRequest for expired or on-hold contracts.");

            // Convert USD→ZAR if provided
            if (request.CostUSD > 0)
            {
                var rate = await _currencyService.GetUsdToZarRateAsync();
                request.CostZAR = Math.Round(request.CostUSD * rate, 2);
            }

            _context.ServiceRequests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetServiceRequest), new { id = request.ServiceRequestId }, request);
        }

        // PUT: api/servicerequests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServiceRequest(int id, ServiceRequest request)
        {
            if (id != request.ServiceRequestId) return BadRequest();

            _context.Entry(request).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/servicerequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceRequest(int id)
        {
            var request = await _context.ServiceRequests.FindAsync(id);
            if (request == null) return NotFound();

            _context.ServiceRequests.Remove(request);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET: api/servicerequests/rate
        [HttpGet("rate")]
        public async Task<ActionResult<decimal>> GetRate()
        {
            var rate = await _currencyService.GetUsdToZarRateAsync();
            return rate;
        }
    }
}
