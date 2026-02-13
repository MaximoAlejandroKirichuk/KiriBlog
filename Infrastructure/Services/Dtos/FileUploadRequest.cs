namespace Infrastructure.Services.Dtos;

public record FileUploadRequest(
    Stream FileStream,
    string FileName,
    string ContentType
);
