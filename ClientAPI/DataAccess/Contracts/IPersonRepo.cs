using System.Threading.Tasks;
using ClientAPI.Core.Shared.Contracts;
using ClientAPI.Models;

namespace ClientAPI.DataAccess.Contracts
{
    public interface IPersonRepo : IDataRepository<Person>
    {
        Task<Person> Register(Person person);
        Task<bool> IsUserExist (string username); 
        
    }
}