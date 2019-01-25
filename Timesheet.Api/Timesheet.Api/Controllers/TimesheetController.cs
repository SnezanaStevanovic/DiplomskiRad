using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Timesheet.BLL.Interfaces;
using Timesheet.Model.APIModel;

namespace Timesheet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesheetController : ControllerBase
    {
        private ILog Logger { get; } = LogManager.GetLogger(typeof(TimesheetController));

        private readonly ITimesheetService _timesheetService;

        public TimesheetController(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
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

                Logger.Error($"{ex}");
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

                Logger.Error($"{ex}");
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

                Logger.Error($"{ex}");
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

                Logger.Error($"{ex}");
            }

            return Ok(response);

        }
    }
}