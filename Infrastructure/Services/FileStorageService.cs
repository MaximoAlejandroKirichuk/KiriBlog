using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Infrastructure.Services.Dtos;

namespace Infrastructure.Services;

public class AzureBlobStorageService : IFileStorageService
{
    private readonly BlobContainerClient _containerClient;

    public AzureBlobStorageService(
        BlobServiceClient blobServiceClient)
    {
        _containerClient = blobServiceClient
            .GetBlobContainerClient("media");

        _containerClient.CreateIfNotExists();
    }

    public async Task<string> UploadAsync(FileUploadRequest request)
    {
        var uniqueFileName = $"{Guid.NewGuid()}-{request.FileName}";
        var blobClient = _containerClient.GetBlobClient(uniqueFileName);

        await blobClient.UploadAsync(
            request.FileStream,
            new BlobHttpHeaders
            {
                ContentType = request.ContentType
            });

        return blobClient.Uri.ToString();
    }

}
