using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Book> Books { get; }
        IRepository<Category> Categories { get; }
        Task SaveAsync();
    }
}
