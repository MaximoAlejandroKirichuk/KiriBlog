using Domain.Interface.Repository;

namespace Application.UseCases.Comments.GetCommentsByPost;

public class GetCommentsByPostUseCase : IGetCommentsByPostUseCase
{
    private readonly ICommentRepository _commentRepository;

    public GetCommentsByPostUseCase(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    public Task<ICollection<GetCommentsByPostResponse>> GetCommentsByPostAsync(GetCommentsByPostRequest request)
    {
        throw new NotImplementedException();
    }
}