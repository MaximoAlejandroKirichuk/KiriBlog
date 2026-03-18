using Application.UseCases.Comments.CreateComment;
using Application.UseCases.Comments.ReplyToComment;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/comments")]
public class CommentController : ControllerBase
{
    private readonly ICreateCommentUseCase _createCommentUseCase;
    private readonly IReplyToCommentUseCase _replyToCommentUseCase;

    public CommentController(ICreateCommentUseCase createCommentUseCase, IReplyToCommentUseCase replyToCommentUseCase)
    {
        _createCommentUseCase = createCommentUseCase;
        _replyToCommentUseCase = replyToCommentUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequest request)
    {
        var response = await _createCommentUseCase.ExecuteAsync(request);
        return StatusCode(201,response);
    }
    
    [HttpPost]
    public async Task<IActionResult> ReplyToComment([FromBody] ReplyToCommentRequestDto request)
    {
        var response = await _replyToCommentUseCase.ExecuteAsync(request);
        return StatusCode(201,response);
    }
}