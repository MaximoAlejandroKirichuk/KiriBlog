using Domain.Enums;

namespace Domain.Entities;

public class User
{
    public Guid IdUser { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string HashPassword { get; private set; } = string.Empty;
    public UserRole Role { get; private set; }

    public bool IsAdmin() => Role == UserRole.Admin;
    public bool IsAuthor() => Role == UserRole.Author;
}