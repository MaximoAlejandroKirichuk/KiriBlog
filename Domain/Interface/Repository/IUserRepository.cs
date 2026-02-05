using Domain.Entities;

namespace Domain.Interface.Repository;

public interface IUserRepository
{
    Task RegisterAsync(User user);
    Task<User?> GetByEmailAsync(string email);
}