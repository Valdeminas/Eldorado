using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EldoradoApi.Domain.Models;
using EldoradoApi.Domain.Repositories;
using EldoradoApi.Domain.Services;
using EldoradoApi.Domain.Services.Communication;
using EldoradoApi.Models;

namespace EldoradoApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
		private readonly IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository,IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
			_unitOfWork = unitOfWork;
        }

		public async Task<PagedList<Order>> ListAllAsync(OrderParameters orderParameters)
		{
			return await _orderRepository.ListAllAsync(orderParameters);
		}

		public async Task<OrderResponse> SaveAsync(Order order)
		{
			try
			{
				await _orderRepository.AddAsync(order);
				await _unitOfWork.CompleteAsync();

				return new OrderResponse(order);
			}
			catch (Exception ex)
			{
				// Do some logging stuff
				return new OrderResponse($"An error occurred when saving the order: {ex.Message}");
			}
		}

		public async Task<OrderResponse> UpdateAsync(long id, Order order)
		{
			var existingOrder = await _orderRepository.FindByIdAsync(id);

			if (existingOrder == null)
				return new OrderResponse("Order not found.");

			existingOrder.Completed = order.Completed;
			existingOrder.Paid = order.Paid;

			try
			{
				_orderRepository.Update(existingOrder);
				await _unitOfWork.CompleteAsync();

				return new OrderResponse(existingOrder);
			}
			catch (Exception ex)
			{
				// Do some logging stuff
				return new OrderResponse($"An error occurred when updating the order: {ex.Message}");
			}
		}

		public async Task<OrderResponse> DeleteAsync(long id)
		{
			var existingOrder = await _orderRepository.FindByIdAsync(id);

			if (existingOrder == null)
				return new OrderResponse("Order not found.");

			try
			{
				_orderRepository.Remove(existingOrder);
				await _unitOfWork.CompleteAsync();

				return new OrderResponse(existingOrder);
			}
			catch (Exception ex)
			{
				// Do some logging stuff
				return new OrderResponse($"An error occurred when deleting the order: {ex.Message}");
			}
		}

        public async Task<OrdersResponse> RemoveExpiredOrders()
        {

			var expiredOrders = await _orderRepository.GetExpiredOrders();
			try
			{
				_orderRepository.RemoveExpiredOrders(expiredOrders);
				await _unitOfWork.CompleteAsync();

				return new OrdersResponse(expiredOrders); ;
			}
			catch (Exception ex)
			{
				// Do some logging stuff
				return new OrdersResponse($"An error occurred when deleting expired orders: {ex.Message}");
			}
		}
	}
}
