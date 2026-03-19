﻿using Application.Exceptions.Comment;
using Domain.Entities;
using Domain.Exceptions.Comment;
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
    
    public async Task<ReplyToCommentResponseDto> ExecuteAsync(Guid authenticatedUserId, ReplyToCommentRequestDto request)
    {
        Validate(authenticatedUserId, request);

        var parentComment = await _commentRepository.GetEntityById(request.ParentCommentId);
        if (parentComment is null)
            throw new ParentCommentNotFoundException("Parent comment not found");

        if (parentComment.IsDeleted)
            throw new ParentCommentDeletedException("Parent comment is deleted");

        var reply = Comment.Create(
            request.Content.Trim(),
            parentComment.PostId,
            authenticatedUserId,
            parentComment.Id);

        await _commentRepository.CreateAsync(reply);
        await _unitOfWork.SaveChangesAsync();

        return new ReplyToCommentResponseDto
        {
            Id = reply.Id,
            ParentCommentId = parentComment.Id,
            PostId = reply.PostId,
            UserId = reply.UserId,
            Content = reply.Content,
            CreatedAt = reply.CreatedAt
        };
    }

    private static void Validate(Guid authenticatedUserId, ReplyToCommentRequestDto request)
    {
        if (request.ParentCommentId == Guid.Empty)
            throw new ParentCommentNotFoundException("Parent comment id is required");

        if (authenticatedUserId == Guid.Empty)
            throw new InvalidFormatCommentException("User id is required");

        if (string.IsNullOrWhiteSpace(request.Content))
            throw new InvalidFormatCommentException("Comment content cannot be empty");
    }
}