using System.ComponentModel.DataAnnotations;

namespace DotnetApiPostgres.Api.Models.DTO;

public class UpdatePersonDTO
{
    public int Id { get; set; }
    [Required]
    public required string Name { get; set; }

    public static Person ToPerson(UpdatePersonDTO updatePersonDto)
    {
        return new Person
        {
            Id = updatePersonDto.Id,
            Name = updatePersonDto.Name
        };
    }
}