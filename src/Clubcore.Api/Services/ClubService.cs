using AutoMapper;
using Clubcore.Api.Models;
using Clubcore.Domain.AggregatesModel;
using Clubcore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Clubcore.Api.Services
{
    public class ClubService(ClubcoreDbContext context, IMapper mapper) : IClubService
    {
        public async Task<IEnumerable<ClubDto>> GetClubs()
        {
            return await context.Clubs
                .Include(c => c.Groups)
                .Select(c => mapper.Map<ClubDto>(c))
                .ToListAsync();
        }

        public async Task<ClubDto?> GetClub(Guid id)
        {
            var club = await context.Clubs
                .Include(g => g.Groups)
                .FirstOrDefaultAsync(c => c.ClubId == id);

            return mapper.Map<ClubDto?>(club);
        }

        public async Task UpdateClub(Guid id, ClubDto clubDto)
        {
            if (id != clubDto.ClubId)
            {
                throw new ArgumentException("ID mismatch");
            }

            var club = mapper.Map<Club>(clubDto);
            context.Entry(club).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ClubExists(id))
                {
                    throw new KeyNotFoundException("Club not found");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<ClubDto> CreateClub(ClubDto clubDto)
        {
            var club = mapper.Map<Club>(clubDto);
            context.Clubs.Add(club);
            await context.SaveChangesAsync();

            return mapper.Map<ClubDto>(club);
        }

        public async Task DeleteClub(Guid id)
        {
            var club = await context.Clubs.FindAsync(id);
            if (club == null)
            {
                throw new KeyNotFoundException("Club not found");
            }

            context.Clubs.Remove(club);
            await context.SaveChangesAsync();
        }

        public async Task AddGroupToClub(Guid id, GroupDto groupDto)
        {
            var club = await context.Clubs.Include(c => c.Groups).FirstOrDefaultAsync(c => c.ClubId == id);
            if (club == null)
            {
                throw new KeyNotFoundException("Club not found");
            }

            var group = await context.Groups.FindAsync(groupDto.GroupId);
            if (group == null)
            {
                group = mapper.Map<Group>(groupDto);
            }

            club.Groups.Add(group);
            await context.SaveChangesAsync();
        }

        private async Task<bool> ClubExists(Guid id)
        {
            return await context.Clubs.AnyAsync(e => e.ClubId == id);
        }
    }
}

