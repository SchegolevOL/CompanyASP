using CompanyASP.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

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
                using(SHA256 sha256 = SHA256.Create())
                {
                    byte[]bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(model.Login));
                    StringBuilder stringBuilder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        stringBuilder.Append(bytes[i].ToString("x2"));
                    }
                    return Json(stringBuilder.ToString());
                }

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
       
    }
}
