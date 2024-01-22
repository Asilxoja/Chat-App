using System.ComponentModel.DataAnnotations;

namespace DataAccsesLayer.Models
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "The FullName field is required.")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "The PhoneNumber field is required.")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "The Password field is required.")]
        public string Password { get; set; } = null!;
    }
}
