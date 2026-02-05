namespace Application.UseCases.Post.GetAllPostPublic;

public interface IGetAllPostPublicUseCase
{
    Task<IEnumerable<Domain.Entities.Post>> ExecuteAsync();
}