using Microsoft.AspNetCore.Mvc;
using WebApplicationEmployé.Models;
using WebApplicationEmployé.Models.Repositories;

namespace WebApplicationEmployé.Controllers
{
    public class EmployeeController : Controller
    {
        readonly IRepository<Employee> employeeRepository;

        // Injection de dépendance
        public EmployeeController(IRepository<Employee> empRepository)
        {
            employeeRepository = empRepository;
        }

        // GET: Employee
        public IActionResult Index()
        {
            var employees = employeeRepository.GetAll();

            ViewData["EmployeesCount"] = employees.Count();
            ViewData["SalaryAverage"] = employeeRepository.SalaryAverage();
            ViewData["MaxSalary"] = employeeRepository.MaxSalary();
            ViewData["HREmployeesCount"] = employeeRepository.HrEmployeesCount();

            return View(employees);
        }

        // GET: Employee/Details/5
        public IActionResult Details(int id)
        {
            var employee = employeeRepository.FindByID(id);

            return View(employee);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee e)
        {
            if (ModelState.IsValid)
            {
                employeeRepository.Add(e);

                return RedirectToAction(nameof(Index));
            }

            return View(e);
        }

        // GET: Employee/Edit/5
        public IActionResult Edit(int id)
        {
            var employee = employeeRepository.FindByID(id);

            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Employee newemployee)
        {
            if (ModelState.IsValid)
            {
                employeeRepository.Update(id, newemployee);

                return RedirectToAction(nameof(Index));
            }

            return View(newemployee);
        }

        // GET: Employee/Delete/5
        public IActionResult Delete(int id)
        {
            var employee = employeeRepository.FindByID(id);

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            employeeRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        // Recherche d'un employé
        public ActionResult Search(string term)
        {
            var result = employeeRepository.Search(term);

            return View("Index", result);
        }
    }
}