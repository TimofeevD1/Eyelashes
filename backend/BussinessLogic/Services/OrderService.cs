using BussinessLogic.Interfaces;
using BussinessLogic.Records;
using DataAccess.Models;
using DataAccess.Interfaces;

namespace BussinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderRec> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var order = await _orderRepository.GetAsync(id, cancellationToken);

            return new OrderRec
            {
                Id = order.Id,
                DesiredDate = order.DesiredDate,
                Name = order.Name,
                PhoneNumber = order.PhoneNumber,
                Service = order.Service,
                Status = (int)order.Status
            };
        }

        public async Task CreateAsync(OrderRec orderRec, CancellationToken cancellationToken = default)
        {
            var order = new Order
            {
                DesiredDate = orderRec.DesiredDate,
                Name = orderRec.Name,
                PhoneNumber = orderRec.PhoneNumber,
                Service = orderRec.Service,
                Status = (OrderStatus)orderRec.Status
            };

            await _orderRepository.CreateAsync(order, cancellationToken);
        }

        public async Task<List<OrderRec>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var orders = await _orderRepository.GetAllOrdersAsync(cancellationToken);

            return orders.Select(order => new OrderRec
            {
                Id = order.Id,
                DesiredDate = order.DesiredDate,
                Name = order.Name,
                PhoneNumber = order.PhoneNumber,
                Service = order.Service,
                Status = (int)order.Status
            }).ToList();
        }

        public async Task UpdateAsync(OrderRec orderRec, CancellationToken cancellationToken = default)
        {
            var updatedOrder = new Order
            {
                Id = orderRec.Id,
                DesiredDate = orderRec.DesiredDate,
                Name = orderRec.Name,
                PhoneNumber = orderRec.PhoneNumber,
                Service = orderRec.Service,
                Status = (OrderStatus)orderRec.Status 
            };

            await _orderRepository.UpdateAsync(updatedOrder, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await _orderRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task SetStatusToCreatedAsync(int orderId, CancellationToken cancellationToken = default)
        {
            await _orderRepository.SetOrderStatusToCreatedAsync(orderId, cancellationToken);
        }

        public async Task SetStatusToConfirmedAsync(int orderId, CancellationToken cancellationToken = default)
        {
            await _orderRepository.SetOrderStatusToConfirmedAsync(orderId, cancellationToken);
        }
    }
}
