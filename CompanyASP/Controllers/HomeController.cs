using CompanyASP.Models;
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
        public HomeController(CompanyDB companyDB)
        {
            this._companyDB = companyDB;
        }


        public IActionResult Index()
        {
            return View();
        }
        #region Department

        public IActionResult ListDepartment()
        {
            var departments = this._companyDB.Department.ToList();
            return View(departments);
        }
        [HttpGet]
        public IActionResult AddDepartment()
        {
            ViewBag.Department = new MultiSelectList(_companyDB.Department, "Id", "Name");
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDepartment(Department department)
        {

            await _companyDB.AddAsync(department);
            await _companyDB.SaveChangesAsync();
            return RedirectToAction("ListDepartment", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> EditDepartment(Guid? id)
        {
            if (id != null)
            {
                Department? department = await _companyDB.Department.FirstOrDefaultAsync(p => p.Id == id);
                if (department != null) return View(department);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditDepartment(Department department)
        {
            _companyDB.Department.Update(department);
            await _companyDB.SaveChangesAsync();
            return RedirectToAction("ListDepartment", "Home");
        }
        [HttpGet]
        public IActionResult DeleteDepartment(Guid id)
        {
            var department = _companyDB.Department.Find(id);
            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDepartment(Guid? id)
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
            return NotFound();
        }
        #endregion

        #region Employee
        public IActionResult ListEmployee()
        {
            var ages = new List<int>();
            var employees = this._companyDB.Employee.ToList();
            foreach (var item in employees)
            {

                if (DateTime.Now.DayOfYear < item.DataOfBirth.DayOfYear)
                {
                    ages.Add(DateTime.Now.Year - item.DataOfBirth.Year + 1);
                }
                else if ((DateTime.Now.Year - item.DataOfBirth.Year) < 0)
                {
                    ages.Add(0);
                }

            }
            ViewBag.Ages = ages;
            return View(employees);

            /*(Employee, int) empoyeeAge;
            var empoyees = new List<(Employee, int)>();
            var employeesDb = this._companyDB.Employee.ToList();

            foreach (var item in employeesDb)
            {
                
                if (DateTime.Now.DayOfYear < item.DataOfBirth.DayOfYear)
                {
                    empoyeeAge = (item, (DateTime.Now.Year - item.DataOfBirth.Year + 1));
                    empoyees.Add(empoyeeAge);

                }
                else if ((DateTime.Now.Year - item.DataOfBirth.Year) < 0)
                {
                    empoyeeAge = (item, 0);
                    empoyees.Add(empoyeeAge);
                }else
                {
                    empoyeeAge = (item, (DateTime.Now.Year - item.DataOfBirth.Year));
                    empoyees.Add(empoyeeAge);
                }

            }
            
            return View(employees);*/
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            ViewBag.Department = new MultiSelectList(_companyDB.Department, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {

            //await _companyDB.AddAsync(employee);
            //await _companyDB.Database.ExecuteSqlInterpolatedAsync($"SET IDENTITY_INSERT dbo.Employee ON;");
            //await _companyDB.SaveChangesAsync();
            //await _companyDB.Database.ExecuteSqlInterpolatedAsync($"SET IDENTITY_INSERT dbo.Employee OFF;");

            await _companyDB.Database.ExecuteSqlInterpolatedAsync($"INSERT INTO dbo.Employee (FirstName, SurName, Patronymic, DateOfBirth, DocSeries, DocNumber, Position, DepartmentID) VALUES ({employee.FirstName}, {employee.SurName}, {employee.Patronymic}, {employee.DataOfBirth}, {employee.DocSeries}, {employee.DocNumber}, {employee.Position}, {employee.DepartmentID});");

            return RedirectToAction("ListDepartment", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> EditEmployee(decimal? id)
        {
            if (id != null)
            {
                Employee? employee = await _companyDB.Employee.FirstOrDefaultAsync(p => p.Id == id);
                if (employee != null) return View(employee);
            }
            return NotFound();
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
            var employee = _companyDB.Employee.Find(id);
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(decimal? id)
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