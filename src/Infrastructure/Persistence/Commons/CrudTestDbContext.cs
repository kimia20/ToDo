using Application.Common.Interfaces;
using ToDo.Domain.Common;
using ToDo.Domain.CustomerAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Commons
{
    public class ToDoDbContext : DbContext, IToDoDbContext
    {
        private readonly IMediator _mediator;
        public ToDoDbContext(
       DbContextOptions<ToDoDbContext> options,
       IMediator mediator)
       : base(options)
        {
            _mediator = mediator;
        }
        public DbSet<CustomerItem> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var modifiedEntities = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified || p.State == EntityState.Deleted || p.State == EntityState.Added).ToList();

            foreach (var entry in ChangeTracker.Entries<BaseAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            }
           
            await DispatchEvent(ChangeTracker);
            var changeCount =await base.SaveChangesAsync(cancellationToken);
            return changeCount;
        }

        private async Task DispatchEvent(ChangeTracker changeTracker)
        {
            var entities = changeTracker
           .Entries<BaseEntity>()
           .Where(e => e.Entity.DomainEvents.Any())
           .Select(e => e.Entity);

            var domainEvents = entities
                .SelectMany(e => e.DomainEvents)
                .ToList();

            entities.ToList().ForEach(e => e.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await _mediator.Publish(domainEvent);
        }
    }
}
