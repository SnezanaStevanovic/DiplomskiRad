using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Timesheet.BLL.Interfaces;
using Timesheet.DAL.Interfaces;
using Timesheet.Model;
using Timesheet.Model.APIModel;

namespace Timesheet.Api.Controllers
{
    [Route("api/Task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;

        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService,
                              ILogger<TaskController> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        [HttpGet("GetAllTasksPerProject/{projectId}")]
        public async Task<IActionResult> TasksPerProjectGet(int projectId)
        {
            TasksListResponse response = new TasksListResponse();
            try
            {
                response.ProjectTasks = await _taskService.TasksPerProjectGetAsync(projectId);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Method execution failed";
                response.Success = false;

                _logger.LogError(ex, $"{nameof(TaskController)}.{MethodBase.GetCurrentMethod().Name}");
            }

            return Ok(response);
        }

        [HttpGet("GetAllEmployeeTasks/{employeeId}")]
        public async Task<IActionResult> EmployeeTasksGet(int employeeId)
        {
            TasksListResponse response = new TasksListResponse();
            try
            {
                response.ProjectTasks = await _taskService.EmployeeTasksGetAsync(employeeId);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Method execution failed";
                response.Success = false;

                _logger.LogError(ex, $"{nameof(TaskController)}.{MethodBase.GetCurrentMethod().Name}");
            }

            return Ok(response);
        }

        [HttpGet("GetNTasksForEmployee")]
        public async Task<IActionResult> GetNTasksForEmployee(int employeeId, int n)
        {
            TasksListResponse response = new TasksListResponse();
            try
            {
                response.ProjectTasks = await _taskService.EmployeeNTasksGetAsync(employeeId, n);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Method execution failed";
                response.Success = false;
                _logger.LogError(ex, $"{nameof(TaskController)}.{MethodBase.GetCurrentMethod().Name}");
            }

            return Ok(response);
        }

        [HttpGet("GetAllEmployeeTasksPerProject/{employeeId}/{projectId}")]
        public async Task<IActionResult> EmployeeTasksPerProjectGet(int employeeId, int projectId)
        {
            TasksListResponse response = new TasksListResponse();
            try
            {
                response.ProjectTasks = await _taskService.EmployeeTasksPerProjectGetAsync(employeeId, projectId);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Method execution failed";
                response.Success = false;

                _logger.LogError(ex, $"{nameof(TaskController)}.{MethodBase.GetCurrentMethod().Name}");
            }

            return Ok(response);
        }

        [HttpPost("AddNew")]
        public async Task<IActionResult> AddNewTask([FromBody]ProjectTask request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                await _taskService.CreateTask(request);

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Method execution failed";
                response.Success = false;

                _logger.LogError(ex, $"{nameof(TaskController)}.{MethodBase.GetCurrentMethod().Name}");
            }

            return Ok(response);
        }
    }
}