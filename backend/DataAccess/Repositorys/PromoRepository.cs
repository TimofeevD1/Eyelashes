using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositorys
{
    internal class PromoRepository(AppContext context) : IPromoRepository
    {
        public async Task<Promo> GetPromoAsync(CancellationToken cancellationToken = default)
        {
            var promo = await context.Promos.FirstOrDefaultAsync(cancellationToken);
            if (promo == null)
            {
                throw new Exception("Promo data not found.");
            }
            return promo;
        }

        public async Task CreatePromoAsync(Promo promo, CancellationToken cancellationToken = default)
        {
            await context.Promos.AddAsync(promo, cancellationToken);
            await context.SaveChangesAsync();
        }

        public async Task UpdatePromoAsync(Promo updatedPromo, CancellationToken cancellationToken = default)
        {
            var existingPromo = await context.Promos.FindAsync(new object[] { updatedPromo.Id }, cancellationToken);
            if (existingPromo == null)
            {
                throw new Exception($"Promo with ID {updatedPromo.Id} not found.");
            }

            existingPromo.Title = updatedPromo.Title;
            existingPromo.OldPrice = updatedPromo.OldPrice;
            existingPromo.NewPrice = updatedPromo.NewPrice;
            existingPromo.DiscountDescription = updatedPromo.DiscountDescription;
            existingPromo.Benefits = updatedPromo.Benefits;

            context.Promos.Update(existingPromo);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeletePromoAsync(int id, CancellationToken cancellationToken = default)
        {
            var promo = await context.Promos.FindAsync(new object[] { id }, cancellationToken);
            if (promo == null)
            {
                throw new Exception($"Promo with ID {id} not found.");
            }

            context.Promos.Remove(promo);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
