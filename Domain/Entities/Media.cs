namespace Domain.Entities;

public class Media
{
    public Guid Id { get; private set; }
    public string FileName { get; private set; }
    public string Url { get; private set; }
    public string ContentType { get; private set; }
    public long Size { get; private set; }
    public Guid UploadedByUserId { get; private set; }
    public DateTime UploadedAt { get; private set; }

    private Media() { } // EF

    public Media(
        string fileName,
        string url,
        string contentType,
        long size,
        Guid uploadedByUserId)
    {
        Id = Guid.NewGuid();
        FileName = fileName;
        Url = url;
        ContentType = contentType;
        Size = size;
        UploadedByUserId = uploadedByUserId;
        UploadedAt = DateTime.UtcNow;
    }
}