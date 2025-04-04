using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Clubcore.Api.Models;
using Clubcore.Api.Services;

namespace Clubcore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController(IGroupService groupService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetGroups()
        {
            var groups = await groupService.GetGroups();
            return Ok(groups);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupDto>> GetGroup(Guid id)
        {
            var group = await groupService.GetGroup(id);

            if (group == null)
            {
                return NotFound();
            }

            return Ok(group);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup(Guid id, GroupDto groupDto)
        {
            try
            {
                await groupService.UpdateGroup(id, groupDto);
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
        public async Task<ActionResult<GroupDto>> PostGroup(GroupDto groupDto)
        {
            var createdGroupDto = await groupService.CreateGroup(groupDto);
            return CreatedAtAction("GetGroup", new { id = createdGroupDto.GroupId }, createdGroupDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(Guid id)
        {
            try
            {
                await groupService.DeleteGroup(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

