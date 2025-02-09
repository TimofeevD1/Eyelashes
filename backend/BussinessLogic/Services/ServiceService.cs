using BussinessLogic.Interfaces;
using DataAccess.Models;
using DataAccess.Interfaces;
using BussinessLogic.Records;

namespace BussinessLogic.Services
{
    public class ServiceService(IServiceRepository serviceRepository) : IServiceService
    {
        public async Task CreateAsync(ServiceRec serviceRec, CancellationToken cancellationToken = default)
        {
            var images = new List<ServiceImage>();

            foreach (var imagePath in serviceRec.ImagePaths)
            {
                if (!string.IsNullOrEmpty(imagePath))
                {
                    images.Add(new ServiceImage
                    {
                        Url = imagePath
                    });
                }
            }

            var service = new Service
            {
                Title = serviceRec.Title,
                MainImage = serviceRec.MainImagePath,
                Price = serviceRec.Price,
                NewPrice = serviceRec.NewPrice,
                OldPrice = serviceRec.OldPrice,
                Images = images
            };

            await serviceRepository.CreateAsync(service, cancellationToken);
        }

        public async Task<ServiceRec> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var service = await serviceRepository.GetByIdAsync(id, cancellationToken);

            return new ServiceRec
            {
                Id = service.Id,
                Title = service.Title,
                MainImagePath = service.MainImage,
                Price = service.Price,
                NewPrice = service.NewPrice,
                OldPrice = service.OldPrice,
                ImagePaths = service.Images.Select(img => img.Url).ToList()
            };
        }

        public async Task<List<ServiceRec>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var services = await serviceRepository.GetAllAsync(cancellationToken);

            return services.Select(service => new ServiceRec
            {
                Id = service.Id,
                Title = service.Title,
                MainImagePath = service.MainImage,
                Price = service.Price,
                NewPrice = service.NewPrice,
                OldPrice = service.OldPrice,
                ImagePaths = service.Images.Select(img => img.Url).ToList()
            }).ToList();
        }

        public async Task UpdateAsync(ServiceRec serviceRec, CancellationToken cancellationToken = default)
        {
            var updatedService = new Service
            {
                Id = serviceRec.Id,
                Title = serviceRec.Title,
                MainImage = serviceRec.MainImagePath,
                Price = serviceRec.Price,
                NewPrice = serviceRec.NewPrice,
                OldPrice = serviceRec.OldPrice,
                Images = serviceRec.ImagePaths.Select(path => new ServiceImage
                {
                    Url = path,
                    ServiceId = serviceRec.Id
                }).ToList()
            };

            await serviceRepository.UpdateAsync(updatedService, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await serviceRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task AddImagesAsync(int serviceId, List<string> imageUrls, CancellationToken cancellationToken = default)
        {
            await serviceRepository.AddImagesAsync(serviceId, imageUrls, cancellationToken);
        }

        public async Task<List<string>> GetImagesAsync(int serviceId, CancellationToken cancellationToken = default)
        {
            var images = await serviceRepository.GetImagesAsync(serviceId, cancellationToken);
            return images.Select(img => img.Url).ToList();
        }

        public async Task DeleteImageAsync(int imageId, CancellationToken cancellationToken = default)
        {
            await serviceRepository.DeleteImageAsync(imageId, cancellationToken);
        }
    }
}
