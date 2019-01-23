using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Model;

namespace Timesheet.BLL.Interfaces
{
    public interface IEmployeeService
    {
        Task AddNewAsync(Employee employee);

        Task<List<Employee>> GetAllAsync();

        Task<Employee> GetAsync(string email,
                                string password);

        Task<List<Employee>> ProjectEmployeesGetAsync(int projectId);

        Task RemoveAsync(int employeeId);

    }
}
