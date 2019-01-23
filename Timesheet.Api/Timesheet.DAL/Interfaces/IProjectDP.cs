using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Model;

namespace Timesheet.DAL.Interfaces
{
    public interface IProjectDP
    {
        Task InsertAsync(Project project);

        Task<Project> GetByIdAsync(int projectId);

        Task<List<Project>> GetAllAsync();

        Task<List<Project>> GetAllProjectsForEmployee(int employeeId);
    }
}
