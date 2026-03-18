namespace Application.UseCases.Comments.ReplyToComment;

public class ReplyToCommentRequestDto
{
    public Guid ParentCommentId { get; set; }
    public Guid UserId { get; set; }
    public string Content { get; set; } = string.Empty;
}