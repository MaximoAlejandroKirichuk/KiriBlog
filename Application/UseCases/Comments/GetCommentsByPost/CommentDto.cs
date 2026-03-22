namespace Application.UseCases.Comments.GetCommentsByPost;

public class CommentDto
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int RepliesCount { get; set; }
}
