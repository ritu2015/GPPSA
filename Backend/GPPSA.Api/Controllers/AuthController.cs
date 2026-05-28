using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPPSA.Application.Dtos;
using GPPSA.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GPPSA.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _jwtService;

    public AuthController(JwtTokenService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequestDto dto)
    {
       
        // TEMP: hardcoded user (OK for interview demo)
        if (dto.Email != "admin@gppsa.com" || dto.Password != "Password@123")
            return Unauthorized("Invalid credentials");

        var userId = "0C97C596-3EC5-41C1-B50E-42678EF285AB"; // TEMP: hardcoded user ID (OK for interview demo)
        var token = _jwtService.GenerateToken(
            userId,
            dto.Email
        );

        return Ok(new { accessToken = token });
    }
        
    }
}