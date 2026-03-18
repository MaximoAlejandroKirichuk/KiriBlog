using Domain.Entities;
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

    public async Task<CreateCommentResponse> ExecuteAsync(CreateCommentRequest request)
    {
        var comment = Comment.Create(
            request.Content,
            request.PostId,
            request.UserId
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
}