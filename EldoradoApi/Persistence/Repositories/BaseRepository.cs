using System;
using EldoradoApi.Persistence.Contexts;

namespace EldoradoApi.Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}
