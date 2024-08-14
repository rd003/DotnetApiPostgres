using System.ComponentModel.DataAnnotations;

namespace DotnetApiPostgres.Api.Models.DTO;

public sealed class CreatePersonDTO
{
    [Required]
    public required string Name { get; set; }

    public static Person ToPerson(CreatePersonDTO createPersonDto)
    {
        return new Person
        {
            Name = createPersonDto.Name
        };
    }


}