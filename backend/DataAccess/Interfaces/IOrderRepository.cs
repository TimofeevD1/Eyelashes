using DataAccess.Models;

namespace DataAccess.Interfaces
{
    public interface IOrderRepository
    {
        Task CreateAsync(Order order, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Order>> GetAllOrdersAsync(CancellationToken cancellationToken = default);
        Task<Order> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<int> GetCountAsync(CancellationToken cancellationToken = default);
        Task SetOrderStatusToConfirmedAsync(int orderId, CancellationToken cancellationToken = default);
        Task SetOrderStatusToCreatedAsync(int orderId, CancellationToken cancellationToken = default);
        Task UpdateAsync(Order updatedOrder, CancellationToken cancellationToken = default);
    }
}