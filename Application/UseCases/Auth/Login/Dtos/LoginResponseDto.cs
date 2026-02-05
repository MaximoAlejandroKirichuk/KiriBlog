namespace Application.UseCases.Auth.Login.Dtos;

public class LoginResponseDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; } 
    public string Email { get; set; }
    public string Role { get; set; }
    public string Token { get; set; }

    public LoginResponseDto(string email, string role, string token, Guid userId, string name, string roleName)
    {
        UserId = userId;
        Name = name;
        Role = role;
        Token = token;
        Email = email;
        Role = roleName;
    }

    public LoginResponseDto()
    {
        
    }


}
