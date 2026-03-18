using Domain.Interface.Repository;

namespace Application.UseCases.Comments.GetRepliesByCommentId;

public class GetRepliesByCommentIdUseCase : IGetRepliesByCommentIdUseCase
{
    private readonly ICommentRepository _commentaryRepository;

    public GetRepliesByCommentIdUseCase(ICommentRepository commentaryRepository)
    {
        _commentaryRepository = commentaryRepository;
    }
    
    public Task<GetRepliesByCommentIdResponse> ExecuteAsync(GetRepliesByCommentIdRequest request)
    {
        throw new NotImplementedException();
    }
}