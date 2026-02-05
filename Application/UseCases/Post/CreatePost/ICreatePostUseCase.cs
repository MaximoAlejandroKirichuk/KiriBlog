using Application.UseCases.Post.CreatePost.Dtos;

namespace Application.UseCases.Post.CreatePost;

public interface ICreatePostUseCase
{
    Task ExecuteAsync(CreatePostRequest createPostDto);
}