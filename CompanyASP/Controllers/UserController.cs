using CompanyASP.Encryptors;
using CompanyASP.Models;
using CompanyASP.Services;
using CompanyASP.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CompanyASP.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserDbContext _userDbContext;
        private readonly IUserManager _userManager;

        public UserController(ILogger<UserController> logger, UserDbContext userDbContext, IUserManager userManager)
        {
            _logger = logger;
            _userDbContext = userDbContext;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_userManager.Login(model.Login, model.Password))
                {
                    return RedirectToAction("ListEmployee", "Home");
                }
            }
            ModelState.AddModelError("all", "Incorrect username or password!");
            return View("Index", model);
        }

        [HttpGet]
        public IActionResult Registry()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registry(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _userDbContext.Users.AddAsync(new User
                {
                    Login = model.Login,
                    PasswordHash = Sha256Encryptor.Encrypt(model.Password),
                    IsAdmin = true
                });
                await _userDbContext.SaveChangesAsync();
                return RedirectToAction("Index", "User");
            }
            return View(model);
        }

    }
}
