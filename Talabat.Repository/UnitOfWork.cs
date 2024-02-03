using System.Collections;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOFWork
    {
        private readonly StoreContext _dbContext;

        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Complete()
       => await _dbContext.SaveChangesAsync();


        public async ValueTask DisposeAsync()
        => await _dbContext.DisposeAsync();



        public Hashtable _Repositories { get; set; }
        public IGenericRepositry<TEntity>? Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_Repositories is null) _Repositories = new Hashtable();

            var type = typeof(TEntity).Name;
            if (!_Repositories.ContainsKey(type))
            {
                var Repository = new GenericRepositry<TEntity>(_dbContext);
                _Repositories.Add(type, Repository);
            }

            return _Repositories[type] as IGenericRepositry<TEntity>;
        }
    }
}
