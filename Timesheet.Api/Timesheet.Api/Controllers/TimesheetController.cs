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

        [HttpPost("startTimeUpdate")]
        public async Task<IActionResult> TimesheetStartTimeSet([FromBody]TimesheetStartTimeRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                await _timesheetService.StartTimeSetAsync(request.EmployeeId,
                                                          request.StartTime)
                                       .ConfigureAwait(false);

                response.Message = "StartTime updated successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "TimesheetStartTimeSet() method failed";
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
                await _timesheetService.EndTimeSetAsync(request.EmployeeId,
                                                        request.EndTime,
                                                        request.Overtime,
                                                        request.Pause).ConfigureAwait(false);

                response.Message = "EndTime updated successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "TimesheetEndTimeSet() method failed";
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

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = $"PeriodTimesheetGet() method failed";
                response.Success = false;

                Logger.Error($"{ex}");
            }

            return Ok(response);

        }
    }
}