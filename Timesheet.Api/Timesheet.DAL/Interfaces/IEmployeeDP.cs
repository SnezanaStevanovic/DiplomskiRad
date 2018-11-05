using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Model;

namespace Timesheet.DAL.Interfaces
{
    public interface IEmployeeDP
    {
        Task Insert(Employee employee);

        Task<List<Employee>> GetAll();

        Task<Employee> GetEmployee(string email,
                                   string hashPass);

        Task<List<Employee>> ProjectEmployeesGet(int projectId);
    }
}
