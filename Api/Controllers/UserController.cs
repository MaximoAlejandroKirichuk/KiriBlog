using Application.UseCases.Auth.Login;
using Application.UseCases.Auth.Login.Dtos;
using Application.UseCases.Auth.Register;
using Application.UseCases.Auth.Register.Dtos;
using Domain.Interface.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[ApiController]
[Route("api/User")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly ILoginUseCase _loginUseCase;
    private readonly IRegisterUseCase _registerUseCase;

    public UserController(ILoginUseCase  loginUseCase, IRegisterUseCase registerUseCase)
    {
        _loginUseCase = loginUseCase;
        _registerUseCase = registerUseCase;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequestDto dto)
    {
        var user = await _loginUseCase.Execute(dto);
        return Ok(user);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterRequestDto dto)
    {
        var response = await _registerUseCase.ExecuteAsync(dto);
        return StatusCode(201,response);
    }
    
}