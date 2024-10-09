using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Entities.Product;
using LinkDev.Talabat.Infrastructrure.Persistence.Data.Repositeries;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructrure.Persistence.Data.UnitOfWork
{
    public class UnitOfWork_Store_ : IUniteOfWork
    {
        private readonly StoreDbContxt dbContxt;

        private readonly ConcurrentDictionary<string, object> _repositry;


        public UnitOfWork_Store_(StoreDbContxt dbContxt)
        {
            this.dbContxt = dbContxt; 
            _repositry = new ();
        }
        public async Task ComplateAsync()
          => await dbContxt.SaveChangesAsync();

        public async ValueTask DisposeAsync() => await dbContxt.DisposeAsync();

        public IGenericRepositeries<TEntity, Tkey> GetRepoitery<TEntity, Tkey>()
            where TEntity : BaseEntity<Tkey>
            where Tkey : IEquatable<Tkey>
        {
            /// var TypeName = typeof(TEntity).Name;
            /// if (_repositry.ContainsKey(TypeName)) return (IGenericRepositeries<TEntity, Tkey>) _repositry[TypeName];
            /// 
            /// var repositry = new GenericRepositeries<TEntity, Tkey>(dbContxt);
            /// _repositry.Add(TypeName, repositry);
            /// 
            /// return repositry;

            return (IGenericRepositeries<TEntity, Tkey>) _repositry.GetOrAdd(typeof(TEntity).Name, new GenericRepositeries<TEntity, Tkey>(dbContxt));
        }
    }
}
