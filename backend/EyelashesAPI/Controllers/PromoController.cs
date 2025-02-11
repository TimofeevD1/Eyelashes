using BussinessLogic.Records;
using EyelashesAPI.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EyelashesAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PromoController : ControllerBase
    {
        private readonly IPromoService _promoService;

        public PromoController(IPromoService promoService)
        {
            _promoService = promoService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetPromoAsync(CancellationToken cancellationToken)
        {
            var promo = await _promoService.GetAsync(cancellationToken);
            if (promo == null)
            {
                return NotFound($"Promo not found.");
            }

            return Ok(promo);
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreatePromoAsync([FromBody] PromoRequest promoRequest, CancellationToken cancellationToken)
        {
            if (promoRequest == null)
            {
                return BadRequest("Promo data is required.");
            }

            var promoRec = new PromoRec
            {
                Title = promoRequest.Title,
                OldPrice = promoRequest.OldPrice.ToString(),
                NewPrice = promoRequest.NewPrice.ToString(),
                DiscountDescription = promoRequest.DiscountDescription,
                Benefits = promoRequest.Benefits
            };

            await _promoService.CreateAsync(promoRec, cancellationToken);
            return Ok("Promo has been created successfully.");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdatePromoAsync([FromForm] PromoRequest promoRequest, CancellationToken cancellationToken)
        {
            var existingPromo = await _promoService.GetAsync(cancellationToken);
            if (existingPromo == null)
            {
                return NotFound($"Promo not found.");
            }

            var updatedPromoRec = new PromoRec { Id = existingPromo.Id, };

            updatedPromoRec.Title = string.IsNullOrEmpty(promoRequest.Title) ? existingPromo.Title : promoRequest.Title;
            updatedPromoRec.OldPrice = string.IsNullOrEmpty(promoRequest.OldPrice) ? existingPromo.OldPrice : promoRequest.OldPrice;
            updatedPromoRec.NewPrice = string.IsNullOrEmpty(promoRequest.NewPrice) ? existingPromo.NewPrice : promoRequest.NewPrice;
            updatedPromoRec.DiscountDescription = string.IsNullOrEmpty(promoRequest.DiscountDescription) ? existingPromo.DiscountDescription : promoRequest.DiscountDescription;
            updatedPromoRec.Benefits = promoRequest.Benefits.Any() ? promoRequest.Benefits : promoRequest.Benefits;

            await _promoService.UpdateAsync(updatedPromoRec, cancellationToken);
            return Ok($"Promo has been updated successfully.");
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> DeletePromoAsync( CancellationToken cancellationToken)
        {
            var existingPromo = await _promoService.GetAsync(cancellationToken);
            if (existingPromo == null)
            {
                return NotFound($"Promo not found.");
            }

            await _promoService.DeleteAsync(existingPromo.Id, cancellationToken);
            return Ok($"Promo with ID {existingPromo.Id} has been deleted successfully.");
        }
    }
}
