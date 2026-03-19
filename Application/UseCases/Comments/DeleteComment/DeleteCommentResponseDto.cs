namespace Application.UseCases.Comments.DeleteComment;

public class DeleteCommentResponseDto
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
}
