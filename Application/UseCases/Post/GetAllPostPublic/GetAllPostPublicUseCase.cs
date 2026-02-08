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
    public async Task<IEnumerable<PostResponse>> ExecuteAsync()
    {
        var posts = await _postRepository.GetAllPublic(); ;
        
        return posts.Select(post => new PostResponse(
            post.Id,
            post.IdAuthor,
            post.Title,
            post.Content,
            post.PostState.ToString(),
            post.Visibility.ToString(),
            post.CreatedAt,
            post.Language.ToString()
        ));
    }
}