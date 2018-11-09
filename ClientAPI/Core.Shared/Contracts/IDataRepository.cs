using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ClientAPI.Core.Shared.Contracts
{    
    public interface IDataRepository<TEntity> where TEntity : class {
         
        TEntity AddEntity(TEntity entity);           
        Task<IEnumerable<TEntity>> GetAllAsync ();
        Task<TEntity> GetByIdAsync (int id);
        IEnumerable<TEntity> SearchBy (Func<TEntity, bool> predicate);
        Task<bool> FindAnyEntityAsync (Expression<Func<TEntity, bool>> predicate);
        TEntity UpdateEntityAsync (TEntity entity);
        Task<bool> Save();
    }
}