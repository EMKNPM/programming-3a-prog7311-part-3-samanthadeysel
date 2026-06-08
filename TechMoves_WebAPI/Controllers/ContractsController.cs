using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechMoves_WebAPI.Data;
using TechMoves_WebAPI.Factory;
using TechMoves_WebAPI.Models;
using TechMoves_WebAPI.Observer;

namespace TechMoves_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractsController : ControllerBase
    {
        private readonly TechMoves_WebAPIContext _context;
        private readonly List<IContractObserver> _observers = new();

        public ContractsController(TechMoves_WebAPIContext context)
        {
            _context = context;

            _observers.Add(new ComplianceModule());
            _observers.Add(new ServiceRequestModule());
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

        [HttpPost("create-by-type")]
        public async Task<ActionResult<Contract>> CreateContractByType([FromQuery] string contractType, [FromQuery] int clientId)
        {
            IContractFactory factory;

            switch (contractType.ToLower())
            {
                case "driver":
                    factory = new DriverContractFactory();
                    break;
                case "freight":
                    factory = new FreightContractFactory();
                    break;
                default:
                    return BadRequest("Invalid or unsupported contract type selection.");
            }

            Contract newContract = factory.CreateContract();
            newContract.ClientId = clientId;
            newContract.StartDate = DateTime.UtcNow;
            newContract.EndDate = DateTime.UtcNow.AddYears(1);

            _context.Contracts.Add(newContract);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContract", new { id = newContract.ContractId }, newContract);
        }

        // PUT: api/contracts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContract(int id, Contract contract)
        {
            if (id != contract.ContractId) return BadRequest();

            try
            {
                // 📢 Notify all observers BEFORE saving changes to allow compliance checks to run
                foreach (var observer in _observers)
                {
                    observer.Update(contract);
                }
            }
            catch (InvalidOperationException ex)
            {
                // If the ComplianceModule throws an error due to a missing agreement path, block the save!
                return BadRequest(ex.Message);
            }

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
