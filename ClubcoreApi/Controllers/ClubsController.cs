using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clubcore.Entities;
using Clubcore.Infrastructure;
using ClubcoreApi.Models;

namespace ClubcoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubsController : ControllerBase
    {
        private readonly ClubcoreDbContext _context;

        public ClubsController(ClubcoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Club>>> GetClubs()
        {
            return await _context.Clubs.Include(g=>g.Groups).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Club>> GetClub(Guid id)
        {
            var club = await _context.Clubs.FindAsync(id);

            if (club == null)
            {
                return NotFound();
            }

            return club;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutClub(Guid id, Club club)
        {
            if (id != club.ClubId)
            {
                return BadRequest();
            }

            _context.Entry(club).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClubExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Club>> PostClub(Club club)
        {
            _context.Clubs.Add(club);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClub", new { id = club.ClubId }, club);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClub(Guid id)
        {
            var club = await _context.Clubs.FindAsync(id);
            if (club == null)
            {
                return NotFound();
            }

            _context.Clubs.Remove(club);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id}/groups")]
        public async Task<IActionResult> AddGroupToClub(Guid id, AddGroupToClubDto dto)
        {
            var club = await _context.Clubs.Include(c => c.Groups).FirstOrDefaultAsync(c => c.ClubId == id);
            if (club == null)
            {
                return NotFound();
            }

            var group = await _context.Groups.FindAsync(dto.GroupId);
            if (group == null)
            {
                return NotFound();
            }

            club.Groups.Add(group);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool ClubExists(Guid id)
        {
            return _context.Clubs.Any(e => e.ClubId == id);
        }
    }
}
