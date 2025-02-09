using BussinessLogic.Interfaces;
using BussinessLogic.Records;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace BussinessLogic.Services
{
    public class AboutMeService(IAboutMeRepository aboutMeRepository) : IAboutMeService
    {

        public async Task CreateAsync(AboutMeRec aboutMeRec, CancellationToken cancellationToken = default)
        {
            var photos = new List<Photo>();

            foreach (var additionalPhotoPath in aboutMeRec.AdditionalPhotosPath)
            {
                if (!string.IsNullOrEmpty(additionalPhotoPath))
                {
                    var additionalPhoto = new Photo
                    {
                        Url = additionalPhotoPath
                    };
                    photos.Add(additionalPhoto);
                }
            }

            var aboutMe = new AboutMe
            {
                FullName = aboutMeRec.FullName,
                Job = aboutMeRec.Job,
                Address = aboutMeRec.Address,
                PhoneNumber = aboutMeRec.PhoneNumber,
                Coordinates = aboutMeRec.Coordinates,
                Description = aboutMeRec.Description,
                Image = aboutMeRec.MainImagePath,
                Photos = photos
            };

            await aboutMeRepository.CreateAsync(aboutMe, cancellationToken);
        }

        public async Task<AboutMeRec> GetAsync(CancellationToken cancellationToken = default)
        {
            var aboutMe = await aboutMeRepository.GetAsync(cancellationToken);
            return new AboutMeRec
            {
                Id = aboutMe.Id,
                FullName = aboutMe.FullName,
                Job = aboutMe.Job,
                Address = aboutMe.Address,
                PhoneNumber = aboutMe.PhoneNumber,
                Coordinates = aboutMe.Coordinates,
                Description = aboutMe.Description,
                MainImagePath = aboutMe.Image,
                AdditionalPhotosPath = aboutMe.Photos.Select(p => p.Url).ToList()
            };
        }

        public async Task<int> GetCountAsync(CancellationToken cancellationToken = default)
        {
            return await aboutMeRepository.GetCountAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await aboutMeRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task UpdateAsync(AboutMeRec aboutMeRec, CancellationToken cancellationToken = default)
        {
            var updatedAboutMe = new AboutMe
            {
                Id = aboutMeRec.Id,
                FullName = aboutMeRec.FullName,
                Job = aboutMeRec.Job,
                Address = aboutMeRec.Address,
                PhoneNumber = aboutMeRec.PhoneNumber,
                Coordinates = aboutMeRec.Coordinates,
                Description = aboutMeRec.Description,
                Image = aboutMeRec.MainImagePath,
                Photos = aboutMeRec.AdditionalPhotosPath.Select(p => new Photo
                {
                    Url = p,
                    AboutMeId = aboutMeRec.Id
                }).ToList()
            };

            await aboutMeRepository.UpdateAsync(updatedAboutMe, cancellationToken);
        }

        public async Task AddPhotosAsync(int aboutMeId, List<string> newPhotoUrls, CancellationToken cancellationToken = default)
        {
            await aboutMeRepository.AddPhotosAsync(aboutMeId, newPhotoUrls, cancellationToken);
        }

        public async Task<List<string>> GetPhotosAsync(int aboutMeId, CancellationToken cancellationToken = default)
        {
            var photos = await aboutMeRepository.GetPhotosAsync(aboutMeId, cancellationToken);
            return photos.Select(p => p.Url).ToList();
        }

        public async Task DeletePhotoAsync(int photoId, CancellationToken cancellationToken = default)
        {
            await aboutMeRepository.DeletePhotoAsync(photoId, cancellationToken);
        }
    }
}
