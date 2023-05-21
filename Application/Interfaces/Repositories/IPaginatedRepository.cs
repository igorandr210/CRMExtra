using Application.Common.DTOs;
using System.Threading.Tasks;
using Domain.Common;

namespace Application.Interfaces.Repositories
{
    public interface IPaginatedRepository<TEntity, in TPaginationInfo> :IBaseRepository<TEntity>
        where TEntity : BaseEntity
        where TPaginationInfo: PaginationInfoDto
    {
        Task<PaginatedDataDto<TEntity>> GetPaginatedDataAsync(TPaginationInfo data);
    }
}