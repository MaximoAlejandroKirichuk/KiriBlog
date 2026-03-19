namespace Application.UseCases.Comments.DeleteComment;

public interface IDeleteCommentUseCase
{
    Task<DeleteCommentResponseDto> ExecuteAsync(Guid authenticatedUserId, DeleteCommentRequestDto request);
}

