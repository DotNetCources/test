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
    public class CategoriesRepository : IRepository<Category>
    {
        private ApplicationContext db;

        public CategoriesRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await db.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await db.Categories.FindAsync(id);
        }

        public async Task CreateAsync(Category model)
        {
            await Task.Run(() => db.Categories.Add(model)).ConfigureAwait(false);
        }

        public void Update(Category model)
        {
            db.Entry(model).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            Category model = await db.Categories.FindAsync(id);
            if (model != null)
                db.Categories.Remove(model);
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
