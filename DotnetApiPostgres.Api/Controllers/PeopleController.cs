using DotnetApiPostgres.Api.Models.DTO;
using DotnetApiPostgres.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotnetApiPostgres.Api.Models;

[Route("api/people")]
[ApiController]
public class PeopleController : ControllerBase
{
    private readonly IPersonService _personService;
    private readonly ILogger<PeopleController> _logger;

    public PeopleController(IPersonService personService, ILogger<PeopleController> logger)
    {
        _personService = personService;
        _logger = logger;
    }
    [HttpPost]
    public async Task<IActionResult> AddPersonAsync(CreatePersonDTO personToCreate)
    {
        try
        {
            var person = await _personService.AddPersonAsync(personToCreate);
            return Ok(person);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePersonAsync(int id, UpdatePersonDTO personToUpdate)
    {
        if (id != personToUpdate.Id)
        {
            return BadRequest($"id in parameter and id in body is different. id in parameter: {id}, id in body: {personToUpdate.Id}");
        }
        try
        {
            GetPersonDto? person = await _personService.FindPersonByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            await _personService.UpdatePersonAsync(personToUpdate);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPersonByIdAsync(int id)
    {
        try
        {
            GetPersonDto? person = await _personService.FindPersonByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetPeopleAsync()
    {
        try
        {
            IEnumerable<GetPersonDto> peoples = await _personService.GetPeopleAsync();
            return Ok(peoples);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteByIdAsync(int id)
    {
        try
        {
            GetPersonDto? person = await _personService.FindPersonByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            await _personService.DeletePersonAsync(GetPersonDto.ToPerson(person));
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}