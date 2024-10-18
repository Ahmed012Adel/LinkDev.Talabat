using LinkDev.Talabat.Core.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructrure.Persistence.Common
{
    public  class DbIntializer(DbContext dbContext) : IDbIntializer
    {
        public virtual async Task DataSeedAsync()
        {
            var PendingMigration = await dbContext.Database.GetPendingMigrationsAsync();
            if (PendingMigration.Any())
                await dbContext.Database.MigrateAsync();
        }

        public virtual Task UpdateDatabaseAsync()
        {
            throw new NotImplementedException();
        }
    }
}
