using Application.UseCases.Auth.Register.Dtos;

namespace Application.UseCases.Auth.Register;

public interface IRegisterUseCase
{
    Task<RegisterResponseDto> ExecuteAsync(RegisterRequestDto useCase);    
}