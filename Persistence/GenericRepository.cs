using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Persistence
{
    public class GenericRepository<T>(AppDbContext appDbContext) : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _appDbContext = appDbContext;
        protected readonly DbSet<T> _dbSet = appDbContext.Set<T>();

        public async ValueTask AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);

        public IQueryable<T> GetAll() => _dbSet.AsQueryable().AsNoTracking();

        public async ValueTask<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public void Update(T entity) => _dbSet.Update(entity);

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate).AsQueryable().AsNoTracking();
    }
}
