using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Timesheet.BLL.Interfaces;
using Timesheet.Model;
using Timesheet.Model.APIModel;

namespace Timesheet.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;

        private readonly IProjectService _projectService;

        private readonly IEmployeeProjectService _employeeProjectService;

        public ProjectController(
            IProjectService projectService,
            ILogger<ProjectController> logger,
            IEmployeeProjectService employeeProjectService)
        {
            _projectService = projectService;
            _logger = logger;
            _employeeProjectService = employeeProjectService;
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

                _logger.LogError(ex, $"{nameof(ProjectController)}.{MethodBase.GetCurrentMethod().Name}");
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

                _logger.LogError(ex, $"{nameof(ProjectController)}.{MethodBase.GetCurrentMethod().Name}");
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

                _logger.LogError(ex, $"{nameof(ProjectController)}.{MethodBase.GetCurrentMethod().Name}");
            }

            return Ok(response);
        }

        [HttpPost("AddNew")]
        public async Task<IActionResult> AddNew([FromBody] Project newProject)
        {
            BaseResponse response = new BaseResponse();

            try
            {
                await _projectService.AddNewAsync(newProject)
                                     .ConfigureAwait(false);

                response.Message = "New project added successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "AddNew() method execution failed";
                response.Success = false;

                _logger.LogError(ex, $"{nameof(ProjectController)}.{MethodBase.GetCurrentMethod().Name}");
            }

            return Ok(response);
        }

        [HttpPost("AddEmployees")]
        public async Task<ActionResult> AddEmployeesToProject([FromBody]AddEmployeesToProjectRequest addEmployeesToProjectRequest)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                await _employeeProjectService.AddEmployeesToProject(addEmployeesToProjectRequest.ProjectId, addEmployeesToProjectRequest.EmployeesIds);
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ProjectController)}.{MethodBase.GetCurrentMethod().Name}");
                response.Success = false;
            }

            return Ok(response);
        }
    }
}