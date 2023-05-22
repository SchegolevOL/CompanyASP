using System.ComponentModel.DataAnnotations;

namespace CompanyASP.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [MinLength(3)]
        public string Login { get; set; }
        [Required]
        [MinLength(3)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
    }
}
