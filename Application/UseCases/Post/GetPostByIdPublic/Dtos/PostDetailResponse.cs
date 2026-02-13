namespace Application.UseCases.Post.GetPostByIdPublic.Dtos;

public record PostDetailResponse(
    Guid Id,
    string Title,
    string Content,
    DateTime CreatedAt,
    string Language
);