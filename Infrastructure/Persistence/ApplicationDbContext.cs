using Application.Common.Interface;
using Domain.Common;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly DateTime _currentDateTime;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            _currentDateTime = DateTime.Now;
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public Task<int> SaveChangesAsync()
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = _currentDateTime;
                        entry.Entity.ModifiedDate = _currentDateTime;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedDate = _currentDateTime;
                        entry.Entity.CreatedDate = entry.Entity.CreatedDate;
                        break;
                }
            }
            return base.SaveChangesAsync();
        }

        public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            modelBuilder.Entity<Product>().Property(e => e.Id).ValueGeneratedOnAdd();
        }
    }
}
