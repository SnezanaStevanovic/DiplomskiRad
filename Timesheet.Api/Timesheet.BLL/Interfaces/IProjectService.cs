using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Model;

namespace Timesheet.BLL.Interfaces
{
    public interface IProjectService
    {
        Task AddNewAsync(Project project);

        Task<Project> GetByIdAsync(int projectId);

        Task<List<Project>> GetAllAsync();

        Task<List<Project>> GetAllProjectsForEmployee(int employeeId);
    }
}
