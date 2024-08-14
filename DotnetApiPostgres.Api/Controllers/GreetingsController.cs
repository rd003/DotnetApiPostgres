using Microsoft.AspNetCore.Mvc;

namespace DotnetApiPostgres.Api.Controllers;

[ApiController]
[Route("/")]
public class GreetingsController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "Aapka swagat hai";
    }
}