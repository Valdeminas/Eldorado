using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EldoradoApi.Domain.Models;
using EldoradoApi.Domain.Repositories;
using EldoradoApi.Models;
using EldoradoApi.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EldoradoApi.Persistence.Repositories
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PagedList<Order>> ListByUserAsync(long userId, OrderParameters orderParameters)
        {
            return PagedList<Order>.ToPagedList(await _context.Orders.Where(x => x.UserId == userId).ToListAsync(),
                                            orderParameters.PageNumber,
                                            orderParameters.PageSize);
        }

        public async Task<PagedList<Order>> ListAllAsync(OrderParameters orderParameters)
        {
            var result = await _context.Orders.ToListAsync();
            if (orderParameters.UserId != null)
            {
                result = result.Where(p => p.UserId == orderParameters.UserId).ToList();
            }

            return PagedList<Order>.ToPagedList(result,
                                            orderParameters.PageNumber,
                                            orderParameters.PageSize);
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);

        }

        public async Task<Order> FindByIdAsync(long id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
        }

        public void Remove(Order order)
        {
            _context.Orders.Remove(order);
        }

        public async Task<IEnumerable<Order>> GetExpiredOrders()
        {
            return await _context.Orders.Where(p => DateTime.Compare(p.ExpiryDate, DateTime.UtcNow)<0).ToListAsync();
        }


        public void RemoveExpiredOrders(IEnumerable<Order> expiredOrders)
        {
            _context.Orders.RemoveRange(expiredOrders);
        }
    }
}
