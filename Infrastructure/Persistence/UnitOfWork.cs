using Application.Common.Interface;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Persistence
{
    internal class UnitOfWork : IUnitOfWork
    {
        #region Properties
        private readonly ApplicationDbContext _context;
        IDbContextTransaction dbContextTransaction;
        private IRepository<Product> _productRepo;
        private IRepository<Category> _categoryRepo;
        #endregion
        #region Ctor
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Repository/*
        public IRepository<Product> ProductRepo
        {
            get
            {
                if (this._productRepo == null)
                    this._productRepo = new EfRepository<Product>(_context);
                return _productRepo;
            }
        }
        public IRepository<Category> CategoryRepo
        {
            get
            {
                if (this._categoryRepo == null)
                    this._categoryRepo = new EfRepository<Category>(_context);
                return _categoryRepo;
            }
        }
        #endregion
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
        public void BeginTransaction()
        {
            dbContextTransaction = _context.Database.BeginTransaction();
        }
        public void CommitTransaction()
        {
            if (dbContextTransaction != null)
            {
                dbContextTransaction.Commit();
            }
        }
        public void RollbackTransaction()
        {
            if (dbContextTransaction != null)
            {
                dbContextTransaction.Rollback();
            }
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
