using ApiBackend.Authentication;
using ApiBackend.Context;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace ApiBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{

    // bu kısma hangi soyutlama gelecek
    private readonly AuthServices _authServices;

    public AuthController(AuthServices authServices)
    {
        _authServices = authServices;
    }


    [HttpPost("RegisterUser")]
    public async Task<IActionResult> RegisterUser(RegisterUser registerUser)
    {
        var result = await _authServices.Register(registerUser);
        if (result.Length > 0)
        {
            return Ok(result);
        }
        return BadRequest();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginUser loginUser)
    {
        var result = await _authServices.Login(loginUser);
        if (result.Id != 0)
        {
            return Ok(result);

        }
        return BadRequest();

    }



}