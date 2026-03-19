﻿namespace Application.UseCases.Comments.CreateComment;

public interface ICreateCommentUseCase
{
    Task<CreateCommentResponse> ExecuteAsync(Guid authenticatedUserId, CreateCommentRequest request);
}