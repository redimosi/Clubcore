using Clubcore.Domain.Models;

namespace Clubcore.Domain.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonDto>> GetAllPersonsAsync();
        Task<PersonDto?> GetPersonByIdAsync(Guid personId);
        Task AddPersonAsync(PersonDto personDto);
        Task UpdatePersonAsync(PersonDto personDto);
        Task DeletePersonAsync(Guid personId);
    }
}
