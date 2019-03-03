
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.BLL.Interfaces;
using Timesheet.DAL.Interfaces;
using Timesheet.Model;

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

        public Task<List<ProjectTask>> EmployeeTasksGetAsync(int employeeId)
        {
            return _taskDP.EmployeeTasksGetAsync(employeeId);
        }


        public Task<List<ProjectTask>> EmployeeNTasksGetAsync(int employeeId,int n)
        {
            return _taskDP.EmployeeNTasksGetAsync(employeeId,n);
        }

        public Task<List<ProjectTask>> EmployeeTasksPerProjectGetAsync(int employeeId, int projectId)
        {
            return _taskDP.EmployeeTasksPerProjectGetAsync(employeeId, projectId);
        }

        public Task<List<ProjectTask>> TasksPerProjectGetAsync(int projectId)
        {
            return _taskDP.TasksPerProjectGetAsync(projectId);
        }

        public Task CreateTask(ProjectTask request)
        {
            return _taskDP.InsertAsync(request); 
        }
    }
}
