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
    public class ProjectService : IProjectService
    {
        private readonly ILogger<ProjectService> _logger;

        private IProjectDP _projectDP;

        public ProjectService(IProjectDP projectDP, ILogger<ProjectService> logger)
        {
            _projectDP = projectDP;
            _logger = logger;
        }

        public Task AddNewAsync(Project project)
        {
             return _projectDP.InsertAsync(project);

        }

        public  Task<List<Project>> GetAllAsync()
        {
            return _projectDP.GetAllAsync();
        }

        public  Task<Project> GetByIdAsync(int projectId)
        {
            return _projectDP.GetByIdAsync(projectId);
        }

        public  Task<List<Project>> GetAllProjectsForEmployee(int employeeId)
        {
            return _projectDP.GetAllProjectsForEmployee(employeeId);
        }
    }
}
