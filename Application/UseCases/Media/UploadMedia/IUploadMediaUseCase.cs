using Application.UseCases.Media.UploadMedia.Dtos;

namespace Application.UseCases.Media.UploadMedia;

public interface IUploadMediaUseCase
{
    Task<UploadMediaResponse> ExecuteAsync(
        UploadMediaRequest request);
}