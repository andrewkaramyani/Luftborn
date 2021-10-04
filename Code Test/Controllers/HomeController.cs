using Code_test;
using Code_Test.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet_Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private IEmployeeReository _employeeReository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(IEmployeeReository employeeReository,
                                IWebHostEnvironment webHostEnvironment)
        {
            _employeeReository = employeeReository;
            this.webHostEnvironment = webHostEnvironment;
        }
        [Route("GetEmployees")]
        [HttpGet]
        public IEnumerable<Employee> GetEmployees()
        {
           //throw new Exception("develop exception");
            IEnumerable<Employee> Employees = _employeeReository.GetAllEmployees();
            return Employees;
        }
        [Route("DeleteEmployee/{id}")]
        [HttpDelete]
        public Employee DeleteEmployee(int? id)
        {
            var Employees = _employeeReository.Delete(id.Value);
            return Employees;
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("EmployeeDetails/{id}")]
        public Employee EmployeeDetails(int? id)
        {
            Employee employee = _employeeReository.GetEmployeeById(id.Value);
            return employee;
        }

        [HttpPost]
        [Route("CreateEmployee")]
        public Employee CreateEmployee(Employee Model)
        {
            if (ModelState.IsValid)
            {
                Employee NewEmployee = new Employee
                {
                    Name = Model.Name,
                    Email = Model.Email,
                    PhoneNumber=Model.PhoneNumber
                };
                _employeeReository.Add(NewEmployee);
                return Model;
            }
            return null;
        }

        [HttpPut]
        [Route("UpdateEmployee")]
        public Employee UpdateEmployee(Employee Model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeReository.GetEmployeeById(Model.Id);
                employee.Name = Model.Name;
                employee.Email = Model.Email;
                employee.PhoneNumber = Model.PhoneNumber;
              
                _employeeReository.Update(employee);
                return Model;
            }
            return null;
        }

    }
}
