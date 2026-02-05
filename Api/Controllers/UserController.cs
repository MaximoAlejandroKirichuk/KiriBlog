using Application.UseCases.Auth.Login;
using Application.UseCases.Auth.Login.Dtos;
using Domain.Interface.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[ApiController]
[Route("api/User")]

public class UserController : ControllerBase
{
    private readonly ILoginUseCase _loginUseCase;

    public UserController(ILoginUseCase  loginUseCase)
    {
        _loginUseCase = loginUseCase;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto dto)
    {
        var user = await _loginUseCase.Execute(dto);
        return Ok(user);
    }
    
    //todo: finish later
    //[HttpPost("register")]
    
}