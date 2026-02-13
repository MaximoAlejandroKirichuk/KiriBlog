using Application.Common.Models;

namespace Application.Common.Interfaces;

public interface IFileStorageService
{
    Task<string> UploadAsync(FileUploadRequest request);
}