using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ClientAPI.Core.Shared.Contracts;
using ClientAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace ClientAPI.Core
{
    public abstract class DataRepository<TEntity, TContext> : 
    DesignTimeDbContextFactory<TContext>,IDataRepository<TEntity>, IDisposable
    where TEntity : class
    where TContext: DbContext
    {
        
        private bool _disposed = false;
        private readonly TContext _dbContext;
        protected abstract Task<TEntity> GetEntityById(DbContext dbContext,  int id);
        public DataRepository()
        {
            _dbContext = GetDbContextInstance();            
        }
        protected virtual TContext GetDbContextInstance()
        {            
            return CreateDbContext(null);
        }       

        public TEntity AddEntity(TEntity entity)
        {
            EntityEntry<TEntity> result; //this EntityEntry need for ChangeTracking

            result = _dbContext.Add(entity);

            return result?.Entity;
        }
        
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var dbSet = _dbContext.Set<TEntity>();

            if (dbSet == null) return null;                    

            var result = await dbSet.AsNoTracking().ToListAsync().ConfigureAwait(false);

            return result;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
             return await GetEntityById(_dbContext, id);          
        }

        public async Task<bool> FindAnyEntityAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var dbSet = _dbContext.Set<TEntity>();

            if (dbSet == null)  return false;                   

            var result = await dbSet.AsNoTracking().AnyAsync(predicate);

            return result;
        }

        public IEnumerable<TEntity> SearchBy(Func<TEntity, bool> predicate)
        {
              var dbSet = _dbContext.Set<TEntity>();

                if (dbSet == null)
                    return null;

                var result = dbSet.AsNoTracking().Where(predicate);

                return result;
        }

        public TEntity UpdateEntityAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            EntityEntry<TEntity> result;

            result = _dbContext.Update<TEntity>(entity);         

            return result?.Entity;

        }

        public async Task<bool> Save()
        {          
            int changes = 0;
    
             using (IDbContextTransaction _transaction = await _dbContext.Database.BeginTransactionAsync().ConfigureAwait(false)){
               
                try {                

                    changes = await _dbContext.SaveChangesAsync().ConfigureAwait(false);

                    _transaction.Commit();

                }
                catch(Exception){

                     _transaction.Rollback();                    

                }finally{

                    Dispose();
                }
            }
            return (changes > 0 ? true : false);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}