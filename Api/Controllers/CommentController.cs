using Application.UseCases.Comments.CreateComment;
using Application.UseCases.Comments.GetRepliesByCommentId;
using Application.UseCases.Comments.ReplyToComment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/comments")]
[Authorize(Roles = "Author, Visitor")]
public class CommentController : ControllerBase
{
    private readonly ICreateCommentUseCase _createCommentUseCase;
    private readonly IReplyToCommentUseCase _replyToCommentUseCase;
    private readonly IGetRepliesByCommentIdUseCase _getRepliesByCommentIdUseCase;

    public CommentController(
        ICreateCommentUseCase createCommentUseCase,
        IReplyToCommentUseCase replyToCommentUseCase,
        IGetRepliesByCommentIdUseCase getRepliesByCommentIdUseCase)
    {
        _createCommentUseCase = createCommentUseCase;
        _replyToCommentUseCase = replyToCommentUseCase;
        _getRepliesByCommentIdUseCase = getRepliesByCommentIdUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequest request)
    {
        var response = await _createCommentUseCase.ExecuteAsync(request);
        return StatusCode(201, response);
    }

    [HttpPost("{commentId:guid}/replies")]
    public async Task<IActionResult> ReplyToComment(Guid commentId, [FromBody] ReplyToCommentRequestDto request)
    {
        request.ParentCommentId = commentId;
        var response = await _replyToCommentUseCase.ExecuteAsync(request);
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
}