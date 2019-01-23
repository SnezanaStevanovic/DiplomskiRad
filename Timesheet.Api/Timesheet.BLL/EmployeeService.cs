using log4net;
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
        private ILog Logger { get; } = LogManager.GetLogger(typeof(EmployeeService));

        private readonly IEmployeeDP _employeeDP;

        public EmployeeService(IEmployeeDP employeeDP)
        {
            _employeeDP = employeeDP;
        }

        public async Task AddNewAsync(Employee employee)
        {
            try
            {
                await _employeeDP.InsertAsync(employee)
                                 .ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                Logger.Error($"{ex}");
            }
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                employees = await _employeeDP.GetAllAsync()
                                             .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.Error($"{ex}");
            }

            return employees;
        }

        public async Task<Employee> GetAsync(string email,
                                             string password)
        {
            Employee employee = new Employee();

            try
            {
                employee = await _employeeDP.GetEmployeeAsync(email, password)
                                            .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.Error($"{ex}");
            }

            return employee;
        }

        public async Task<List<Employee>> ProjectEmployeesGetAsync(int projectId)
        {
            List<Employee> projectEmployees = new List<Employee>();
            try
            {
                projectEmployees = await _employeeDP.ProjectEmployeesGetAsync(projectId)
                                                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.Error($"{ex}");
            }

            return projectEmployees;
        }

        public Task RemoveAsync(int employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
