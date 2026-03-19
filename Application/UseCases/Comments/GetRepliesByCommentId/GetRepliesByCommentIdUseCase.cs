using Domain.Exceptions.Comment;
using Domain.Interface.Repository;

namespace Application.UseCases.Comments.GetRepliesByCommentId;

public class GetRepliesByCommentIdUseCase : IGetRepliesByCommentIdUseCase
{
    private readonly ICommentRepository _commentRepository;

    public GetRepliesByCommentIdUseCase(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<GetRepliesByCommentIdResponse> ExecuteAsync(GetRepliesByCommentIdRequest request)
    {
        if (request.CommentId == Guid.Empty)
            throw new InvalidFormatCommentException("Comment id is required");

        var replies = await _commentRepository.GetRepliesByCommentId(request.CommentId);
        var replyIds = replies.Select(r => r.Id).ToList();

        var repliesCountByParentId = await _commentRepository.GetRepliesCountByParentIds(replyIds);

        return new GetRepliesByCommentIdResponse
        {
            Replies = replies.Select(reply => new CommentReplyDto
            {
                Id = reply.Id,
                Content = reply.Content,
                UserId = reply.UserId,
                CreatedAt = reply.CreatedAt,
                RepliesCount = repliesCountByParentId.GetValueOrDefault(reply.Id, 0)
            }).ToList()
        };
    }
}