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
    public class ProjectService : IProjectService
    {
        private ILog Logger { get; } = LogManager.GetLogger(typeof(ProjectService));

        private IProjectDP _projectDP;

        public ProjectService(IProjectDP projectDP)
        {
            _projectDP = projectDP;
        }

        public async Task AddNewAsync(Project project)
        {
            await _projectDP.InsertAsync(project).ConfigureAwait(false);

        }

        public async Task<List<Project>> GetAllAsync()
        {
            List<Project> projects = await _projectDP.GetAllAsync()
                                                     .ConfigureAwait(false);


            return projects;
        }

        public async Task<Project> GetByIdAsync(int projectId)
        {
            Project project = await _projectDP.GetByIdAsync(projectId)
                                              .ConfigureAwait(false);

            return project;
        }

        public async Task<List<Project>> GetAllProjectsForEmployee(int employeeId)
        {
            List<Project> employeeProjects = await _projectDP.GetAllProjectsForEmployee(employeeId)
                                                             .ConfigureAwait(false);


            return employeeProjects;
        }
    }
}
