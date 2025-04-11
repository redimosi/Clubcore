using Clubcore.Api.Models;
using Clubcore.Domain.AggregatesModel;
using Clubcore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Clubcore.Api.Services
{
    public class ClubService(ClubcoreDbContext context) : IClubService
    {
        public async Task<IEnumerable<ClubDto>> GetClubs()
        {
            return await context.Clubs
                .Include(c => c.Groups)
                .Select(c => new ClubDto
                {
                    ClubId = c.ClubId,
                    Name = c.Name,
                    Groups = c.Groups.Select(g => new GroupDto
                    {
                        GroupId = g.GroupId,
                        Name = g.Name
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<ClubDto?> GetClub(Guid id)
        {
            var club = await context.Clubs
                .Include(g => g.Groups)
                .FirstOrDefaultAsync(c => c.ClubId == id);

            if (club == null)
            {
                return null;
            }

            return new ClubDto
            {
                ClubId = club.ClubId,
                Name = club.Name,
                Groups = club.Groups.Select(g => new GroupDto
                {
                    GroupId = g.GroupId,
                    Name = g.Name
                }).ToList()
            };
        }

        public async Task UpdateClub(Guid id, ClubDto clubDto)
        {
            if (id != clubDto.ClubId)
            {
                throw new ArgumentException("ID mismatch");
            }

            var club = await context.Clubs.Include(c => c.Groups).FirstOrDefaultAsync(c => c.ClubId == id);
            if (club == null)
            {
                throw new KeyNotFoundException("Club not found");
            }

            club.Name = clubDto.Name;
            club.Groups = clubDto.Groups.Select(g => new Group
            {
                GroupId = g.GroupId,
                Name = g.Name
            }).ToList();

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
            var club = new Club
            {
                ClubId = clubDto.ClubId,
                Name = clubDto.Name,
                Groups = clubDto.Groups.Select(g => new Group
                {
                    GroupId = g.GroupId,
                    Name = g.Name
                }).ToList()
            };

            context.Clubs.Add(club);
            await context.SaveChangesAsync();

            return new ClubDto
            {
                ClubId = club.ClubId,
                Name = club.Name,
                Groups = club.Groups.Select(g => new GroupDto
                {
                    GroupId = g.GroupId,
                    Name = g.Name
                }).ToList()
            };
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
                group = new Group
                {
                    GroupId = groupDto.GroupId,
                    Name = groupDto.Name
                };
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

