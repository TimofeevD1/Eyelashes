using BussinessLogic.Records;
using DataAccess.Interfaces;
using DataAccess.Models;

public class PromoService(IPromoRepository promoRepository) : IPromoService
{
    public async Task CreateAsync(PromoRec promoRec, CancellationToken cancellationToken = default)
    {
        var promo = new Promo
        {
            Title = promoRec.Title,
            OldPrice = promoRec.OldPrice,
            NewPrice = promoRec.NewPrice,
            DiscountDescription = promoRec.DiscountDescription,
            Benefits = promoRec.Benefits
        };

        await promoRepository.CreatePromoAsync(promo, cancellationToken);
    }

    public async Task<PromoRec> GetAsync(CancellationToken cancellationToken = default)
    {
        var promo = await promoRepository.GetPromoAsync(cancellationToken);
        return new PromoRec
        {
            Id = promo.Id,
            Title = promo.Title,
            OldPrice = promo.OldPrice,
            NewPrice = promo.NewPrice,
            DiscountDescription = promo.DiscountDescription,
            Benefits = promo.Benefits
        };
    }



    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        await promoRepository.DeletePromoAsync(id, cancellationToken);
    }

    public async Task UpdateAsync(PromoRec promoRec, CancellationToken cancellationToken = default)
    {
        var updatedPromo = new Promo
        {
            Id = promoRec.Id,
            Title = promoRec.Title,
            OldPrice = promoRec.OldPrice,
            NewPrice = promoRec.NewPrice,
            DiscountDescription = promoRec.DiscountDescription,
            Benefits = promoRec.Benefits
        };

        await promoRepository.UpdatePromoAsync(updatedPromo, cancellationToken);
    }
}