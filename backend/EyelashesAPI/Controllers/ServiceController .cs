using BussinessLogic.Interfaces;
using BussinessLogic.Records;
using EyelashesAPI.Requests;
using EyelashesAPI.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace EyelashesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        private readonly HttpClient _httpClient;

        public ServiceController(IServiceService serviceService, HttpClient httpClient)
        {
            _serviceService = serviceService;
            _httpClient = httpClient;
        }

        [HttpGet("get/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var service = await _serviceService.GetByIdAsync(id, cancellationToken);
            if (service == null)
            {
                return NotFound($"Service with ID {id} not found.");
            }
            return Ok(service);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var services = await _serviceService.GetAllAsync(cancellationToken);
            return Ok(services);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromForm] ServiceRequest serviceRequest, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(serviceRequest.MainImageUrl))
            {
                return BadRequest("Main image URL is required.");
            }

            var mainImagePath = await ImageDownloader.DownloadImageAsync(_httpClient, serviceRequest.MainImageUrl, cancellationToken);

            var imagePaths = new List<string>();
            if (serviceRequest.ImageUrls != null && serviceRequest.ImageUrls.Count > 0)
            {
                foreach (var imageUrl in serviceRequest.ImageUrls)
                {
                    var imagePath = await ImageDownloader.DownloadImageAsync(_httpClient, imageUrl, cancellationToken);
                    imagePaths.Add(imagePath);
                }
            }

            var serviceRec = new ServiceRec
            {
                Title = serviceRequest.Title,
                MainImagePath = mainImagePath,
                Price = serviceRequest.Price ?? 0,
                NewPrice = serviceRequest.NewPrice,
                OldPrice = serviceRequest.OldPrice,
                ImagePaths = imagePaths
            };

            await _serviceService.CreateAsync(serviceRec, cancellationToken);

            return Ok("Service has been created successfully.");
        }

        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] ServiceRequest serviceRequest, CancellationToken cancellationToken)
        {
            var existingService = await _serviceService.GetByIdAsync(id, cancellationToken);
            if (existingService == null)
            {
                return NotFound($"Service with ID {id} not found.");
            }

            var mainImagePath = string.IsNullOrEmpty(serviceRequest.MainImageUrl)
                ? existingService.MainImagePath
                : await ImageDownloader.DownloadImageAsync(_httpClient, serviceRequest.MainImageUrl, cancellationToken);

            var imagePaths = existingService.ImagePaths;

            if (serviceRequest.ImageUrls != null && serviceRequest.ImageUrls.Count > 0)
            {
                var newImagePaths = new List<string>();
                foreach (var imageUrl in serviceRequest.ImageUrls)
                {
                    var imagePath = await ImageDownloader.DownloadImageAsync(_httpClient, imageUrl, cancellationToken);
                    newImagePaths.Add(imagePath);
                }

                if (newImagePaths.Count > 0)
                {
                    imagePaths = newImagePaths;
                }
            }

            var updatedServiceRec = new ServiceRec
            {
                Id = id,
                Title = !string.IsNullOrEmpty(serviceRequest.Title) ? serviceRequest.Title : existingService.Title,
                MainImagePath = mainImagePath,
                Price = serviceRequest.Price ?? existingService.Price,
                NewPrice = serviceRequest.NewPrice ?? existingService.NewPrice,
                OldPrice = serviceRequest.OldPrice ?? existingService.OldPrice,
                ImagePaths = imagePaths
            };

            await _serviceService.UpdateAsync(updatedServiceRec, cancellationToken);

            return Ok($"Service with ID {id} has been updated successfully.");
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var existingService = await _serviceService.GetByIdAsync(id, cancellationToken);
            if (existingService == null)
            {
                return NotFound($"Service with ID {id} not found.");
            }

            await _serviceService.DeleteAsync(id, cancellationToken);
            return Ok($"Service with ID {id} has been deleted successfully.");
        }

        [HttpPost("{id:int}/addimages")]
        public async Task<IActionResult> AddImagesAsync(int id, [FromForm] List<string> imageUrls, CancellationToken cancellationToken)
        {
            var newImagePaths = new List<string>();

            foreach (var imageUrl in imageUrls)
            {
                var imagePath = await ImageDownloader.DownloadImageAsync(_httpClient, imageUrl, cancellationToken);
                newImagePaths.Add(imagePath);
            }

            await _serviceService.AddImagesAsync(id, newImagePaths, cancellationToken);

            return Ok($"Images have been added to Service with ID {id}.");
        }

        [HttpGet("{id:int}/images")]
        public async Task<IActionResult> GetImagesAsync(int id, CancellationToken cancellationToken)
        {
            var images = await _serviceService.GetImagesAsync(id, cancellationToken);
            return Ok(images);
        }

        [HttpDelete("deleteimage/{imageId:int}")]
        public async Task<IActionResult> DeleteImageAsync(int imageId, CancellationToken cancellationToken)
        {
            await _serviceService.DeleteImageAsync(imageId, cancellationToken);
            return Ok($"Image with ID {imageId} has been deleted successfully.");
        }
    }
}
