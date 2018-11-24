using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Model.APIModel;

namespace Timesheet.BLL.Interfaces
{
    public interface ITimesheetService
    {
        Task<BaseResponse> StartTimeSet(TimesheetStartTimeRequest startTimeRequest);

        Task<BaseResponse> EndTimeSet(TimesheetEndTimeRequest endTimeRequest);

        Task<PeriodTimeheetGetResponse> PeriodTimesheetGet(PeriodTimeheetGetRequest periodTimeheetGetRequest);
    }
}
