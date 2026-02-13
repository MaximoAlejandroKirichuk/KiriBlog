namespace Application.UseCases.Media.UploadMedia.Dtos;

public record UploadMediaRequest(
    Stream FileStream,
    string FileName,
    string ContentType,
    long Size,
    Guid UserId
);
