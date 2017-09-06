using DAL.Context;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class BooksRepository : IRepository<Book>
    {
        private ApplicationContext db;

        public BooksRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await db.Books.ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await db.Books.FindAsync(id);
        }

        public async Task CreateAsync(Book model)
        {
            await Task.Run(() => db.Books.Add(model)).ConfigureAwait(false);
        }

        public void Update(Book model)
        {
            db.Entry(model).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            Book model = await db.Books.FindAsync(id);
            if (model != null)
                db.Books.Remove(model);
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
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
