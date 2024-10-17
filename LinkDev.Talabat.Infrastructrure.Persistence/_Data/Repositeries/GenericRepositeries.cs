using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Entities.Product;
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
        {

            //if (typeof(TEntity) == typeof(Product))
            //{
            //    return (IEnumerable<TEntity>)(withTracking ? await dbContxt.Set<Product>().Include(p => p.Category).Include(P => P.Brand).ToListAsync() :
            //         await dbContxt.Set<Product>().Include(p => p.Category).Include(P => P.Brand).AsNoTracking().ToListAsync());
            //}

            return withTracking ? await dbContxt.Set<TEntity>().ToListAsync() : 
                await dbContxt.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetAsync(TKey id)
        {
            //    if (typeof(TEntity) == typeof(Product))
            //        return (TEntity)(await dbContxt.Set<Product>().Where(P => P.Id.Equals(id)).Include(p => p.Category).Include(p => p.Brand).FirstOrDefaultAsync() as TEntity;

            return await dbContxt.Set<TEntity>().FindAsync();
        }

        public async Task<IEnumerable<TEntity>> GetWithSpecAllAsync(ISpecification<TEntity, TKey> spec, bool withTracking = false)
        {
            return await ApplyQuery(spec).ToListAsync();
        }

        public async Task<int> GetCountAsync(ISpecification<TEntity, TKey> spec)
        {
            return await ApplyQuery(spec).CountAsync();
        }

        public async Task<TEntity?> GetWithSpecAsync(ISpecification<TEntity, TKey> spec)
        {
            return await ApplyQuery(spec).FirstOrDefaultAsync();
        }


        public async Task AddAsync(TEntity entity)
            => await dbContxt.Set<TEntity>().AddAsync(entity); 

        public void Update(TEntity entity)
         => dbContxt.Update<TEntity>(entity);

        public void Delete(TEntity entity)
          => dbContxt.Set<TEntity>().Remove(entity);



        #region Helpers
        private IQueryable<TEntity> ApplyQuery( ISpecification<TEntity, TKey> spec)
        {
            return SpecificationEvaluter<TEntity, TKey>.GetQuery(dbContxt.Set<TEntity>(),spec);
        }


        #endregion
    }
}
