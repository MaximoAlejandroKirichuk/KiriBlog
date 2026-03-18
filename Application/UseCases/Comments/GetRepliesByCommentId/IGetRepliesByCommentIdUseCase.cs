namespace Application.UseCases.Comments.GetRepliesByCommentId;

public interface IGetRepliesByCommentIdUseCase
{
    Task<GetRepliesByCommentIdResponse> ExecuteAsync(GetRepliesByCommentIdRequest request);
}