using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Contracts
{
    public interface IDbIntializer
    {

         Task UpdateDatabaseAsync();

        Task DataSeedAsync();
    }
}
