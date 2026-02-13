namespace Application.Common.Models;

public record FileUploadRequest(
    Stream FileStream,
    string FileName,
    string ContentType
);