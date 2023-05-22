namespace CompanyASP.Models
{
    public class User
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public bool IsAdmin { get; set; }

    }
}
