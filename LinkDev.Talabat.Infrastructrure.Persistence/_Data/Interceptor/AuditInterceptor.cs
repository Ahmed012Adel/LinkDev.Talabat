using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Domain.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LinkDev.Talabat.Infrastructrure.Persistence.Data.Interceptor
{

    // Interceptor
    internal class AuditInterceptor : SaveChangesInterceptor
    {
        private readonly ILoggedUserInService _loggedUserInService;

        public AuditInterceptor(ILoggedUserInService loggedUserInService)
        {
            _loggedUserInService = loggedUserInService;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntites(eventData.Context);

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntites(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntites(DbContext? dbContext)
        {
            if (dbContext is null) return;

            var UTC = DateTime.UtcNow;

            foreach(var Entry in dbContext.ChangeTracker.Entries<IBaseAuditableEntity>()
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
