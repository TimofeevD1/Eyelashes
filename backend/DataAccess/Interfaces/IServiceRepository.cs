using DataAccess.Models;

namespace DataAccess.Interfaces
{
    public interface IServiceRepository
    {
        Task AddImagesAsync(int serviceId, List<string> imageUrls, CancellationToken cancellationToken = default);
        Task CreateAsync(Service service, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task DeleteImageAsync(int imageId, CancellationToken cancellationToken = default);
        Task<List<Service>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Service> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<ServiceImage>> GetImagesAsync(int serviceId, CancellationToken cancellationToken = default);
        Task UpdateAsync(Service updatedService, CancellationToken cancellationToken = default);
    }
}