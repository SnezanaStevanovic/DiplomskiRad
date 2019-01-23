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
            try
            {
                await _projectDP.InsertAsync(project).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.Error($"{ex}");
            }
        }

        public async Task<List<Project>> GetAllAsync()
        {
            List<Project> projects = new List<Project>();

            try
            {
                projects = await _projectDP.GetAllAsync()
                                           .ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                Logger.Error($"{ex}");
            }

            return projects;
        }

        public async Task<Project> GetByIdAsync(int projectId)
        {
            Project project = new Project();

            try
            {
                project = await _projectDP.GetByIdAsync(projectId)
                                          .ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                Logger.Error($"{ex}");
            }

            return project;
        }

        public async Task<List<Project>> GetAllProjectsForEmployee(int employeeId)
        {
            List<Project> employeeProjects = new List<Project>();

            try
            {
                employeeProjects = await _projectDP.GetAllProjectsForEmployee(employeeId)
                                                   .ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                Logger.Error($"{ex}");
            }

            return employeeProjects;
        }
    }
}
