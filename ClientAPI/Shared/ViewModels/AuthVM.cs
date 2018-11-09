using System.ComponentModel.DataAnnotations;

namespace ClientAPI.Shared.ViewModels
{
    public class AuthVM
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}