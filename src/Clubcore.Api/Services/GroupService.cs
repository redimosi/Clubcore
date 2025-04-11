using Clubcore.Api.Models;
using Clubcore.Domain.AggregatesModel;
using Clubcore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Clubcore.Api.Services
{
    public class GroupService(ClubcoreDbContext context) : IGroupService
    {
        public async Task<IEnumerable<GroupDto>> GetGroups()
        {
            var groups = await context.Groups.ToListAsync();
            return groups.Select(g => new GroupDto
            {
                GroupId = g.GroupId,
                Name = g.Name
            }).ToList();
        }

        public async Task<GroupDto?> GetGroup(Guid id)
        {
            var group = await context.Groups.FindAsync(id);
            if (group == null) return null;

            return new GroupDto
            {
                GroupId = group.GroupId,
                Name = group.Name
            };
        }

        public async Task UpdateGroup(Guid id, GroupDto groupDto)
        {
            if (id != groupDto.GroupId)
            {
                throw new ArgumentException("ID mismatch");
            }

            var group = await context.Groups.FindAsync(id);
            if (group == null)
            {
                throw new KeyNotFoundException("Group not found");
            }

            group.Name = groupDto.Name;

            context.Entry(group).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await GroupExists(id))
                {
                    throw new KeyNotFoundException("Group not found");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<GroupDto> CreateGroup(GroupDto groupDto)
        {
            var group = new Group
            {
                GroupId = Guid.NewGuid(),
                Name = groupDto.Name
            };

            context.Groups.Add(group);
            await context.SaveChangesAsync();

            return new GroupDto
            {
                GroupId = group.GroupId,
                Name = group.Name
            };
        }

        public async Task DeleteGroup(Guid id)
        {
            var group = await context.Groups.FindAsync(id);
            if (group == null)
            {
                throw new KeyNotFoundException("Group not found");
            }

            context.Groups.Remove(group);
            await context.SaveChangesAsync();
        }

        private async Task<bool> GroupExists(Guid id)
        {
            return await context.Groups.AnyAsync(e => e.GroupId == id);
        }
    }
}

