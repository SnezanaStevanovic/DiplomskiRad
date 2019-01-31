using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Timesheet.BLL.Interfaces;
using Timesheet.Model.APIModel;

namespace Timesheet.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private ILog Logger { get; } = LogManager.GetLogger(typeof(ProjectController));

        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            AllProjectsResponse response = new AllProjectsResponse();
            try
            {
                response.Projects = await _projectService.GetAllAsync()
                                                         .ConfigureAwait(false);

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "GetAll() method execution failed";
                response.Success = false;

                Logger.Error($"{ex}");
            }

            return Ok(response);
        }

        [HttpGet("GetEmployeeProjects/{employeeId}")]
        public async Task<IActionResult> GetEmployeeProjects(int employeeId)
        {
            EmployeeProjectsResponse response = new EmployeeProjectsResponse();

            try
            {
                response.Projects = await _projectService.GetAllProjectsForEmployee(employeeId)
                                                         .ConfigureAwait(false);

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "GetEmployeeProjects() method execution failed";
                response.Success = false;

                Logger.Error($"{ex}");
            }

            return Ok(response);
        }

        [HttpGet("GetById/{projectId}")]
        public async Task<IActionResult> GetById(int projectId)
        {
            GetProjectByIdResponse response = new GetProjectByIdResponse();

            try
            {
                response.Project = await _projectService.GetByIdAsync(projectId)
                                                         .ConfigureAwait(false);

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Method execution failed";
                response.Success = false;

                Logger.Error($"{ex}");
            }

            return Ok(response);
        }


        [HttpPost("AddNew")]
        public async Task<IActionResult> AddNew([FromBody] AddNewProjectRequest request)
        {
            BaseResponse response = new BaseResponse();

            try
            {
                await _projectService.AddNewAsync(request)
                                     .ConfigureAwait(false);

                response.Message = "New project added successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "AddNew() method execution failed";
                response.Success = false;

                Logger.Error($"{ex}");
            }

            return Ok(response);
        }
    }
}