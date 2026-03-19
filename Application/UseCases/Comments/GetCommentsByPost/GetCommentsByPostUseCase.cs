using Domain.Exceptions.Comment;
using Domain.Interface.Repository;

namespace Application.UseCases.Comments.GetCommentsByPost;

public class GetCommentsByPostUseCase : IGetCommentsByPostUseCase
{
    private readonly ICommentRepository _commentRepository;

    public GetCommentsByPostUseCase(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<GetCommentsByPostResponse> ExecuteAsync(GetCommentsByPostRequest request)
    {
        if (request.PostId == Guid.Empty)
            throw new InvalidFormatCommentException("Post id is required");

        var comments = await _commentRepository.GetCommentsByPost(request.PostId);
        var commentIds = comments.Select(c => c.Id).ToList();

        var repliesCountByParentId = await _commentRepository.GetRepliesCountByParentIds(commentIds);

        return new GetCommentsByPostResponse
        {
            Comments = comments.Select(comment => new CommentDto
            {
                Id = comment.Id,
                Content = comment.Content,
                UserId = comment.UserId,
                CreatedAt = comment.CreatedAt,
                RepliesCount = repliesCountByParentId.GetValueOrDefault(comment.Id, 0)
            }).ToList()
        };
    }
}