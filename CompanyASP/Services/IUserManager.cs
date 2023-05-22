using CompanyASP.Models;

namespace CompanyASP.Services
{
    public interface IUserManager
    {
        bool Login(string username, string password);
        UsersCredentials GetCredentials();
        bool GetCookieAdmin();
    }
    
}
