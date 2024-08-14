using DotnetApiPostgres.Api.Models;
using DotnetApiPostgres.Api.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace DotnetApiPostgres.Api.Services;

public interface IPersonService
{
    Task<GetPersonDto> AddPersonAsync(CreatePersonDTO personToCreate);
    Task UpdatePersonAsync(UpdatePersonDTO personToUpdate);
    Task DeletePersonAsync(Person person);
    Task<GetPersonDto?> FindPersonByIdAsync(int id);
    Task<IEnumerable<GetPersonDto>> GetPeopleAsync();
}
public sealed class PersonService : IPersonService
{
    private readonly ApplicationDbContext _context;

    public PersonService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetPersonDto> AddPersonAsync(CreatePersonDTO personToCreate)
    {
        Person person = CreatePersonDTO.ToPerson(personToCreate);
        _context.People.Add(person);
        await _context.SaveChangesAsync();
        return Person.ToGetPersonDto(person);
    }

    public async Task DeletePersonAsync(Person person)
    {
        _context.People.Remove(person);
        await _context.SaveChangesAsync();
    }

    public async Task<GetPersonDto?> FindPersonByIdAsync(int id)
    {
        Person? person = await _context.People.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        if (person == null)
        {
            return null;
        }
        return Person.ToGetPersonDto(person);
    }

    public async Task<IEnumerable<GetPersonDto>> GetPeopleAsync()
    {
        IEnumerable<Person> people = await _context.People.AsNoTracking().ToListAsync();
        return people.Select(Person.ToGetPersonDto);
    }

    public async Task UpdatePersonAsync(UpdatePersonDTO personToUpdate)
    {
        Person person = UpdatePersonDTO.ToPerson(personToUpdate);
        _context.People.Update(person);
        await _context.SaveChangesAsync();
    }
}
