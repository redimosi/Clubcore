using Clubcore.Domain.AggregatesModel;
using Clubcore.Domain.Models;
using Clubcore.Domain.Services;
using Clubcore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Clubcore.Api.Services
{
    public class PersonService(ClubcoreDbContext context) : IPersonService
    {
        public async Task<IEnumerable<PersonDto>> GetAllPersonsAsync()
        {
            var persons = await context.Persons.ToListAsync();
            return persons.Select(person => new PersonDto
            {
                PersonId = person.PersonId,
                FirstName = person.Details.FirstName,
                LastName = person.Details.LastName,
                MobileNr = person.Details.MobileNr
            }).ToList();
        }

        public async Task<PersonDto?> GetPersonByIdAsync(Guid personId)
        {
            var person = await context.Persons.FindAsync(personId);
            if (person == null) return null;

            return new PersonDto
            {
                PersonId = person.PersonId,
                FirstName = person.Details.FirstName,
                LastName = person.Details.LastName,
                MobileNr = person.Details.MobileNr
            };
        }

        public async Task AddPersonAsync(PersonDto personDto)
        {
            var person = new Person
            {
                PersonId = Guid.NewGuid(),
                Details = new PersonDetails
                {
                    FirstName = personDto.FirstName,
                    LastName = personDto.LastName,
                    MobileNr = personDto.MobileNr
                }
            };

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

            person.Details.FirstName = personDto.FirstName;
            person.Details.LastName = personDto.LastName;
            person.Details.MobileNr = personDto.MobileNr;

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
