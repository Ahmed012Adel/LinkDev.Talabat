using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructrure.Persistence.Data.Repositeries
{
    public class GenericRepositeries<TEntity, TKey>(StoreDbContxt dbContxt) : IGenericRepositeries<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false)
         =>  withTracking ? await dbContxt.Set<TEntity>().ToListAsync() : await dbContxt.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<TEntity?> GetAsync(TKey id)
               =>  await dbContxt.Set<TEntity>().FindAsync();

        public async Task AddAsync(TEntity entity)
            => await dbContxt.Set<TEntity>().AddAsync(entity); 

        public void Update(TEntity entity)
         => dbContxt.Update<TEntity>(entity);

        public void Delete(TEntity entity)
          => dbContxt.Set<TEntity>().Remove(entity);

    }
}
