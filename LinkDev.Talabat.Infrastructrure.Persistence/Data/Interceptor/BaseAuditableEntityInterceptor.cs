using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Domain.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LinkDev.Talabat.Infrastructrure.Persistence.Data.Interceptor
{

    // Interceptor
    internal class BaseAuditableEntityInterceptor : SaveChangesInterceptor
    {
        private readonly ILoggedUserInService _loggedUserInService;

        public BaseAuditableEntityInterceptor(ILoggedUserInService loggedUserInService)
        {
            _loggedUserInService = loggedUserInService;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntites(eventData.Context);

            return base.SavingChanges(eventData, result);
        }
        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            UpdateEntites(eventData.Context);
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntites(DbContext? dbContext)
        {
            if (dbContext is null) return;

            var UTC = DateTime.UtcNow;

            foreach(var Entry in dbContext.ChangeTracker.Entries<BaseAuditableEntity<int>>())
            {
                if(Entry is {State: EntityState.Added or EntityState.Modified })
                {
                    if(Entry.State == EntityState.Added)
                    {
                        Entry.Entity.CreatedBy = "";
                        Entry.Entity.CreatedOn = UTC;
                    }

                    Entry.Entity.LastModifiedBy = "";
                    Entry.Entity.LastModifiedOn = UTC;
                } 
            }
        }
    }
}
