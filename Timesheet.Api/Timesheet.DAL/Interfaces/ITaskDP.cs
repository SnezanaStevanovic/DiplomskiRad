using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Model;

namespace Timesheet.DAL.Interfaces
{
    public interface ITaskDP
    {
        Task InsertAsync(ProjectTask task);

        Task<List<ProjectTask>> TasksPerProjectGetAsync(int projectId);

        Task<List<ProjectTask>> EmployeeTasksGetAsync(int employeeId);

        Task<List<ProjectTask>> EmployeeTasksPerProjectGetAsync(int employeeId, int projectId);
        Task<List<ProjectTask>> EmployeeNTasksGetAsync(int employeeId, int n);
    }
}
