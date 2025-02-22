using BussinessLogic.Records;

namespace BussinessLogic.Interfaces
{
    public interface IOrderService
    {
        Task CreateAsync(OrderRec orderRec, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<List<OrderRec>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<OrderRec> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task SetStatusToConfirmedAsync(int orderId, CancellationToken cancellationToken = default);
        Task SetStatusToCancelledAsync(int orderId, CancellationToken cancellationToken = default);
        Task UpdateAsync(OrderRec orderRec, CancellationToken cancellationToken = default);
    }
}