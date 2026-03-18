namespace Application.UseCases.Comments.GetRepliesByCommentId;

//Response dto end of useCase
public class GetRepliesByCommentIdResponse
{
    public List<CommentReplyDto> Replies { get; set; } = new();
}