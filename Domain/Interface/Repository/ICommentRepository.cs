using Domain.Entities;

namespace Domain.Interface.Repository;

public interface ICommentRepository : IRepository<Comment, Guid>
{
    Task<List<Comment>> GetCommentsByPost(Guid postId);
    Task<List<Comment>> GetRepliesByCommentId(Guid commentId);
    
}