using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace Fakir.EntityFramework
{
    public abstract class FakirRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<FakirDbContext, TEntity, TPrimaryKey>
           where TEntity : class, IEntity<TPrimaryKey>
    {
        protected FakirRepositoryBase(IDbContextProvider<FakirDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }
    }

    public abstract class FakirRepositoryBase<TEntity> : FakirRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected FakirRepositoryBase(IDbContextProvider<FakirDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }
    }
}
