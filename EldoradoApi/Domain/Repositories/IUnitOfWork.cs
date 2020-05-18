using System;
using System.Threading.Tasks;

namespace EldoradoApi.Domain.Repositories
{

        public interface IUnitOfWork
        {
            Task CompleteAsync();
        }
}
