using Domain.Entities;
using Domain.Interface.Repository;

namespace Application.UseCases.Comments;

public interface ICommentService
{
    Task<Comment> CreateComment(Guid postId, Guid userId, string content);

    Task<Comment> ReplyToComment(Guid parentCommentId, Guid userId, string content);

    Task DeleteComment(Guid commentId, Guid userId);

    Task<List<Comment>> GetCommentsByPost(Guid postId);
}