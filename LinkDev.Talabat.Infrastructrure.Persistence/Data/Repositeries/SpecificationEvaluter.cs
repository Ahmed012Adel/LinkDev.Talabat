using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructrure.Persistence.Data.Repositeries
{
    internal static class SpecificationEvaluter<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> InputQuery , ISpecification<TEntity,TKey> Spec)
        {
            var Query = InputQuery;

            if(Spec.Criteria is not null)
                 Query = Query.Where(Spec.Criteria);

            if(Spec.OrderByDesc is not null) 
                Query = Query.OrderByDescending(Spec.OrderByDesc);
            else if(Spec.OrderBy is not null)
                Query = Query.OrderBy(Spec.OrderBy);

            Query = Spec.Includes.Aggregate(Query , (CurrentQuery , include) => CurrentQuery.Include(include)); 

            return Query;
        }
    }
}
