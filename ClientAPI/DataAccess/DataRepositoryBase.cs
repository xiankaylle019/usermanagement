using ClientAPI.Core;

namespace ClientAPI.DataAccess
{
    public abstract class DataRepositoryBase<TEntity> : 
    DataRepository<TEntity, DataContext> where TEntity : class
    {
       
    }
}