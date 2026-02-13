using Domain.Enums;
using Domain.Exceptions.Post;

namespace Domain.Entities;

public class Post
{
    public Guid Id { get; private set; }
    public Guid IdAuthor { get; private set; }
    public User Author { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public PostState PostState { get;private set; }
    public Visibility Visibility { get;private set; }
    public DateTime CreatedAt { get; private set; }
    public Language Language  { get; private set; }

    private Post(){} //EF

    public Post(Guid idAuthor, string title, string content, Visibility visibility,Language language)
    {
        Validation(title, content);
        Id = Guid.NewGuid();
        IdAuthor = idAuthor;
        Title = title;
        Content = content;
        PostState = PostState.Published;
        Visibility = visibility;
        CreatedAt = DateTime.UtcNow;
        Language = language;
    }
    public void Publish()
    {
        if (PostState != PostState.Published)
        {
            PostState = PostState.Published;
        }
    }

    private void Validation(string title, string content)
    {
        if(string.IsNullOrEmpty(title)) throw new PostValidationException("Post title is invalid");;
        if(string.IsNullOrEmpty(content)) throw new PostValidationException("Post content is invalid");
        
        if(title.Length > 80) throw new PostValidationException("Post title must be less than 80 characters");
        
    }
}