using Api.Extensions;
using Application.UseCases.Comments.CreateComment;
using Application.UseCases.Comments.DeleteComment;
using Application.UseCases.Comments.GetCommentsByPost;
using Application.UseCases.Comments.GetRepliesByCommentId;
using Application.UseCases.Comments.ReplyToComment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/comments")]
[Authorize]
public class CommentController : ControllerBase
{
    private readonly ICreateCommentUseCase _createCommentUseCase;
    private readonly IReplyToCommentUseCase _replyToCommentUseCase;
    private readonly IGetRepliesByCommentIdUseCase _getRepliesByCommentIdUseCase;
    private readonly IGetCommentsByPostUseCase _getCommentsByPostUseCase;
    private readonly IDeleteCommentUseCase _deleteCommentUseCase;

    public CommentController(
        ICreateCommentUseCase createCommentUseCase,
        IReplyToCommentUseCase replyToCommentUseCase,
        IGetRepliesByCommentIdUseCase getRepliesByCommentIdUseCase,
        IGetCommentsByPostUseCase getCommentsByPostUseCase,
        IDeleteCommentUseCase deleteCommentUseCase)
    {
        _createCommentUseCase = createCommentUseCase;
        _replyToCommentUseCase = replyToCommentUseCase;
        _getRepliesByCommentIdUseCase = getRepliesByCommentIdUseCase;
        _getCommentsByPostUseCase = getCommentsByPostUseCase;
        _deleteCommentUseCase = deleteCommentUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequest request)
    {
        if (!User.TryGetUserId(out var authenticatedUserId))
            return Unauthorized("Invalid or missing user claim");

        var response = await _createCommentUseCase.ExecuteAsync(authenticatedUserId, request);
        return StatusCode(201, response);
    }

    [HttpPost("{commentId:guid}/replies")]
    public async Task<IActionResult> ReplyToComment(Guid commentId, [FromBody] ReplyToCommentRequestDto request)
    {
        if (!User.TryGetUserId(out var authenticatedUserId))
            return Unauthorized("Invalid or missing user claim");

        request.ParentCommentId = commentId;
        var response = await _replyToCommentUseCase.ExecuteAsync(authenticatedUserId, request);
        return StatusCode(201, response);
    }
    
    [AllowAnonymous]
    [HttpGet("{commentId:guid}/replies")]
    public async Task<IActionResult> GetRepliesByCommentId(Guid commentId)
    {
        var request = new GetRepliesByCommentIdRequest
        {
            CommentId = commentId
        };

        var response = await _getRepliesByCommentIdUseCase.ExecuteAsync(request);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpGet("post/{postId:guid}")]
    public async Task<IActionResult> GetCommentsByPost(Guid postId)
    {
        var request = new GetCommentsByPostRequest
        {
            PostId = postId
        };

        GetCommentsByPostResponse response = await _getCommentsByPostUseCase.ExecuteAsync(request);
        return Ok(response);
    }

    [HttpDelete("{commentId:guid}")]
    public async Task<IActionResult> DeleteComment(Guid commentId)
    {
        if (!User.TryGetUserId(out var authenticatedUserId))
            return Unauthorized("Invalid or missing user claim");

        var request = new DeleteCommentRequestDto
        {
            CommentId = commentId
        };

        var response = await _deleteCommentUseCase.ExecuteAsync(authenticatedUserId, request);
        return Ok(response);
    }
}