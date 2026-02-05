using Domain.Enums;
using Domain.Interface.Repository;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Post;

public class PostRepository : IPostRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PostRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<Domain.Entities.Post>> GetAll()
    {
        return await _dbContext.Posts
            .AsNoTracking()
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<Domain.Entities.Post?> GetEntityById(Guid idPost)
    {
        return await _dbContext.Posts.FirstOrDefaultAsync(p => p.IdPost == idPost);
    }

    public async Task CreateAsync(Domain.Entities.Post post)
    { 
        await _dbContext.Posts.AddAsync(post);
    }
    
    public Task UpdateAsync(Domain.Entities.Post post)
    {
        _dbContext.Posts.Update(post);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Domain.Entities.Post post)
    {
        _dbContext.Posts.Remove(post);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistByTitle(string title)
    {
        return await _dbContext.Posts.AnyAsync(p => p.Title == title);
    }

    public async Task<IEnumerable<Domain.Entities.Post>> GetAllPublic()
    {
        return await _dbContext.Posts
                    .AsNoTracking()
                    .Where(p => p.Visibility == Visibility.Public)
                    .OrderByDescending(p => p.CreatedAt)
                    .ToListAsync();
    }
}