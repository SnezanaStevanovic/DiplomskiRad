using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheet.BLL.Interfaces;
using Timesheet.DAL.Interfaces;
using Timesheet.Model;

namespace Timesheet.BLL
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ILogger<EmployeeService> _logger;

        private readonly IEmployeeDP _employeeDP;

        public EmployeeService(IEmployeeDP employeeDP, ILogger<EmployeeService> logger)
        {
            _employeeDP = employeeDP;
            _logger = logger;
        }

        public async Task AddNewAsync(Employee employee)
        {

            await _employeeDP.InsertAsync(employee)
                             .ConfigureAwait(false);

        }

        public async Task<List<Employee>> GetAllAsync()
        {
            List<Employee> employees = await _employeeDP.GetAllAsync()
                                                        .ConfigureAwait(false);

            return employees;
        }

        public async Task<Employee> GetAsync(string email,
                                             string password)
        {

            Employee employee = await _employeeDP.GetEmployeeAsync(email, password)
                                                 .ConfigureAwait(false);

            return employee;
        }

        public async Task<List<Employee>> ProjectEmployeesGetAsync(int projectId)
        {
            List<Employee> projectEmployees = await _employeeDP.ProjectEmployeesGetAsync(projectId)
                                                               .ConfigureAwait(false);

            return projectEmployees;
        }

        public Task RemoveAsync(int employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
