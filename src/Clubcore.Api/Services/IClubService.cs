using Clubcore.Api.Models;

namespace Clubcore.Api.Services
{
    public interface IClubService
    {
        Task AddGroupToClub(Guid id, GroupDto groupDto);
        Task<ClubDto> CreateClub(ClubDto clubDto);
        Task DeleteClub(Guid id);
        Task<ClubDto?> GetClub(Guid id);
        Task<IEnumerable<ClubDto>> GetClubs();
        Task UpdateClub(Guid id, ClubDto clubDto);
    }
}