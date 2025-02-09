using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IAboutMeRepository
    {
        Task AddPhotosAsync(int aboutMeId, List<string> newPhotoUrls, CancellationToken cancellationToken = default);
        Task CreateAsync(AboutMe me, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task DeletePhotoAsync(int photoId, CancellationToken cancellationToken = default);
        Task<AboutMe> GetAsync(CancellationToken cancellationToken = default);
        Task<int> GetCountAsync(CancellationToken cancellationToken = default);
        Task<List<Photo>> GetPhotosAsync(int aboutMeId, CancellationToken cancellationToken = default);
        Task UpdateAsync(AboutMe updatedAboutMe, CancellationToken cancellationToken = default);
    }
}
