using DataAccessLayer.Context;
using DataAccessLayer.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _set;

        public EfRepository(ApplicationDbContext context)
        {
            _context = context;
            _set = context.Set<T>();
        }

        public IQueryable<T> Query() => _set.AsQueryable();

        public async Task<T?> GetByIdAsync(Guid id)
            => await _set.FindAsync(id);

        public async Task AddAsync(T entity)
            => await _set.AddAsync(entity);

        public void Update(T entity)
            => _set.Update(entity);

        public void Remove(T entity)
            => _set.Remove(entity);

        public Task<int> SaveChangesAsync()
            => _context.SaveChangesAsync();
    }
}