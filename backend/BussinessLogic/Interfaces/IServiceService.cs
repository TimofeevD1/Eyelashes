using BussinessLogic.Records;

namespace BussinessLogic.Interfaces
{
    public interface IServiceService
    {
        Task AddImagesAsync(int serviceId, List<string> imageUrls, CancellationToken cancellationToken = default);
        Task CreateAsync(ServiceRec serviceRec, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task DeleteImageAsync(int imageId, CancellationToken cancellationToken = default);
        Task<List<ServiceRec>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ServiceRec> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<string>> GetImagesAsync(int serviceId, CancellationToken cancellationToken = default);
        Task UpdateAsync(ServiceRec serviceRec, CancellationToken cancellationToken = default);
    }
}