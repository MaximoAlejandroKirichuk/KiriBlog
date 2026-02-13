using Application.UseCases.Post.GetAllPostPublic.Dtos;
using Domain.Interface.Repository;

namespace Application.UseCases.Post.GetAllPostPublic;

public class GetAllPostPublicUseCase : IGetAllPostPublicUseCase
{
    private readonly IPostRepository _postRepository;

    public GetAllPostPublicUseCase(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    public async Task<IEnumerable<PostListItemResponse>> ExecuteAsync()
    {
        var posts = await _postRepository.GetAllPublic();

        return posts.Select(post => new PostListItemResponse(
            post.Id,
            post.Title,
            post.Content.Length > 200
                ? post.Content.Substring(0, 200) + "..."
                : post.Content,
            post.CreatedAt
        ));
    }

}