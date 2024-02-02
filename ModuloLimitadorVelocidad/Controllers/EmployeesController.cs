using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModuloLimitadorVelocidad.Data;
using ModuloLimitadorVelocidad.Models;
using ModuloLimitadorVelocidad.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace ModuloLimitadorVelocidad.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVC_DbContext mvc_DbContext;

        public EmployeesController(MVC_DbContext mvc_DbContext)
        {
            this.mvc_DbContext = mvc_DbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await mvc_DbContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth
            };
            await mvc_DbContext.Employees.AddAsync(employee);
            await mvc_DbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id) 
        { 
            var employee = await mvc_DbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {

                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth
                };

                return await Task.Run(() => View("view", viewModel));
            }


            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await mvc_DbContext.Employees.FindAsync(model.Id);

            if (employee != null) 
            { 
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;

                await mvc_DbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await mvc_DbContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                mvc_DbContext.Employees.Remove(employee);
                await mvc_DbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

    }
}
