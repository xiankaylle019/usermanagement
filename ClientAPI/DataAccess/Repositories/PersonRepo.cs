using System.Threading.Tasks;
using ClientAPI.DataAccess.Contracts;
using ClientAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientAPI.DataAccess.Repositories
{
    public class PersonRepo : DataRepositoryBase<Person>, IPersonRepo
    {
        public async Task<bool> IsUserExist(string username)
        {
            bool isExist = await FindAnyEntityAsync (e => e.Username == username) ? true : false;

            return isExist;
        }

        public async Task<Person> Register(Person person)
        {
            var newUser = AddEntity(person);
                      
            await Save();

            return (newUser != null ? newUser : null);

        }

        protected override async Task<Person> GetEntityById(DbContext dbContext, int id)
        {
            var dbSet = dbContext.Set<Person> ();

            var data = await dbSet.AsNoTracking().SingleOrDefaultAsync(x => x.PersonId == id).ConfigureAwait(false);

            return data;
        }
    }
}