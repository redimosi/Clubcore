using Clubcore.Api.Models;

namespace Clubcore.Api.Services
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupDto>> GetGroups();
        Task<GroupDto?> GetGroup(Guid id);
        Task UpdateGroup(Guid id, GroupDto groupDto);
        Task<GroupDto> CreateGroup(GroupDto groupDto);
        Task DeleteGroup(Guid id);
    }
}

