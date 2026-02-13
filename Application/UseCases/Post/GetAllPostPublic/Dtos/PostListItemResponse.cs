namespace Application.UseCases.Post.GetAllPostPublic.Dtos;


public record PostListItemResponse(
    Guid Id,
    string Title,
    string? Excerpt,
    DateTime CreatedAt
);
