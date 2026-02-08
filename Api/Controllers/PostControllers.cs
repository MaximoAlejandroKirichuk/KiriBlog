using Application.UseCases.Post.CreatePost;
using Application.UseCases.Post.CreatePost.Dtos;
using Application.UseCases.Post.GetAllPostPublic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/post")]
[Authorize]
public class PostControllers : ControllerBase
{
    private readonly ICreatePostUseCase _createPostUseCase;
    private readonly IGetAllPostPublicUseCase _getAllPostPublicUseCase;

    public PostControllers(ICreatePostUseCase createPostUseCase, IGetAllPostPublicUseCase  getAllUseCase)
    {
        _createPostUseCase = createPostUseCase;
        _getAllPostPublicUseCase = getAllUseCase;
    }
    
    [HttpPost]
    [Authorize(Roles = "Author")]
    public async Task<IActionResult> CreatePost(CreatePostRequest createPostDto)
    {
        await _createPostUseCase.ExecuteAsync(createPostDto);
        return Ok();
    }

    [HttpGet("public")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllPost()
    {
        var posts = await _getAllPostPublicUseCase.ExecuteAsync();
        return Ok(posts);
    }
}