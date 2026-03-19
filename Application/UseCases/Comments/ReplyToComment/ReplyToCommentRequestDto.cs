namespace Application.UseCases.Comments.ReplyToComment;

public class ReplyToCommentRequestDto
{
    public Guid ParentCommentId { get; set; }
    public string Content { get; set; } = string.Empty;
}