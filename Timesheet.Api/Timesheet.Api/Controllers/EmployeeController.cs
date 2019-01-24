using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Timesheet.BLL.Interfaces;
using Timesheet.Model;
using Timesheet.Model.APIModel;

namespace Timesheet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private ILog Logger { get; } = LogManager.GetLogger(typeof(EmployeeController));

        private readonly IEmployeeService _employeeService;

        private readonly IEmployeeProjectService _employeeProjectService;

        public EmployeeController(IEmployeeService employeeService,
                                  IEmployeeProjectService employeeProjectService)
        {
            _employeeService = employeeService;
            _employeeProjectService = employeeProjectService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            AllEmployeesResponse response = new AllEmployeesResponse();

            try
            {
                response.Employees = await _employeeService.GetAllAsync()
                                                           .ConfigureAwait(false);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "GetAll() method failed";
                response.Success = false;

                Logger.Error($"{ex}");
            }

            return Ok(response);
        }

        [HttpGet("GetAllEmployeesPerProject/{projectId}")]
        public async Task<IActionResult> GetAllEmployeesPerProject(int projectId)
        {
            ProjectEmployeesResponse response = new ProjectEmployeesResponse();

            try
            {
                response.Employees = await _employeeService.ProjectEmployeesGetAsync(projectId)
                                                           .ConfigureAwait(false);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "GetAllEmployeesPerProject() method failed";
                response.Success = false;

                Logger.Error($"{ex}");
            }

            return Ok(response);
        }

        [HttpGet("EmployeeGet/{email}/{pass}")]
        //public async Task<IActionResult> GetEmployeeAsync(string email, string pass)
        //{
        //    GetEmployeeResponse response = new GetEmployeeResponse();
        //    try
        //    {
        //        response.Employee = await _employeeService.GetAsync(email,
        //                                                            pass)
        //                                                  .ConfigureAwait(false);

        //        response.Message = "Method executed successfully";
        //        response.Success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Message = "Method GetEmployeeAsync() failed";
        //        response.Success = false;

        //        Logger.Error($"{ex}");
        //    }

        //    return Ok(response);
        //}

        [HttpPost("AddNew")]
        public async Task<IActionResult> AddNewEmployee([FromBody] AddNewEmployeeRequest request)
        {
            BaseResponse response = new BaseResponse();

            try
            {
                await _employeeService.AddNewAsync(request)
                                      .ConfigureAwait(false);

                await _employeeProjectService.AddNewAsync(request.Id,
                                                          request.ProjectId)
                                             .ConfigureAwait(false);

                response.Message = "New employee added successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Method AddNewEmployee() failed";
                response.Success = false;

                Logger.Error($"{ex}");
            }

            return Ok(response);
        }
    }
}