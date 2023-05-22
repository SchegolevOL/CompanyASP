using System.ComponentModel.DataAnnotations;

namespace CompanyASP.ViewModel
{
    public class RegistrationViewModel
    {
        [Required]
        [MinLength(3)]
        public string Login { get; set; }
        [Required]
        [MinLength(3)]
        public string Password { get; set; }
        [Required]
        [MinLength(3)]
        [Compare("Password")]
        public string PasswordAgain { get; set; }

    }
}
