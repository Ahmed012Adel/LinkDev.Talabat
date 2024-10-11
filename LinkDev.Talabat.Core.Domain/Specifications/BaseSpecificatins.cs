using LinkDev.Talabat.Core.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specifications
{
    public class BaseSpecificatins<TEntity, TKey> : ISpecification<TEntity, TKey>
        where TEntity : BaseAuditableEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; set; }
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new();
        public Expression<Func<TEntity, object>>? OrderBy { get; set; } = null;
        public Expression<Func<TEntity, object>>? OrderByDesc { get; set; } = null;
        public int Skip { get ; set ; }
        public int Take { get ; set ; }
        public bool IsPaginationPage { get; set ; }

        public BaseSpecificatins(Expression<Func<TEntity, bool>>? expression)
        {
            Criteria = expression;
            
        }

        public BaseSpecificatins(int id)
        {
            Criteria = E => E.Id.Equals(id);
        }

        private protected virtual void AddOrderBy(Expression<Func<TEntity, object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;
        }
        private protected virtual void AddOrderByDesc(Expression<Func<TEntity, object>> OrderByExpressionDesc)
        {
            OrderByDesc = OrderByExpressionDesc;
        }
        private protected virtual void AddIncludes()
        {
           
        }
        private protected void AddPagination(int Skip, int Take) 
        {
            IsPaginationPage = true;
            this.Skip = Skip;
            this.Take = Take;
        }
    }
}
