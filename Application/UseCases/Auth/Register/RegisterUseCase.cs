using Application.Exceptions.Auth;
using Application.UseCases.Auth.Register.Dtos;
using Domain.Entities;
using Domain.Interface;
using Domain.Interface.Common.Security;
using Domain.Interface.Repository;

namespace Application.UseCases.Auth.Register;

public class RegisterVisitorUseCase : IRegisterVisitorUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    public RegisterVisitorUseCase(IUserRepository userRepository, IPasswordHasher passwordHasher,IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    public async Task<RegisterResponseDto> ExecuteAsync(RegisterRequestDto registerDto)
    {
        var existByEmail = await _userRepository.ExistByEmail(registerDto.Email);
        if (existByEmail) throw new EmailAlreadyExistException("Email already exists");
        
        var passwordHash = _passwordHasher.Hash(registerDto.Password);
        
        var newUserVisitor = User.CreateVisitor(
            registerDto.Email, 
            registerDto.Name, 
            registerDto.LastName, 
            passwordHash 
        );

        await _userRepository.RegisterAsync(newUserVisitor);
        await _unitOfWork.SaveChangesAsync();
        var token = _jwtTokenGenerator.Generate(newUserVisitor);

      
        return new RegisterResponseDto
        {
            Name = newUserVisitor.Name,
            LastName = newUserVisitor.LastName,
            Email = newUserVisitor.Email,
            Token = token
        };
    }
}