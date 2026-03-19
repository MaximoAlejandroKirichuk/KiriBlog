using Application.Exceptions.Comment;
using Domain.Entities;
using Domain.Exceptions.Comment;
using Domain.Interface;
using Domain.Interface.Repository;

namespace Application.UseCases.Comments.DeleteComment;

public class DeleteCommentUseCase : IDeleteCommentUseCase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCommentUseCase(ICommentRepository commentRepository, IUnitOfWork unitOfWork)
    {
        _commentRepository = commentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteCommentResponseDto> ExecuteAsync(Guid authenticatedUserId, DeleteCommentRequestDto request)
    {
        ValidateRequest(authenticatedUserId, request);
        var comment = await _commentRepository.GetEntityById(request.CommentId);
        ValidateComment(comment, authenticatedUserId);
        comment.Delete();

        await _commentRepository.UpdateAsync(comment);
        await _unitOfWork.SaveChangesAsync();

        return new DeleteCommentResponseDto
        {
            Id = comment.Id,
            IsDeleted = comment.IsDeleted
        };
    }

    private static void ValidateRequest(Guid authenticatedUserId, DeleteCommentRequestDto request)
    {
        if (authenticatedUserId == Guid.Empty)
            throw new InvalidFormatCommentException("User id is required");

        if (request.CommentId == Guid.Empty)
            throw new InvalidFormatCommentException("Comment id is required");
    }
    private static void ValidateComment(Comment comment, Guid authenticatedUserId)
    {
        if (comment is null)
            throw new CommentNotFoundException("Comment not found");

        if (comment.IsDeleted)
            throw new CommentAlreadyDeletedException("Comment is already deleted");

        if (comment.UserId != authenticatedUserId)
            throw new CommentForbiddenException("You cannot delete this comment");
    }
    
}

