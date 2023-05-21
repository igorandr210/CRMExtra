using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Common;

namespace Application.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        public Task<List<TEntity>> GetAllAsync();
        public Task<TEntity> GetByIdAsync(Guid id);
        public Task<TEntity> InsertAsync(TEntity entity);
        public Task DeleteByIdAsync(Guid id);
        public void Delete(TEntity entity);
        public TEntity Update(TEntity entity);
        public Task SaveChangesAsync();
    }
}
