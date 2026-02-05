using Domain.Entities;

namespace Domain.Interface.Repository;

public interface IPostRepository: IRepository<Post, Guid>
{
    public Task<bool> ExistByTitle(string title);
    public Task<IEnumerable<Post>> GetAllPublic();
}