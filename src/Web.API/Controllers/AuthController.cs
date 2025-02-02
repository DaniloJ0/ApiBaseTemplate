using Infrastructure.Interfacess;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IJwtTokenService jwtTokenService) : ApiController
{
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;

    [HttpGet("login")]
    public IActionResult Login()
    {
        var token = _jwtTokenService.GenerateToken("Prueba");
        return Ok(new { Token = token });
    }
}
