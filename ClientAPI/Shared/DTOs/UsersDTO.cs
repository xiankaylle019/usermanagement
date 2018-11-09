namespace ClientAPI.Shared.DTOs
{
    public class UsersDTO : BaseEntityDTO
    {
        public int PersonId { get; set; }  
        public string IdentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string FullName { get =>   $"{ this.FirstName } { this.LastName }"; }     
                                  
    }
}