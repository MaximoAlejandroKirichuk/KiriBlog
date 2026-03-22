namespace Application.UseCases.Comments.GetRepliesByCommentId;

public class CommentReplyDto
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int RepliesCount { get; set; }
    public bool HasReplies => RepliesCount > 0;
}