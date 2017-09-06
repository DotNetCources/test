using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Context;

namespace DAL.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;
        private BooksRepository booksRepository;
        private CategoriesRepository categoriesRepository;

        public UnitOfWork()
         {
             db = new ApplicationContext("DefaultConnection");
         }
        public UnitOfWork(string connectionString)
        {
            db = new ApplicationContext(connectionString);
        }
        public IRepository<Book> Books
        {
            get
            {
                if (booksRepository == null)
                    booksRepository = new BooksRepository(db);
                return booksRepository;
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                if (categoriesRepository == null)
                    categoriesRepository = new CategoriesRepository(db);
                return categoriesRepository;
            }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

