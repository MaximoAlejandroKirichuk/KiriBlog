using Domain.Entities;

namespace Domain.Interface.Repository;

public interface IMediaRepository
{
    Task CreateAsync(Media media);
}