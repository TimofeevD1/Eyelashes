using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositorys
{
    internal class AboutMeRepository(AppContext context) : IAboutMeRepository
    {

        public async Task<AboutMe> GetAsync(CancellationToken cancellationToken = default)
        {
            var aboutMe = await context.AboutMe
                        .Include(a => a.Photos) 
                        .FirstOrDefaultAsync(cancellationToken);
            if (aboutMe == null)
            {
                throw new Exception($"AboutMe not found.");
            }
            return aboutMe;
        }

        public async Task CreateAsync(AboutMe me, CancellationToken cancellationToken = default)
        {
            await context.AboutMe.AddAsync(me, cancellationToken);
            await context.SaveChangesAsync();
        }

        public async Task<int> GetCountAsync(CancellationToken cancellationToken = default)
        {
            return await context.AboutMe.CountAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var aboutMe = await context.AboutMe.FindAsync(new object[] { id }, cancellationToken);
            if (aboutMe == null)
            {
                throw new Exception($"AboutMe with ID {id} not found.");
            }

            context.AboutMe.Remove(aboutMe);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(AboutMe updatedAboutMe, CancellationToken cancellationToken = default)
        {
            var existingAboutMe = await context.AboutMe.FindAsync(new object[] { updatedAboutMe.Id }, cancellationToken);
            if (existingAboutMe == null)
            {
                throw new Exception($"AboutMe with ID {updatedAboutMe.Id} not found.");
            }

            existingAboutMe.FullName = updatedAboutMe.FullName;
            existingAboutMe.Job = updatedAboutMe.Job;
            existingAboutMe.Address = updatedAboutMe.Address;
            existingAboutMe.PhoneNumber = updatedAboutMe.PhoneNumber;
            existingAboutMe.Coordinates = updatedAboutMe.Coordinates;
            existingAboutMe.Description = updatedAboutMe.Description;
            existingAboutMe.Image = updatedAboutMe.Image;
            existingAboutMe.Photos = updatedAboutMe.Photos;

            context.AboutMe.Update(existingAboutMe);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task AddPhotosAsync(int aboutMeId, List<string> newPhotoUrls, CancellationToken cancellationToken = default)
        {
            var aboutMe = await context.AboutMe
                .Include(a => a.Photos)
                .FirstOrDefaultAsync(a => a.Id == aboutMeId, cancellationToken);

            if (aboutMe == null)
            {
                throw new Exception($"AboutMe with ID {aboutMeId} not found.");
            }

            var newPhotos = newPhotoUrls.Select(url => new Photo
            {
                Url = url,
                AboutMeId = aboutMeId
            }).ToList();

            aboutMe.Photos.AddRange(newPhotos);

            context.AboutMe.Update(aboutMe);
            await context.SaveChangesAsync(cancellationToken);
        }


        public async Task<List<Photo>> GetPhotosAsync(int aboutMeId, CancellationToken cancellationToken = default)
        {
            var aboutMe = await context.AboutMe
                .Include(a => a.Photos)
                .FirstOrDefaultAsync(a => a.Id == aboutMeId, cancellationToken);

            if (aboutMe == null)
            {
                throw new Exception($"AboutMe with ID {aboutMeId} not found.");
            }

            return aboutMe.Photos;
        }


        public async Task DeletePhotoAsync(int photoId, CancellationToken cancellationToken = default)
        {
            var photo = await context.Photos.FindAsync(new object[] { photoId }, cancellationToken);

            if (photo == null)
            {
                throw new Exception($"Photo with ID {photoId} not found.");
            }

            context.Photos.Remove(photo);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
