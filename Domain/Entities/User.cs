using Domain.Enums;

namespace Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string HashPassword { get; private set; } = string.Empty;
    public UserRole Role { get; private set; }

    public bool IsAdmin() => Role == UserRole.Admin;
    public bool IsAuthor() => Role == UserRole.Author;

    private User() { } //EF
    private User(string email, string name, string lastName, string hashPassword, UserRole role)
    {
        Id = Guid.NewGuid(); 
        Email = email;
        Name = name;
        LastName = lastName;
        HashPassword = hashPassword;
        Role = role; 
    }
    public static User CreateVisitor(string email, string name, string lastName, string hashPassword)
    {
        return new User(email, name, lastName, hashPassword, UserRole.Visitor);
    }
    public static User CreateAdmin(string email, string name, string lastName, string hashPassword)
    {
        return new User(email, name, lastName, hashPassword, UserRole.Admin);
    }
    public static User CreateAuthor(string email, string name, string lastName, string hashPassword)
    {
        return new User(email, name, lastName, hashPassword, UserRole.Author);
    }
}