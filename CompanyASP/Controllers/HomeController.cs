using CompanyASP.Models;
using CompanyASP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CompanyASP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CompanyDB _companyDB;
        private readonly IUserManager _userManager;
        public HomeController(ILogger<HomeController> logger, CompanyDB companyDB, IUserManager userManager)
        {
            _logger = logger;
            _companyDB = companyDB;
            _userManager = userManager;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        #region Department

        public IActionResult ListDepartment()
        {

            if (_userManager.GetCookieAdmin())
            {
                var departments = this._companyDB.Department.
                                             Join(this._companyDB.Department, e => e.DepartmentId, s => s.Id,
                                             (e, s) => new DepartmentView
                                             {
                                                 Id = e.Id,
                                                 DepartmentId = e.DepartmentId,
                                                 ParentDepartment = s.Name,
                                                 Name = e.Name,
                                                 Code = e.Code,

                                             }).ToList();
                return View(departments);
            }
            ModelState.AddModelError("all", "Incorrect username or password!");
            return RedirectToAction("Index", "User");

        }
        [HttpGet]
        public IActionResult AddDepartment()
        {
            if (_userManager.GetCookieAdmin())
            {
                ViewBag.Department = new MultiSelectList(_companyDB.Department, "Id", "Name");

                return View();
            }
            ModelState.AddModelError("all", "Incorrect username or password!");
            return RedirectToAction("Index", "User");



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDepartment(Department department)
        {
            if (_userManager.GetCookieAdmin())
            {
                await _companyDB.AddAsync(department);
                await _companyDB.SaveChangesAsync();
                return RedirectToAction("ListDepartment", "Home");
            }
            ModelState.AddModelError("all", "Incorrect username or password!");
            return RedirectToAction("Index", "User");


        }
        [HttpGet]
        public async Task<IActionResult> EditDepartment(Guid? id)
        {
            if (_userManager.GetCookieAdmin())
            {
                ViewBag.Department = new MultiSelectList(_companyDB.Department, "Id", "Name");
                if (id != null)
                {
                    Department? department = await _companyDB.Department.FirstOrDefaultAsync(p => p.Id == id);
                    if (department != null) return View(department);
                }
            }
            ModelState.AddModelError("all", "Incorrect username or password!");
            return RedirectToAction("Index", "User");

        }
        [HttpPost]
        public async Task<IActionResult> EditDepartment(Department department)
        {
            if (_userManager.GetCookieAdmin())
            {
                _companyDB.Department.Update(department);
                await _companyDB.SaveChangesAsync();
                return RedirectToAction("ListDepartment", "Home");
            }
            ModelState.AddModelError("all", "Incorrect username or password!");
            return RedirectToAction("Index", "User");

        }
        [HttpGet]
        public IActionResult DeleteDepartment(Guid id)
        {
            if (_userManager.GetCookieAdmin())
            {
                var department = _companyDB.Department.Find(id);
                return View(department);
            }
            ModelState.AddModelError("all", "Incorrect username or password!");
            return RedirectToAction("Index", "User");

        }

        [HttpPost]
        public async Task<IActionResult> DeleteDepartment(Guid? id)
        {
            if (await _companyDB.Employee.FirstOrDefaultAsync(p => p.DepartmentId == id) == null)
            {
                if (id != null)
                {
                    Department? department = await _companyDB.Department.FirstOrDefaultAsync(p => p.Id == id);
                    if (department != null)
                    {
                        _companyDB.Department.Remove(department);
                        await _companyDB.SaveChangesAsync();
                        return RedirectToAction("ListDepartment");
                    }
                }
            }
            return RedirectToAction("ListDepartment", "Home");
        }

        public IActionResult ListEmployeeOfDepartment(Guid id)
        {
            if (_userManager.GetCookieAdmin())
            {
                var employees = this._companyDB.Employee.
                             Join(this._companyDB.Department, e => e.DepartmentId, s => s.Id,
                             (e, s) => new EmployeeView
                             {
                                 Id = e.Id,
                                 DataOfBirth = e.DataOfBirth,
                                 DepartmentId = e.DepartmentId,
                                 FirstName = e.FirstName,
                                 SurName = e.SurName,
                                 Department = s.Name,
                                 Patronymic = e.Patronymic,
                                 DocSeries = e.DocSeries,
                                 DocNumber = e.DocNumber,
                                 Position = e.Position
                             }).ToList();
                var selected = from p in employees
                               where p.DepartmentId == id
                               select p;

                return View(selected);
            }

            return RedirectToAction("Index", "User");

        }

        #endregion

        #region Employee
        public IActionResult ListEmployee()
        {

            if (_userManager.GetCookieAdmin())
            {
                var employees = this._companyDB.Employee.
                             Join(this._companyDB.Department, e => e.DepartmentId, s => s.Id,
                             (e, s) => new EmployeeView
                             {
                                 Id = e.Id,
                                 DataOfBirth = e.DataOfBirth,
                                 DepartmentId = e.DepartmentId,
                                 FirstName = e.FirstName,
                                 SurName = e.SurName,
                                 Department = s.Name,
                                 Patronymic = e.Patronymic,
                                 DocSeries = e.DocSeries,
                                 DocNumber = e.DocNumber,
                                 Position = e.Position
                             }).ToList();
                return View(employees);
            }

            return RedirectToAction("Index", "User");





        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            if (_userManager.GetCookieAdmin())
            {
                ViewBag.Department = new MultiSelectList(_companyDB.Department, "Id", "Name");
                return View();
            }
            ModelState.AddModelError("all", "Incorrect username or password!");
            return RedirectToAction("Index", "User");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {

            //TODO ??????????????????????????????????????
            //await _companyDB.Database.ExecuteSqlInterpolatedAsync($"SET IDENTITY_INSERT dbo.Employee ON;");
            //await _companyDB.Employee.AddAsync(employee);
            //await _companyDB.SaveChangesAsync();
            //await _companyDB.Database.ExecuteSqlInterpolatedAsync($"SET IDENTITY_INSERT dbo.Employee OFF;");

            await _companyDB.Database.ExecuteSqlInterpolatedAsync($"INSERT INTO dbo.Employee (FirstName, SurName, Patronymic, DateOfBirth, DocSeries, DocNumber, Position, DepartmentID) VALUES ({employee.FirstName}, {employee.SurName}, {employee.Patronymic}, {employee.DataOfBirth}, {employee.DocSeries}, {employee.DocNumber}, {employee.Position}, {employee.DepartmentId});");

            return RedirectToAction("ListEmployee", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> EditEmployee(decimal? id)
        {
            if (_userManager.GetCookieAdmin())
            {
                ViewBag.Department = new MultiSelectList(_companyDB.Department, "Id", "Name");
                if (id != null)
                {
                    Employee? employee = await _companyDB.Employee.FirstOrDefaultAsync(p => p.Id == id);
                    if (employee != null) return View(employee);
                }
                return NotFound();
            }
            ModelState.AddModelError("all", "Incorrect username or password!");
            return RedirectToAction("Index", "User");

        }
        [HttpPost]
        public async Task<IActionResult> EditEmployee(Employee employee)
        {

            _companyDB.Employee.Update(employee);
            await _companyDB.SaveChangesAsync();
            return RedirectToAction("ListEmployee", "Home");
        }

        [HttpGet]
        public IActionResult DeleteEmployee(decimal id)
        {
            if (_userManager.GetCookieAdmin())
            {
                var employee = _companyDB.Employee.Find(id);
                return View(employee);
            }
            ModelState.AddModelError("all", "Incorrect username or password!");
            return RedirectToAction("Index", "User");

        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(decimal? id)
        {
            if (_userManager.GetCookieAdmin())
            {
                if (id != null)
                {
                    Employee? employee = await _companyDB.Employee.FirstOrDefaultAsync(p => p.Id == id);
                    if (employee != null)
                    {
                        _companyDB.Employee.Remove(employee);
                        await _companyDB.SaveChangesAsync();
                        return RedirectToAction("ListEmployee");
                    }
                }
                return NotFound();
            }
            ModelState.AddModelError("all", "Incorrect username or password!");
            return RedirectToAction("Index", "User");

        }


        #endregion

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}