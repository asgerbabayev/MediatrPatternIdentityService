using Code.Domain.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Code.Infrastructure.Persistance.Interceptors
{
    public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly IDateTime _dateTime;
        private readonly IHttpContextAccessor _accessor;

        public AuditableEntitySaveChangesInterceptor(
            IDateTime dateTime,
            IHttpContextAccessor contextAccessor)
        {
            this._dateTime = dateTime;
            _accessor = contextAccessor;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntities(DbContext? context)
        {
            var user = _accessor.HttpContext?.User.Identity?.Name;
            if (context == null) return;

            foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = _dateTime.Now;
                    entry.Entity.CreatedBy = user;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedDate = _dateTime.Now;
                    entry.Entity.LastModifedBy = user;
                }
            }
        }
    }
}
