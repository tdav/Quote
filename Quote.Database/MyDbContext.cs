using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quote.Database.Models;
using Quote.Global;
using Quote.Utils;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Quote.Database
{
    public partial class MyDbContext : DbContext
    {
        private readonly IHttpContextAccessor accessor; 

        public MyDbContext(DbContextOptions<MyDbContext> options, IHttpContextAccessor _accessor) : base(options)
        {
            accessor = _accessor;

            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        #region Declare

      
        public DbSet<spRole> spRoles { get; set; }
       
       
        public DbSet<tbUser> tbUsers { get; set; }



        #endregion

        #region Audit Base Model
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            AuditEvent();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            AuditEvent();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AuditEvent();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            AuditEvent();
            return base.SaveChanges();
        }

        public int GetId()
        {
            var user = accessor.HttpContext.User.FindFirst(ClaimTypes.Sid);
            return user.Value.ToInt();
        }

        private void AuditEvent()
        {
            ChangeTracker.DetectChanges();

            var addList = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);

            var UserId = GetId();

            foreach (var item in addList)
            {
                if (item.Entity is BaseModel entity)
                {
                    //entity.Status = 1;
                    entity.CreateUser = UserId;
                    entity.CreateDate = DateTime.Now;

                    entity.UpdateUser = UserId;
                    entity.UpdateDate = DateTime.Now;
                }
            }

            var updateList = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);

            foreach (var item in updateList)
            {
                if (item.Entity is BaseModel entity)
                {
                    entity.UpdateUser = UserId;
                    entity.UpdateDate = DateTime.Now;
                }
            }
        }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // modelBuilder.BuildIndexesFromAnnotations();

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}