namespace DotnetApiPostgres.Api.Models.DTO;

public sealed class GetPersonDto
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public static Person ToPerson(GetPersonDto getPersonDto)
    {
        return new Person
        {
            Id = getPersonDto.Id,
            Name = getPersonDto.Name
        };
    }
}