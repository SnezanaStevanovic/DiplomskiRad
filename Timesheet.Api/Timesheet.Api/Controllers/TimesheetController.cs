using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly ITimesheetService _timesheetService;

        public TimesheetController(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        [HttpPost("startTimeUpdate")]
        public async Task<IActionResult> TimesheetStartTimeSet([FromBody]TimesheetStartTimeRequest request)
        {
            BaseResponse response = await _timesheetService.StartTimeSet(request)
                                                           .ConfigureAwait(false);

            return Ok(response);

        }

        [HttpPost("endTimeUpdate")]
        public async Task<IActionResult> TimesheetEndTimeSet([FromBody]TimesheetEndTimeRequest request)
        {
            BaseResponse response = await _timesheetService.EndTimeSet(request)
                                                           .ConfigureAwait(false);

            return Ok(response);

        }

        [HttpPost("periodTimesheetGet")]
        public async Task<IActionResult> PeriodTimesheetGet([FromBody]PeriodTimeheetGetRequest request)
        {
            PeriodTimeheetGetResponse response = await _timesheetService.PeriodTimesheetGet(request)
                                                                        .ConfigureAwait(false);

            return Ok(response);

        }
    }
}