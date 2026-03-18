using Domain.Interface;
using Domain.Interface.Repository;

namespace Application.UseCases.Comments.ReplyToComment;

public class ReplyToCommentUseCase : IReplyToCommentUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICommentRepository _commentRepository;

    public ReplyToCommentUseCase(IUnitOfWork unitOfWork, ICommentRepository commentRepository)
    {
        _unitOfWork = unitOfWork;
        _commentRepository = commentRepository;
    }
    
    //TODO: Implement the logic to reply to a comment
    public Task<ReplyToCommentResponseDto> ExecuteAsync(ReplyToCommentRequestDto request)
    {
        throw new NotImplementedException();
    }
}