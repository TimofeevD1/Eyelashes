using BussinessLogic.Records;

namespace BussinessLogic.Interfaces
{
    public interface IAboutMeService
    {
        Task AddPhotosAsync(int aboutMeId, List<string> newPhotoUrls, CancellationToken cancellationToken = default);
        Task CreateAsync(AboutMeRec aboutMeRec, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task DeletePhotoAsync(int photoId, CancellationToken cancellationToken = default);
        Task<AboutMeRec> GetAsync(CancellationToken cancellationToken = default);
        Task<int> GetCountAsync(CancellationToken cancellationToken = default);
        Task<List<string>> GetPhotosAsync(int aboutMeId, CancellationToken cancellationToken = default);
        Task UpdateAsync(AboutMeRec aboutMeRec, CancellationToken cancellationToken = default);
    }
}
