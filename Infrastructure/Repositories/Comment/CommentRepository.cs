using Domain.Interface.Repository;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Comment;

public class CommentRepository: ICommentRepository
{
    private readonly ApplicationDbContext _context;

    public CommentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Domain.Entities.Comment>> GetAll()
    {
        return await _context.Comments
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Domain.Entities.Comment?> GetEntityById(Guid id)
    {
        return await _context.Comments
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task CreateAsync(Domain.Entities.Comment comment)
    {
         await _context.AddAsync(comment);
    }

    public Task UpdateAsync(Domain.Entities.Comment comment)
    {
        _context.Update(comment);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Domain.Entities.Comment post)
    {
        _context.Remove(post);
        return Task.CompletedTask;
    }

    public async Task<List<Domain.Entities.Comment>> GetCommentsByPost(Guid postId)
    {
        return await _context.Comments
            .AsNoTracking()
            .Include(c => c.User)
            .Where(c => c.PostId == postId
                        && c.ParentCommentId == null
                        && !c.IsDeleted)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<Domain.Entities.Comment>> GetRepliesByCommentId(Guid commentId)
    {
        return await _context.Comments
            .AsNoTracking()
            .Include(c => c.User)
            .Where(c => c.ParentCommentId == commentId && !c.IsDeleted)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task<Dictionary<Guid, int>> GetRepliesCountByParentIds(List<Guid> parentIds)
    {
        if (parentIds.Count == 0)
        {
            return new Dictionary<Guid, int>();
        }

        return await _context.Comments
            .AsNoTracking()
            .Where(c => c.ParentCommentId.HasValue
                        && parentIds.Contains(c.ParentCommentId.Value)
                        && !c.IsDeleted)
            .GroupBy(c => c.ParentCommentId!.Value)
            .Select(g => new
            {
                ParentCommentId = g.Key,
                RepliesCount = g.Count()
            })
            .ToDictionaryAsync(x => x.ParentCommentId, x => x.RepliesCount);
    }
}