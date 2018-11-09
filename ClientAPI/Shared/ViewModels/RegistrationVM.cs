using System.ComponentModel.DataAnnotations;

namespace ClientAPI.Shared.ViewModels
{
    public class RegistrationVM
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Username { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage= "You must specify a password with a minimum of 4 characters" )]
        public string Password { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name minimum length is 2")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last Name minimum length is 2")]
        public string LastName { get; set; }        
        public string IdentityId { get; set; }
    }
}