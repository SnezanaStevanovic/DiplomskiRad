
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<TaskService> _logger;

        private readonly ITaskDP _taskDP;

        public TaskService(ITaskDP taskDP, ILogger<TaskService> logger)
        {
            _taskDP = taskDP;
            _logger = logger;
        }

        public  Task<List<ProjectTask>> TasksPerProjectGetAsync(int projectId)
        {
            return _taskDP.TasksPerProjectGetAsync(projectId);
        }

    }
}
