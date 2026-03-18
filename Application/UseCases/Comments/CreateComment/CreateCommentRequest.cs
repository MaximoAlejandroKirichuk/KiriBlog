namespace Application.UseCases.Comments.CreateComment;

public class CreateCommentRequest
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public string Content { get; set; }
}