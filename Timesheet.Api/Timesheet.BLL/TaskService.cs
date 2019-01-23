using log4net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.BLL.Interfaces;
using Timesheet.DAL.Interfaces;
using Timesheet.Model;
using Timesheet.Model.APIModel;


namespace Timesheet.BLL
{
    public class TaskService : ITaskService
    {
        private ILog Logger { get; } = LogManager.GetLogger(typeof(TaskService));

        private readonly ITaskDP _taskDP;

        public TaskService(ITaskDP taskDP)
        {
            _taskDP = taskDP;
        }

        public async Task<List<ProjectTask>> TasksPerProjectGetAsync(int projectId)
        {
            List<ProjectTask> projectTasks = new List<ProjectTask>();

            try
            {
                projectTasks = await _taskDP.TasksPerProjectGetAsync(projectId);
                
            }
            catch (Exception ex)
            {
                Logger.Error($"{ex}");
            }

            return projectTasks;
        }

    }
}
