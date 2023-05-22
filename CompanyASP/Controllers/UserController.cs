using CompanyASP.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CompanyASP.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Registry()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registry(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
       
    }
}
