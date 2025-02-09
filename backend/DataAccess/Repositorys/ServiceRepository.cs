using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositorys
{
    internal class ServiceRepository(AppContext context) : IServiceRepository
    {
        public async Task<List<Service>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.Services
                .Include(s => s.Images)
                .ToListAsync(cancellationToken);
        }

        public async Task<Service> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var service = await context.Services
                .Include(s => s.Images)
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

            if (service == null)
            {
                throw new Exception($"Service with ID {id} not found.");
            }

            return service;
        }

        public async Task CreateAsync(Service service, CancellationToken cancellationToken = default)
        {
            await context.Services.AddAsync(service, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Service updatedService, CancellationToken cancellationToken = default)
        {
            var existingService = await context.Services
                .Include(s => s.Images)
                .FirstOrDefaultAsync(s => s.Id == updatedService.Id, cancellationToken);

            if (existingService == null)
            {
                throw new Exception($"Service with ID {updatedService.Id} not found.");
            }

            existingService.Title = updatedService.Title;
            existingService.MainImage = updatedService.MainImage;
            existingService.Price = updatedService.Price;
            existingService.NewPrice = updatedService.NewPrice;
            existingService.OldPrice = updatedService.OldPrice;
            existingService.Images = updatedService.Images;

            context.Services.Update(existingService);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var service = await context.Services.FindAsync(new object[] { id }, cancellationToken);
            if (service == null)
            {
                throw new Exception($"Service with ID {id} not found.");
            }

            context.Services.Remove(service);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task AddImagesAsync(int serviceId, List<string> imageUrls, CancellationToken cancellationToken = default)
        {
            var service = await context.Services
                .Include(s => s.Images)
                .FirstOrDefaultAsync(s => s.Id == serviceId, cancellationToken);

            if (service == null)
            {
                throw new Exception($"Service with ID {serviceId} not found.");
            }

            var newImages = imageUrls.Select(url => new ServiceImage
            {
                Url = url,
                ServiceId = serviceId
            }).ToList();

            service.Images.AddRange(newImages);

            context.Services.Update(service);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<ServiceImage>> GetImagesAsync(int serviceId, CancellationToken cancellationToken = default)
        {
            var service = await context.Services
                .Include(s => s.Images)
                .FirstOrDefaultAsync(s => s.Id == serviceId, cancellationToken);

            if (service == null)
            {
                throw new Exception($"Service with ID {serviceId} not found.");
            }

            return service.Images;
        }

        public async Task DeleteImageAsync(int imageId, CancellationToken cancellationToken = default)
        {
            var image = await context.ServiceImages.FindAsync(new object[] { imageId }, cancellationToken);

            if (image == null)
            {
                throw new Exception($"Image with ID {imageId} not found.");
            }

            context.ServiceImages.Remove(image);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
