using AutoMapper;
using Clubcore.Domain.AggregatesModel;
using Clubcore.Domain.Models;
using Clubcore.Domain.Services;
using Clubcore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Clubcore.Api.Services
{
    public class PersonService(ClubcoreDbContext context, IMapper mapper) : IPersonService
    {
        public async Task<IEnumerable<PersonDto>> GetAllPersonsAsync()
        {
            var persons = await context.Persons.ToListAsync();
            return mapper.Map<List<PersonDto>>(persons);
        }

        public async Task<PersonDto?> GetPersonByIdAsync(Guid personId)
        {
            var person = await context.Persons.FindAsync(personId);
            return mapper.Map<PersonDto?>(person);
        }

        public async Task AddPersonAsync(PersonDto personDto)
        {
            var person = mapper.Map<Person>(personDto);
            person.PersonId = Guid.NewGuid();
            context.Persons.Add(person);
            await context.SaveChangesAsync();
        }

        public async Task UpdatePersonAsync(PersonDto personDto)
        {
            var person = await context.Persons.FindAsync(personDto.PersonId);
            if (person == null)
            {
                throw new KeyNotFoundException("Person not found");
            }

            mapper.Map(personDto, person);
            context.Entry(person).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PersonExists(personDto.PersonId))
                {
                    throw new KeyNotFoundException("Person not found");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeletePersonAsync(Guid personId)
        {
            var person = await context.Persons.FindAsync(personId);
            if (person == null)
            {
                throw new KeyNotFoundException("Person not found");
            }

            context.Persons.Remove(person);
            await context.SaveChangesAsync();
        }

        private async Task<bool> PersonExists(Guid id)
        {
            return await context.Persons.AnyAsync(e => e.PersonId == id);
        }
    }
}
