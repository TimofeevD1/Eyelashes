﻿using BussinessLogic.Interfaces;
using BussinessLogic.Records;
using EyelashesAPI.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EyelashesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("get/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetByIdAsync(id, cancellationToken);
            if (order == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }
            return Ok(order);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetAllAsync(cancellationToken);
            return Ok(orders);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] OrderRequest orderRequest, CancellationToken cancellationToken)
        {
            if (orderRequest == null)
            {
                return BadRequest("Invalid order data.");
            }

            var orderRec = new OrderRec
            {
                DesiredDate = orderRequest.DesiredDate,
                Name = orderRequest.Name,
                PhoneNumber = orderRequest.PhoneNumber,
                Service = orderRequest.Service,
                Status = orderRequest.Status
            };

            await _orderService.CreateAsync(orderRec, cancellationToken);
            return Ok("Order has been created successfully.");
        }

        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] OrderRequest orderRequest, CancellationToken cancellationToken)
        {
            var existingOrder = await _orderService.GetByIdAsync(id, cancellationToken);
            if (existingOrder == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }

            var updatedOrderRec = new OrderRec
            {
                Id = id,
                DesiredDate = orderRequest.DesiredDate,
                Name = orderRequest.Name,
                PhoneNumber = orderRequest.PhoneNumber,
                Service = orderRequest.Service,
                Status = orderRequest.Status
            };

            await _orderService.UpdateAsync(updatedOrderRec, cancellationToken);
            return Ok($"Order with ID {id} has been updated successfully.");
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var existingOrder = await _orderService.GetByIdAsync(id, cancellationToken);
            if (existingOrder == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }

            await _orderService.DeleteAsync(id, cancellationToken);
            return Ok($"Order with ID {id} has been deleted successfully.");
        }

        [HttpPost("{id:int}/setstatus/created")]
        public async Task<IActionResult> SetStatusToCreatedAsync(int id, CancellationToken cancellationToken)
        {
            var existingOrder = await _orderService.GetByIdAsync(id, cancellationToken);
            if (existingOrder == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }

            await _orderService.SetStatusToCreatedAsync(id, cancellationToken);
            return Ok($"Order with ID {id} status has been set to 'Created'.");
        }

        [HttpPost("{id:int}/setstatus/confirmed")]
        public async Task<IActionResult> SetStatusToConfirmedAsync(int id, CancellationToken cancellationToken)
        {
            var existingOrder = await _orderService.GetByIdAsync(id, cancellationToken);
            if (existingOrder == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }

            await _orderService.SetStatusToConfirmedAsync(id, cancellationToken);
            return Ok($"Order with ID {id} status has been set to 'Confirmed'.");
        }
    }
}
