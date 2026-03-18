using Domain.Exceptions.Comment;

namespace Domain.Entities;

public class Comment
{
    public Guid Id { get; private set; }
    public string Content { get; private set; } = string.Empty;
    
    public Guid PostId { get; private set; }
    public Post? Post { get; private set; }
    
    public Guid UserId { get; private set; }
    public User? User { get; set; }
    
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public bool IsDeleted { get; private set; } = false;
    public Guid? ParentCommentId { get; private set; }
    public Comment? ParentComment { get; private set; }

    public ICollection<Comment> Replies { get; private set; } = new List<Comment>();
    
    public static Comment Create(string content, Guid postId, Guid userId, Guid? parentId = null)
    {
        if(string.IsNullOrEmpty(content)) throw new InvalidFormatCommentException("Comment content cannot be empty");
        if (content.Length > 700) throw new InvalidFormatCommentException("Comment content must be less than");
        return new Comment
        {
            Id = Guid.NewGuid(),
            Content = content,
            PostId = postId,
            UserId = userId,
            ParentCommentId = parentId,
            CreatedAt = DateTime.UtcNow
        };
    }
    public void Delete()
    {
        IsDeleted = true;
    }
}