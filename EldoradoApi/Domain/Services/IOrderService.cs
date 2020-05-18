using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EldoradoApi.Domain.Models;
using EldoradoApi.Domain.Services.Communication;
using EldoradoApi.Models;

namespace EldoradoApi.Domain.Services
{
    public interface IOrderService
    {
        Task<PagedList<Order>> ListAllAsync(OrderParameters orderParameters);
        Task<OrderResponse> SaveAsync(Order order);
        Task<OrderResponse> UpdateAsync(long id, Order order);
        Task<OrderResponse> DeleteAsync(long id);
        Task<OrdersResponse> RemoveExpiredOrders();
    }
}
