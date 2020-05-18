using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EldoradoApi.Domain.Models;
using EldoradoApi.Models;

namespace EldoradoApi.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<PagedList<Order>> ListAllAsync(OrderParameters orderParameters);
        Task AddAsync(Order order);
        Task<Order> FindByIdAsync(long id);
        void Update(Order order);
        void Remove(Order order);
        void RemoveExpiredOrders(IEnumerable<Order> expiredOrders);
        Task<IEnumerable<Order>> GetExpiredOrders();

    }
}
