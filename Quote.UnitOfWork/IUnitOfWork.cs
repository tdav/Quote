namespace Arch.EntityFrameworkCore.UnitOfWork
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public interface IUnitOfWork : IDisposable
    {
        void ChangeDatabase(string database);

        IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class;

        int SaveChanges(bool ensureAutoHistory = false);

        Task<int> SaveChangesAsync(bool ensureAutoHistory = false);

        int ExecuteSqlCommand(string sql, params object[] parameters);

        Task<IEnumerable<T>> FromSqlDapperAsync<T>(string sql);
        Task<List<TEntity>> FromSqlAsNoTracking<TEntity>(string sql) where TEntity : class;
        IQueryable<TEntity> FromSql<TEntity>(string sql) where TEntity : class;
        IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class;

        void TrackGraph(object rootEntity, Action<EntityEntryGraphNode> callback);

        void SetForceMode();
        void DetectChanges();
        void DisableForceMode();

        void BeginTransaction();
        Task BeginTransactionAsync();
        void Commit();
        Task CommitAsync();
        void Rollback();
        Task RollbackAsync();
    }
}