using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechMoves_WebAPI.Data;
using TechMoves_WebAPI.Models;

namespace TechMoves_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManagersController : ControllerBase
    {
        private readonly TechMoves_WebAPIContext _context;

        public ManagersController(TechMoves_WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/managers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manager>>> GetManagers()
        {
            return await _context.Managers.ToListAsync();
        }

        // GET: api/managers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Manager>> GetManager(int id)
        {
            var manager = await _context.Managers.FindAsync(id);
            if (manager == null) return NotFound();
            return manager;
        }

        // POST: api/managers
        [HttpPost]
        public async Task<ActionResult<Manager>> CreateManager(Manager manager)
        {
            _context.Managers.Add(manager);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetManager), new { id = manager.ManagerId }, manager);
        }

        // PUT: api/managers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateManager(int id, Manager manager)
        {
            if (id != manager.ManagerId) return BadRequest();

            _context.Entry(manager).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/managers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManager(int id)
        {
            var manager = await _context.Managers.FindAsync(id);
            if (manager == null) return NotFound();

            _context.Managers.Remove(manager);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
