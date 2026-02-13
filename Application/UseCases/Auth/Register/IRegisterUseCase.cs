using Application.UseCases.Auth.Register.Dtos;

namespace Application.UseCases.Auth.Register;

public interface IRegisterVisitorUseCase
{
    Task<RegisterResponseDto> ExecuteAsync(RegisterRequestDto useCase);    
}