using Application.UseCases.Post.GetPostByIdPublic.Dtos;

namespace Application.UseCases.Post.GetPostByIdPublic;

public interface IGetPostByIdPublicUseCase
{
    Task<PostDetailResponse?> ExecuteAsync(Guid id);
}