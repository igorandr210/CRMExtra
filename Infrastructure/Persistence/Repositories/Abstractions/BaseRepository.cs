using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Abstractions
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly IApplicationDbContext _context;
        protected DbSet<TEntity> DbSet { get; }

        protected BaseRepository(IApplicationDbContext context)
        {
            _context = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual Task<List<TEntity>> GetAllAsync()
        {
            return DbSet.ToListAsync();
        }

        public virtual Task<TEntity> GetByIdAsync(Guid id)
        {
            return DbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<TEntity> InsertAsync(TEntity entity)
        {
            return DbSet.AddAsync(entity).AsTask().ContinueWith(x => x.Result.Entity);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
            DbSet.Remove(entity);
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public virtual TEntity Update(TEntity entity)
        {
            return DbSet.Update(entity).Entity;
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}