namespace Application.UseCases.Comments.GetRepliesByCommentId;

public class CommentReplyDto
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    // This property is used to determine if the comment has replies without needing to fetch them
    public int RepliesCount { get; set; }
    public bool HasReplies => RepliesCount > 0;
}