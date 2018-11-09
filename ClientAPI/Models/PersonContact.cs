namespace ClientAPI.Models
{
    public class PersonContact : BaseEntity
    {
        public int PersonContactId { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        
    }
}