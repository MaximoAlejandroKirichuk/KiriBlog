namespace Application.UseCases.Post.GetAllPostPublic.Dtos;

public record PostResponse(
    Guid Id,
    Guid IdAuthor,
    string Title,
    string Content,
    string PostState, //return string instead of enum
    string Visibility,
    DateTime CreatedAt,
    string Language
);