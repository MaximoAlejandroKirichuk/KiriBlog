using Domain.Entities;
using Domain.Exceptions.Comment;
using Domain.Interface;
using Domain.Interface.Repository;

namespace Application.UseCases.Comments.CreateComment;

public class CreateCommentUseCase : ICreateCommentUseCase
{
    private readonly ICommentRepository _repo;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCommentUseCase(ICommentRepository repo, IUnitOfWork unitOfWork)
    {
        _repo = repo;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateCommentResponse> ExecuteAsync(Guid authenticatedUserId, CreateCommentRequest request)
    {
        ValidateRequest(request, authenticatedUserId);
        
        var comment = Comment.Create(
            request.Content.Trim(),
            request.PostId,
            authenticatedUserId
        );

        await _repo.CreateAsync(comment);
        await _unitOfWork.SaveChangesAsync();

        return new CreateCommentResponse
        {
            Id = comment.Id,
            Content = comment.Content,
            CreatedAt = comment.CreatedAt
        };
    }
    
    private static void ValidateRequest(CreateCommentRequest request, Guid authenticatedUserId)
    {
        if (authenticatedUserId == Guid.Empty)
            throw new InvalidFormatCommentException("User id is required");

        if (request.PostId == Guid.Empty)
            throw new InvalidFormatCommentException("Post id is required");

        if (string.IsNullOrWhiteSpace(request.Content))
            throw new InvalidFormatCommentException("Comment content cannot be empty");
    }
}