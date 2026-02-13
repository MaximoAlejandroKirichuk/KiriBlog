using Application.UseCases.Media.UploadMedia;
using Application.UseCases.Media.UploadMedia.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[ApiController]
[Route("api/media")]
public class MediaController : ControllerBase
{
    private readonly IUploadMediaUseCase _uploadMediaUseCase;

    public MediaController(IUploadMediaUseCase uploadMediaUseCase)
    {
        _uploadMediaUseCase = uploadMediaUseCase;
    }

    [Authorize(Roles = "Admin,Author")]
    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Invalid file.");

        var userIdClaim = User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
            return Unauthorized();

        var userId = Guid.Parse(userIdClaim);

        using var stream = file.OpenReadStream();

        var request = new UploadMediaRequest(
            stream,
            file.FileName,
            file.ContentType,
            file.Length,
            userId
        );

        var response = await _uploadMediaUseCase.ExecuteAsync(request);

        return Ok(response);
    }
}