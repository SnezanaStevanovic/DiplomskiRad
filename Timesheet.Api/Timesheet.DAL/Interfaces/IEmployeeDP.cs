using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Model;

namespace Timesheet.DAL.Interfaces
{
    public interface IEmployeeDP
    {
        Task InsertAsync(Employee employee);

        Task<List<Employee>> GetAllAsync();

        Task<Employee> GetEmployeeAsync(string email,
                                        string hashPass);

        Task<List<Employee>> ProjectEmployeesGetAsync(int projectId);
    }
}
