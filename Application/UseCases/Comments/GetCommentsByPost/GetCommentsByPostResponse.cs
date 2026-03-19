namespace Application.UseCases.Comments.GetCommentsByPost;

public class GetCommentsByPostResponse
{
    public List<CommentDto> Comments { get; set; } = new();
}