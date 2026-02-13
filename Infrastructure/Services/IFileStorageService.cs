using Infrastructure.Services.Dtos;

namespace Infrastructure.Services;

public interface IFileStorageService
{
    Task<string> UploadAsync(FileUploadRequest request);

}