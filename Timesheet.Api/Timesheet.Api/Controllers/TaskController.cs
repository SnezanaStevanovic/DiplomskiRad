using log4net;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Timesheet.BLL.Interfaces;
using Timesheet.DAL.Interfaces;
using Timesheet.Model.APIModel;

namespace Timesheet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ILog Logger { get; } = LogManager.GetLogger(typeof(TaskController));

        private readonly ITaskService _taskService;
        private readonly ITaskDP _taskDP;

        public TaskController(ITaskService taskService,
                              ITaskDP taskDP)
        {
            _taskService = taskService;
            _taskDP = taskDP;
        }

        [HttpGet("GetAllTasksPerProject/{projectId}")]
        public async Task<IActionResult> TasksPerProjectGet([FromQuery]int projectId)
        {
            TasksPerProjectResponse response = new TasksPerProjectResponse();
            try
            {
                response.ProjectTasks = await _taskDP.TasksPerProjectGetAsync(projectId).ConfigureAwait(false);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "TasksPerProjectGet() method failed";
                response.Success = false;

                Logger.Error($"{ex}");
            }

            return Ok(response);
        }

        [HttpPost("AddNewTask")]
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
                response.Message = "AddNewTask() method failed";
                response.Success = false;

                Logger.Error($"{ex}");
            }

            return Ok(response);

        }
    }
}
