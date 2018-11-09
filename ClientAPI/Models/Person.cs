using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ClientAPI.Models
{
    public class Person : BaseEntity
    {     
        public int PersonId { get; set; }
        public string IdentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public bool IsContactUpdated { get; set; }
        public virtual PersonContact PersonContact { get; set; }
        
    }
}