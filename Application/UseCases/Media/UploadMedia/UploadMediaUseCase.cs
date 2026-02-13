using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Exceptions.Media;
using Application.UseCases.Media.UploadMedia.Dtos;
using Domain.Interface;
using Domain.Interface.Repository;

namespace Application.UseCases.Media.UploadMedia;

public class UploadMediaUseCase : IUploadMediaUseCase
{
    private readonly IFileStorageService _fileStorageService;
    private readonly IMediaRepository _mediaRepository;
    private readonly IUnitOfWork _unitOfWork;

    private const long MaxFileSize = 5 * 1024 * 1024; // 5MB

    private static readonly string[] AllowedContentTypes =
    {
        "image/png",
        "image/jpeg",
        "image/webp"
    };
    
    public UploadMediaUseCase(
        IFileStorageService fileStorageService,
        IMediaRepository mediaRepository,
        IUnitOfWork unitOfWork)
    {
        _fileStorageService = fileStorageService;
        _mediaRepository = mediaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UploadMediaResponse> ExecuteAsync(UploadMediaRequest request)
    {
        ValidateFileSize(request.Size);
        ValidateContentType(request.ContentType);
        ValidateExtension(request.FileName);

        var url = await UploadFileAsync(request);

        var media = CreateMediaEntity(request, url);

        await PersistMediaAsync(media);

        return new UploadMediaResponse(url);
    }

    private void ValidateFileSize(long size)
    {
        if (size > MaxFileSize)
            throw new InvalidMediaException("File size exceeds 5MB limit.");
    }

    private void ValidateContentType(string contentType)
    {
        if (!AllowedContentTypes.Contains(contentType))
            throw new InvalidMediaException("Unsupported file type.");
    }

    private void ValidateExtension(string fileName)
    {
        var allowedExtensions = new[] { ".png", ".jpg", ".jpeg", ".webp" };

        if (!allowedExtensions.Any(ext =>
                fileName.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidMediaException("Invalid file extension.");
        }
    }

    private async Task<string> UploadFileAsync(UploadMediaRequest request)
    {
        var fileUploadRequest = new FileUploadRequest(
            request.FileStream,
            request.FileName,
            request.ContentType
        );

        return await _fileStorageService.UploadAsync(fileUploadRequest);
    }

    private Domain.Entities.Media CreateMediaEntity(UploadMediaRequest request, string url)
    {
        return new Domain.Entities.Media(
            request.FileName,
            url,
            request.ContentType,
            request.Size,
            request.UserId);
    }

    private async Task PersistMediaAsync(Domain.Entities.Media media)
    {
        await _mediaRepository.CreateAsync(media);
        await _unitOfWork.SaveChangesAsync();
    }
}

