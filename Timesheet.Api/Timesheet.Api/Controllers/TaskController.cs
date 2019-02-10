using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Timesheet.BLL.Interfaces;
using Timesheet.DAL.Interfaces;
using Timesheet.Model.APIModel;

namespace Timesheet.Api.Controllers
{
    [Route("api/Task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;

        private readonly ITaskService _taskService;
        private readonly ITaskDP _taskDP;

        public TaskController(ITaskService taskService,
                              ITaskDP taskDP, 
                              ILogger<TaskController> logger)
        {
            _taskService = taskService;
            _taskDP = taskDP;
            _logger = logger;
        }

        [HttpGet("GetAllTasksPerProject/{projectId}")]
        public async Task<IActionResult> TasksPerProjectGet(int projectId)
        {
            TasksPerProjectResponse response = new TasksPerProjectResponse();
            try
            {
                response.ProjectTasks = await _taskDP.TasksPerProjectGetAsync(projectId)
                                                     .ConfigureAwait(false);
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
        public async Task<IActionResult> AddNewTask([FromBody]AddNewTaskRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                await _taskDP.InsertAsync(request).ConfigureAwait(false);

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
