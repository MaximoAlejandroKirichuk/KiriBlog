using Application.UseCases.Auth.Login.Dtos;
namespace Application.UseCases.Auth.Login;

public interface ILoginUseCase
{
    Task<LoginResponseDto> Execute(LoginRequestDto user);

}