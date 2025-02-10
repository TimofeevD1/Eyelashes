using BussinessLogic.Records;

public interface IPromoService
{
    Task CreateAsync(PromoRec promoRec, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<PromoRec> GetAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(PromoRec promoRec, CancellationToken cancellationToken = default);
}