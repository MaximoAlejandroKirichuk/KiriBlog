using Domain.Enums;

namespace Application.UseCases.Post.CreatePost.Dtos;

public class CreatePostRequest
{
    public string Email { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Visibility Visibility { get;set; }
    public Language Language  { get; set; }
}