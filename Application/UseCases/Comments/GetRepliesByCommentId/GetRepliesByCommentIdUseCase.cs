using Domain.Interface.Repository;

namespace Application.UseCases.Comments.GetRepliesByCommentId;

public class GetRepliesByCommentIdUseCase : IGetRepliesByCommentIdUseCase
{
    private readonly ICommentRepository _commentaryRepository;

    public GetRepliesByCommentIdUseCase(ICommentRepository commentaryRepository)
    {
        _commentaryRepository = commentaryRepository;
    }
    
    public async Task<GetRepliesByCommentIdResponse> ExecuteAsync(GetRepliesByCommentIdRequest request)
    {
        var replies = await _commentaryRepository.GetRepliesByCommentId(request.CommentId);
        var replyIds = replies.Select(r => r.Id).ToList();

        var repliesCountByParentId = await _commentaryRepository.GetRepliesCountByParentIds(replyIds);

        var response = new GetRepliesByCommentIdResponse
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

        return response;
    }
}