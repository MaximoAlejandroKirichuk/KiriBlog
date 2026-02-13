using Application.UseCases.Post.GetAllPostPublic.Dtos;

namespace Application.UseCases.Post.GetAllPostPublic;

public interface IGetAllPostPublicUseCase
{
    Task<IEnumerable<PostListItemResponse>> ExecuteAsync();
}