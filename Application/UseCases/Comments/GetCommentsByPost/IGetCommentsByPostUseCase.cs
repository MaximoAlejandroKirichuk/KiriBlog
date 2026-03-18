namespace Application.UseCases.Comments.GetCommentsByPost;

public interface IGetCommentsByPostUseCase
{
    Task<ICollection<GetCommentsByPostResponse>> GetCommentsByPostAsync(GetCommentsByPostRequest request);
}