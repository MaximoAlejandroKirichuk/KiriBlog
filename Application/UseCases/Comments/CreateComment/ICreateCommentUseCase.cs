namespace Application.UseCases.Comments.CreateComment;

public interface ICreateCommentUseCase
{
    Task<CreateCommentResponse> ExecuteAsync(CreateCommentRequest request);
}