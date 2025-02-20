using BussinessLogic.Interfaces;
using BussinessLogic.Records;
using EyelashesAPI.Requests;
using EyelashesAPI.TelegramBot;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;

namespace EyelashesAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AboutMeController: ControllerBase
    {
        private readonly IAboutMeService _aboutMeService;
        private readonly HttpClient _httpClient;
        private readonly ITelegramMessageService _telegramMessageService;

        //private TelegramOrderBot _bot;

        public AboutMeController(IAboutMeService aboutMeService, HttpClient httpClient, ITelegramMessageService telegramMessageService)
        {
            _aboutMeService = aboutMeService;
            _httpClient = httpClient;
            _telegramMessageService = telegramMessageService;
            //var token = "7755669526:AAGozCoAOWxHgjnykUzIaZ5iKach1UkV2Qo";
            //_bot = new TelegramOrderBot(token);

            //var cts = new CancellationTokenSource();
            //var cancellationToken1 = cts.Token;
            //_bot.StartReceiving(cancellationToken1);
        }


        [HttpGet("get")]
        public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
        {
            var count = await _aboutMeService.GetCountAsync(cancellationToken);

            if (count == 0)
            {
                return BadRequest(new { success = false, message = "Information about me has not been created yet." });
            }

            long chatId = 977105840;
            string orderDetails = "Имя клиента: Иван, Номер: 123456, Дата: 01/01/2025, Услуга: Стрижка, Время: 12:00";

            await _telegramMessageService.SendMessageAsync(chatId, "", cancellationToken, true, orderDetails);

            var aboutMe = await _aboutMeService.GetAsync(cancellationToken);

            if (aboutMe == null)
            {
                return BadRequest(new { success = false, message = "Failed to retrieve information about me." });
            }

            return Ok(new { success = true, data = aboutMe });
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromForm] AboutMeRequest aboutMeRequest, CancellationToken cancellationToken)
        {
            var count = await _aboutMeService.GetCountAsync(cancellationToken);

            if (count > 0)
            {
                return BadRequest(new { success = false, message = "Information about me has already been created." });
            }

            if (string.IsNullOrEmpty(aboutMeRequest.MainImageUrl))
            {
                return BadRequest(new { success = false, message = "Main image URL is required." });
            }

            var aboutMe = new AboutMeRec
            {
                FullName = aboutMeRequest.FullName,
                Job = aboutMeRequest.Job,
                Address = aboutMeRequest.Address,
                PhoneNumber = aboutMeRequest.PhoneNumber,
                Coordinates = new NpgsqlTypes.NpgsqlPoint(aboutMeRequest.X, aboutMeRequest.Y),
                Description = aboutMeRequest.Description,
                MainImagePath = aboutMeRequest.MainImageUrl,
                AdditionalPhotosPath = aboutMeRequest.AdditionalPhotoUrls
            };

            await _aboutMeService.CreateAsync(aboutMe, cancellationToken);

            // Возвращаем Ok с дополнительной информацией
            return Ok(new { success = true, message = "AboutMe has been created successfully." });
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var existingAboutMe = await _aboutMeService.GetAsync(cancellationToken);

            if (existingAboutMe == null || existingAboutMe.Id != id)
            {
                return NotFound(new { success = false, message = $"AboutMe with ID {id} not found." });
            }

            await _aboutMeService.DeleteAsync(id, cancellationToken);

            return Ok(new { success = true, message = $"AboutMe with ID {id} has been deleted successfully." });
        }

        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] AboutMeRequest aboutMeRequest, CancellationToken cancellationToken)
        {
            var existingAboutMe = await _aboutMeService.GetAsync(cancellationToken);
            if (existingAboutMe == null || existingAboutMe.Id != id)
            {
                return NotFound(new { success = false, message = $"AboutMe with ID {id} not found." });
            } 
            var mainImageFilePath = string.IsNullOrEmpty(aboutMeRequest.MainImageUrl) 
                ? existingAboutMe.MainImagePath
                : aboutMeRequest.MainImageUrl;

            var photoUrls = existingAboutMe.AdditionalPhotosPath; 


            if (aboutMeRequest.AdditionalPhotoUrls != null && aboutMeRequest.AdditionalPhotoUrls.Count > 0)
            {
                var photoUrlsCurrent = new List<string>();
                foreach (var photoUrl in aboutMeRequest.AdditionalPhotoUrls)
                {
                    if (!string.IsNullOrEmpty(photoUrl)) 
                    {
                        photoUrlsCurrent.Add(photoUrl);
                    }
                }

                if (photoUrlsCurrent.Count > 0) { photoUrls = photoUrlsCurrent; }
            }

            var aboutMe = new AboutMeRec
            {
                Id = id,
                FullName = !string.IsNullOrEmpty(aboutMeRequest.FullName) ? aboutMeRequest.FullName : existingAboutMe.FullName,
                Job = !string.IsNullOrEmpty(aboutMeRequest.Job) ? aboutMeRequest.Job : existingAboutMe.Job,
                Address = !string.IsNullOrEmpty(aboutMeRequest.Address) ? aboutMeRequest.Address : existingAboutMe.Address,
                PhoneNumber = !string.IsNullOrEmpty(aboutMeRequest.PhoneNumber) ? aboutMeRequest.PhoneNumber : existingAboutMe.PhoneNumber,
                Description = !string.IsNullOrEmpty(aboutMeRequest.Description) ? aboutMeRequest.Description : existingAboutMe.Description,
                MainImagePath = mainImageFilePath,
                AdditionalPhotosPath = photoUrls
            };

            if (string.IsNullOrEmpty(aboutMeRequest.X.ToString()) ||
                string.IsNullOrEmpty(aboutMeRequest.Y.ToString()))
            {
                aboutMe.Coordinates = existingAboutMe.Coordinates;
            }
            else 
            {
                aboutMe.Coordinates = new NpgsqlTypes.NpgsqlPoint(aboutMeRequest.X, aboutMeRequest.Y);
            }

            await _aboutMeService.UpdateAsync(aboutMe, cancellationToken);

            return Ok(new { success = true, message = $"AboutMe with ID {id} has been updated successfully." });
        }


        [HttpGet("{id:int}/photos")]
        public async Task<IActionResult> GetPhotosAsync(int id, CancellationToken cancellationToken)
        {
            var photos = await _aboutMeService.GetPhotosAsync(id, cancellationToken);

            if (photos == null || !photos.Any())
            {
                return NotFound(new { success = false, message = $"No photos found for AboutMe with ID {id}." });
            }

            return Ok(new { success = true, photos = photos });
        }



        [HttpPost("{id:int}/addphotos")]
        public async Task<IActionResult> AddPhotosAsync(int id, [FromForm] List<string> photoUrls, CancellationToken cancellationToken)
        {
            var photoUrlsCurrent = new List<string>();
            foreach (var photoUrl in photoUrls)
            {
                if (!string.IsNullOrEmpty(photoUrl))
                {
                    photoUrlsCurrent.Add(photoUrl);
                }
            }

            if (photoUrlsCurrent.Count == 0)
            {
                return BadRequest(new { success = false, message = "No valid photo URLs provided." });
            }

            await _aboutMeService.AddPhotosAsync(id, photoUrlsCurrent, cancellationToken);
            return Ok(new { success = true, message = $"Photos have been added to AboutMe with ID {id}." });
        }

        [HttpDelete("deletephoto/{photoId:int}")]
        public async Task<IActionResult> DeletePhotoAsync(int photoId, CancellationToken cancellationToken)
        {
            await _aboutMeService.DeletePhotoAsync(photoId, cancellationToken);
            return Ok(new { success = true, message = $"Photo with ID {photoId} has been deleted successfully." });
        }


    }
}
