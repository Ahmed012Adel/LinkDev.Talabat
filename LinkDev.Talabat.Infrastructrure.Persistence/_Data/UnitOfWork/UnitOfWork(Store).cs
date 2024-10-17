using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Infrastructrure.Persistence.Data.Repositeries;
using System.Collections.Concurrent;

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
