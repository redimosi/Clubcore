using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Clubcore.Api.Models;
using Clubcore.Api.Services;

namespace Clubcore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController(IClubService clubService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClubDto>>> GetClubs()
        {
            var clubs = await clubService.GetClubs();
            return Ok(clubs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClubDto>> GetClub(Guid id)
        {
            var club = await clubService.GetClub(id);

            if (club == null)
            {
                return NotFound();
            }

            return Ok(club);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutClub(Guid id, ClubDto clubDto)
        {
            try
            {
                await clubService.UpdateClub(id, clubDto);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ClubDto>> PostClub(ClubDto clubDto)
        {
            var createdClubDto = await clubService.CreateClub(clubDto);
            return CreatedAtAction("GetClub", new { id = createdClubDto.ClubId }, createdClubDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClub(Guid id)
        {
            try
            {
                await clubService.DeleteClub(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("{id}/groups")]
        public async Task<IActionResult> AddGroupToClub(Guid id, GroupDto groupDto)
        {
            try
            {
                await clubService.AddGroupToClub(id, groupDto);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }

            return NoContent();
        }
    }
}

