namespace Application.UseCases.Comments.ReplyToComment;

public interface IReplyToCommentUseCase
{
    Task<ReplyToCommentResponseDto> ExecuteAsync(Guid authenticatedUserId, ReplyToCommentRequestDto request);
}