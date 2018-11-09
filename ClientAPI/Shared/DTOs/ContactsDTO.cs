namespace ClientAPI.Shared.DTOs
{
    public class ContactsDTO : BaseEntityDTO
    {
        public int ContactId { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        
    }
}