using CompanyASP.Encryptors;
using CompanyASP.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Text.Json;

namespace CompanyASP.Services
{
    public class UserManager : IUserManager
    {
        private readonly UserDbContext _userDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserManager(UserDbContext userDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _userDbContext = userDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool GetCookieAdmin()
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("auth"))
            {
                var hash = _httpContextAccessor.HttpContext.Request.Cookies["auth"];
                var json = AesEncryptor.DecryptString("b14ca5898a4e4133bbce2ea2315a1916", hash);
                var user = JsonSerializer.Deserialize<UsersCredentials>(json);

                return user.IsAdmin;


            }
            return false;
        }
        public UsersCredentials GetCredentials()
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("auth"))
            {
                var hash = _httpContextAccessor.HttpContext.Request.Cookies["auth"];
                var json = AesEncryptor.DecryptString("b14ca5898a4e4133bbce2ea2315a1916", hash);
                return JsonSerializer.Deserialize<UsersCredentials>(json);
            }
            else
            {
                return null;
            }
        }

        public bool Login(string username, string password)
        {
            var passwordHash = Sha256Encryptor.Encrypt(password);
            var user = _userDbContext.Users.FirstOrDefault(x => x.Login == username && x.PasswordHash == passwordHash);
            if (user != null)
            {
                UsersCredentials credentials = new UsersCredentials()
                {
                    IsAdmin = user.IsAdmin,
                    Login = user.Login
                };

                var json = JsonSerializer.Serialize(credentials);
                var hash = AesEncryptor.EncryptString("b14ca5898a4e4133bbce2ea2315a1916", json);
                _httpContextAccessor.HttpContext.Response.Cookies.Append("auth", hash);
                return true;
            }
            return false;
        }
    }
}
