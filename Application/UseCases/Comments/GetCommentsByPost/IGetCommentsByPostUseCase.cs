namespace Application.UseCases.Comments.GetCommentsByPost;

public interface IGetCommentsByPostUseCase
{
    Task<GetCommentsByPostResponse> ExecuteAsync(GetCommentsByPostRequest request);
}