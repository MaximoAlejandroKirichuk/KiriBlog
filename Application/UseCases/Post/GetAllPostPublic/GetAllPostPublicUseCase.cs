using Domain.Interface.Repository;

namespace Application.UseCases.Post.GetAllPostPublic;

public class GetAllPostPublicUseCase : IGetAllPostPublicUseCase
{
    private readonly IPostRepository _postRepository;

    public GetAllPostPublicUseCase(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    public async Task<IEnumerable<Domain.Entities.Post>> ExecuteAsync()
    {
        var posts = await _postRepository.GetAllPublic(); ;
        return posts;
    }
}