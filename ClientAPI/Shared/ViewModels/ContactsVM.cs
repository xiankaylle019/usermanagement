using System.ComponentModel.DataAnnotations;

namespace ClientAPI.Shared.ViewModels
{
    public class ContactsVM
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 11, ErrorMessage = "Mobile Number minimum length is 11")]
        public string MobileNo { get; set; }
        
        public string Address { get; set; }
    }
}