using System;
using System.Threading.Tasks;
using EldoradoApi.Domain.Repositories;
using EldoradoApi.Persistence.Contexts;

namespace EldoradoApi.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
