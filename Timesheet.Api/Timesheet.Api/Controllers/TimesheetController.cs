using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Timesheet.BLL.Interfaces;
using Timesheet.Model.APIModel;

namespace Timesheet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TimesheetController : ControllerBase
    {
        private readonly ILogger<TimesheetController> _logger;

        private readonly ITimesheetService _timesheetService;

        public TimesheetController(ITimesheetService timesheetService, ILogger<TimesheetController> logger)
        {
            _timesheetService = timesheetService;
            _logger = logger;
        }

        [HttpPost("setStartTime")]
        public async Task<IActionResult> TimesheetStartTimeSet([FromBody]int employeeId)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                await _timesheetService.SetStartTimeForEmployee(employeeId)
                                       .ConfigureAwait(false);

                response.Message = "StartTime updated successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Method failed";
                response.Success = false;

                _logger.LogError(ex, $"{nameof(TimesheetController)}.{MethodBase.GetCurrentMethod().Name}");
            }


            return Ok(response);

        }

        [HttpPost("setEndTime")]
        public async Task<IActionResult> TimesheetEndTimeSet([FromBody]int employeeId)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                await _timesheetService.SetEndTimeForEmployee(employeeId)
                                       .ConfigureAwait(false);

                response.Message = "EndTime updated successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Method failed";
                response.Success = false;
                _logger.LogError(ex, $"{nameof(TimesheetController)}.{MethodBase.GetCurrentMethod().Name}");
            }

            return Ok(response);

        }

        [HttpPost("endTimeUpdate")]
        public async Task<IActionResult> TimesheetEndTimeSet([FromBody]TimesheetEndTimeRequest request)
        {
            BaseResponse response = new BaseResponse();

            try
            {
                bool isUpdated = await _timesheetService.EndTimeSetAsync(request.EmployeeId,
                                                                         request.EndTime,
                                                                         request.Overtime,
                                                                         request.Pause)
                                                        .ConfigureAwait(false);

                if (isUpdated)
                {
                    response.Message = "EndTime updated successfully";
                    response.Success = true;
                }
                else
                {
                    response.Message = "Do not exist timesheet record for this employee.";
                    response.Success = false;
                }

            }
            catch (Exception ex)
            {
                response.Message = "Method execution failed";
                response.Success = false;

                _logger.LogError(ex, $"{nameof(TimesheetController)}.{MethodBase.GetCurrentMethod().Name}");
            }

            return Ok(response);
        }

        [HttpPost("periodTimesheetGet")]
        public async Task<IActionResult> PeriodTimesheetGet([FromBody]PeriodTimeheetGetRequest request)
        {
            PeriodTimeheetGetResponse response = new PeriodTimeheetGetResponse();
            try
            {
                response.AllTimesheetsForPeriod = await _timesheetService.PeriodTimesheetGetAsync(request.EmployeeId,
                                                                                                  request.StartDate,
                                                                                                  request.EndDate)
                                                                         .ConfigureAwait(false);

                if (response.AllTimesheetsForPeriod.Count > 0)
                {
                    response.Success = true;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Do no exist timesheet record in this time interval";
                }

            }
            catch (Exception ex)
            {
                response.Message = $"Method execution failed";
                response.Success = false;

                _logger.LogError(ex, $"{nameof(TimesheetController)}.{MethodBase.GetCurrentMethod().Name}");
            }

            return Ok(response);

        }


        [HttpGet("TimesheetStateForDay")]
        public async Task<ActionResult> GetTimesheetState(int employeeId)
        {
            TimesheetStateOfDayResponse response = new TimesheetStateOfDayResponse();

            try
            {
                response.WorkingSecounds = await _timesheetService.GetTimesheetStateOfDayAsync(employeeId);
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(TimesheetController)}.{MethodBase.GetCurrentMethod().Name}  EmployeeId : {employeeId}");
                response.Success = false;
                response.Message = "";
            }

            return Ok(response);

        }


    }
}