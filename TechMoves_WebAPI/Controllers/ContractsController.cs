using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechMoves_WebAPI.Data;
using TechMoves_WebAPI.Models;

namespace TechMoves_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractsController : ControllerBase
    {
        private readonly TechMoves_WebAPIContext _context;

        public ContractsController(TechMoves_WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/contracts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contract>>> GetContracts(DateTime? startDate, DateTime? endDate, string? status)
        {
            var contracts = _context.Contracts.Include(c => c.Client).Include(c => c.ServiceRequests).AsQueryable();

            if (startDate.HasValue) contracts = contracts.Where(c => c.StartDate >= startDate.Value);
            if (endDate.HasValue) contracts = contracts.Where(c => c.EndDate <= endDate.Value);
            if (!string.IsNullOrEmpty(status)) contracts = contracts.Where(c => c.Status == status);

            return await contracts.ToListAsync();
        }

        // GET: api/contracts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contract>> GetContract(int id)
        {
            var contract = await _context.Contracts.Include(c => c.Client)
                                                   .FirstOrDefaultAsync(c => c.ContractId == id);
            if (contract == null) return NotFound();
            return contract;
        }

        // POST: api/contracts
        [HttpPost]
        public async Task<ActionResult<Contract>> CreateContract(Contract contract)
        {
            contract.Status = "Draft";
            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContract), new { id = contract.ContractId }, contract);
        }

        // PUT: api/contracts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContract(int id, Contract contract)
        {
            if (id != contract.ContractId) return BadRequest();

            _context.Entry(contract).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/contracts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContract(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null) return NotFound();

            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
