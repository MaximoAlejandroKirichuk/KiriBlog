namespace Application.UseCases.Comments.CreateComment;

public class CreateCommentResponse
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
}