using DataAccess.Models;

namespace DataAccess.Interfaces
{
    public interface IPromoRepository
    {
        Task CreatePromoAsync(Promo promo, CancellationToken cancellationToken = default);
        Task DeletePromoAsync(int id, CancellationToken cancellationToken = default);
        Task<Promo> GetPromoAsync(CancellationToken cancellationToken = default);
        Task UpdatePromoAsync(Promo updatedPromo, CancellationToken cancellationToken = default);
    }
}