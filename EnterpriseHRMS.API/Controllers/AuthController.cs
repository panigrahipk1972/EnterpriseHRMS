using EnterpriseHRMS.API.Models;
using EnterpriseHRMS.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseHRMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IJwtService _jwtService;

    public AuthController(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        // Demo credentials
        if (request.UserId != "admin" ||
            request.Password != "Admin@123")
        {
            return Unauthorized(new
            {
                status = 401,
                message = "Invalid user ID or password."
            });
        }

        var token = _jwtService.GenerateToken(
            request.UserId,
            "Admin");

        return Ok(new
        {
            token
        });
    }
}