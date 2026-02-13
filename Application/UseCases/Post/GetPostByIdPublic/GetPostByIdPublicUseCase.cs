using Application.UseCases.Post.GetPostByIdPublic.Dtos;
using Domain.Enums;
using Domain.Interface.Repository;

namespace Application.UseCases.Post.GetPostByIdPublic;

public class GetPostByIdPublicUseCase : IGetPostByIdPublicUseCase
{
    private readonly IPostRepository _postRepository;


    public GetPostByIdPublicUseCase(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<PostDetailResponse?> ExecuteAsync(Guid id)
    {
        var p = await _postRepository.GetEntityById(id);
        if (p == null) throw new ApplicationException("Post not found");
        return new PostDetailResponse(p.Id, p.Title, p.Content, p.CreatedAt,p.Language.ToString());
    }
}