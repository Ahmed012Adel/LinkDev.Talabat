using LinkDev.Talabat.Core.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Contracts
{
    public interface IUniteOfWork : IAsyncDisposable
    {

        IGenericRepositeries<TEntity, Tkey> GetRepoitery<TEntity, Tkey>()
            where TEntity : BaseEntity<Tkey>
            where Tkey : IEquatable<Tkey>;


        Task<int> ComplateAsync();
    }
}
