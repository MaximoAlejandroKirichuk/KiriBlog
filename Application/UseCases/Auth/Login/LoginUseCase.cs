using Application.Exceptions.Auth;
using Application.UseCases.Auth.Login.Dtos;
using Domain.Interface.Common.Security;
using Domain.Interface.Repository;

namespace Application.UseCases.Auth.Login;

public class LoginUseCase : ILoginUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginUseCase(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    public async Task<LoginResponseDto> Execute(LoginRequestDto userDto)
    {
        Validate(userDto.Email, userDto.Password);
        
        var user = await _userRepository.GetByEmailAsync(userDto.Email);
        if (user is null)
            throw new UnauthorizedAccessException("Invalid credentials");

        if (!_passwordHasher.Verify(userDto.Password, user.HashPassword))
            throw new UnauthorizedAccessException("Invalid credentials");
        
        var token = _jwtTokenGenerator.Generate(user);
        
        return new LoginResponseDto
        {
            UserId = user.IdUser,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role.ToString(),
            Token = token
        };
        
    }

    private static void Validate(string email, string password)
    {
        if(string.IsNullOrEmpty(password)) throw new InvalidCredentialsException("Password is required");
        if(string.IsNullOrEmpty(email)) throw new InvalidCredentialsException("Email is required");
    }
}