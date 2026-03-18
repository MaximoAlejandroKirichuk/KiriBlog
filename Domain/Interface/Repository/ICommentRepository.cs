using Domain.Entities;

namespace Domain.Interface.Repository;

public interface ICommentRepository : IRepository<Comment, Guid>
{
    Task<List<Comment>> GetCommentsByPost(Guid postId);
    Task<List<Comment>> GetRepliesByCommentId(Guid commentId);
    Task<Dictionary<Guid, int>> GetRepliesCountByParentIds(List<Guid> parentIds);
}