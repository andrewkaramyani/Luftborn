using Code_Test.Data;
using Code_Test.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Code_test
{
    public class SqlEmployeeRepository : IEmployeeReository
    {
        private readonly AppDbContext Context;

        public SqlEmployeeRepository(AppDbContext Context)
        {
            this.Context = Context;
        }
        public Employee Add(Employee employee)
        {
            Context.Employees.Add(employee);
            Context.SaveChanges();
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = Context.Employees.Find(id);
            if (employee != null)
            {
                Context.Remove(employee);
                Context.SaveChanges();
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return Context.Employees;
        }

        public Employee GetEmployeeById(int id)
        {
            Employee employee = Context.Employees.Find(id);
            return employee;
        }

        public Employee Update(Employee employeeChanges)
        {
            var employee = Context.Employees.Attach(employeeChanges);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();
            return employeeChanges;
        }
    }
}
