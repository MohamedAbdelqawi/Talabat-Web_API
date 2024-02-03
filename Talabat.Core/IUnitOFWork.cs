using Talabat.Core.Entities;
using Talabat.Core.IRepositories;

namespace Talabat.Core
{
    public interface IUnitOFWork : IAsyncDisposable
    {
        IGenericRepositry<TEntity>? Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}
