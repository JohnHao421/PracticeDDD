using FindSelf.Domain.Entities.UserAggregate;
using FindSelf.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FindSelf.Infrastructure.DomainEventBus;
using System.Linq;
using Microsoft.Extensions.Logging;
using FindSelf.Infrastructure.Repositories;
using FindSelf.Infrastructure.Database.EntityConfigurations;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using FindSelf.Infrastructure.Idempotent;

namespace FindSelf.Infrastructure.Database
{
    public class FindSelfDbContext : DbContext, IUnitOfWork
    {
        private readonly IDomainEventsDispatcher eventsDispatcher;

        public FindSelfDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        public FindSelfDbContext([NotNull] DbContextOptions options, IDomainEventsDispatcher domainEventsDispatcher) : base(options)
        {
            eventsDispatcher = domainEventsDispatcher;
        }

        public DbSet<IdempotentRequest> IdempotentRequests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MessageBox> MessageBoxes { get; set; }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            var domainEntities = ChangeTracker.Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .Select(x => x.Entity);

            await eventsDispatcher.DispatchEventsAsync(domainEntities);
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEntityTypeConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }


        public IDbContextTransaction CurrentTranscation => _currentTransaction;
        private IDbContextTransaction _currentTransaction;

        public bool HasTranscation => _currentTransaction != null;

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;
            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            return _currentTransaction;
        }

        public async Task CommitTranscationAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                RollbackTranscation();
                throw;
            }
            finally
            {
                if (HasTranscation)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        private void RollbackTranscation()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}