﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Persistence.Interceptors
{
    public class AuditDbContextInterceptor : SaveChangesInterceptor
    {
        private static readonly Dictionary<EntityState, Action<DbContext, IAuditEntity>> Behaviors = new()
        {
            {EntityState.Added, AddBehavior },
            {EntityState.Modified, UpdateBehavior },
        };

        private static void AddBehavior(DbContext context, IAuditEntity auditEntity)
        {
            auditEntity.CreatedDate = DateTime.Now;
            context.Entry(auditEntity).Property(x => x.UpdatedDate).IsModified = false;
        }
        private static void UpdateBehavior(DbContext context, IAuditEntity auditEntity)
        {
            auditEntity.UpdatedDate = DateTime.Now;
            context.Entry(auditEntity).Property(x => x.CreatedDate).IsModified = false;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            foreach (var entityEntry in eventData.Context!.ChangeTracker.Entries().ToList())
            {
                if (entityEntry.Entity is not IAuditEntity auditEntity) continue;

                if (entityEntry.State is not EntityState.Added or EntityState.Modified) continue;

                Behaviors[entityEntry.State](eventData.Context, auditEntity);
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
