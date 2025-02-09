using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    internal class OrderRepository : IOrderRepository
    {
        private readonly AppContext _context;

        public OrderRepository(AppContext context)
        {
            _context = context;
        }

        public async Task<Order> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            var order = await _context.Orders
                                       .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

            if (order == null)
            {
                throw new Exception($"Order with ID {id} not found.");
            }

            return order;
        }

        public async Task CreateAsync(Order order, CancellationToken cancellationToken = default)
        {
            await _context.Orders.AddAsync(order, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> GetCountAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Orders.CountAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var order = await _context.Orders.FindAsync(new object[] { id }, cancellationToken);
            if (order == null)
            {
                throw new Exception($"Order with ID {id} not found.");
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Order updatedOrder, CancellationToken cancellationToken = default)
        {
            var existingOrder = await _context.Orders.FindAsync(new object[] { updatedOrder.Id }, cancellationToken);
            if (existingOrder == null)
            {
                throw new Exception($"Order with ID {updatedOrder.Id} not found.");
            }

            existingOrder.DesiredDate = updatedOrder.DesiredDate;
            existingOrder.Name = updatedOrder.Name;
            existingOrder.PhoneNumber = updatedOrder.PhoneNumber;
            existingOrder.Service = updatedOrder.Service;
            existingOrder.Status = updatedOrder.Status;

            _context.Orders.Update(existingOrder);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Order>> GetAllOrdersAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Orders.ToListAsync(cancellationToken);
        }

        public async Task SetOrderStatusToCreatedAsync(int orderId, CancellationToken cancellationToken = default)
        {
            var order = await _context.Orders.FindAsync(new object[] { orderId }, cancellationToken);
            if (order == null)
            {
                throw new Exception($"Order with ID {orderId} not found.");
            }

            order.Status = OrderStatus.Created;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task SetOrderStatusToConfirmedAsync(int orderId, CancellationToken cancellationToken = default)
        {
            var order = await _context.Orders.FindAsync(new object[] { orderId }, cancellationToken);
            if (order == null)
            {
                throw new Exception($"Order with ID {orderId} not found.");
            }

            order.Status = OrderStatus.Confirmed;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
