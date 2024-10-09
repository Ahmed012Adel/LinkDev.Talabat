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

            foreach(var Entry in dbContext.ChangeTracker.Entries<BaseAuditableEntity<int>>()
                .Where(Entery => Entery.State is EntityState.Added or EntityState.Modified))
            {
                if(Entry.State is EntityState.Added  )
                {
                    Entry.Entity.CreatedBy = _loggedUserInService.UserId;
                        Entry.Entity.CreatedOn = UTC;
                    
                } 
                    Entry.Entity.LastModifiedBy = _loggedUserInService.UserId;
                    Entry.Entity.LastModifiedOn = UTC;
            }
        }
    }
}
