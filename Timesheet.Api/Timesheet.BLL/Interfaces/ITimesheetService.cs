using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Model.APIModel;
using Model = Timesheet.Model;

namespace Timesheet.BLL.Interfaces
{
    public interface ITimesheetService
    {
        Task SetStartTimeForEmployee(int employeeId);

        Task SetEndTimeForEmployee(int employeeId);

        Task<bool> EndTimeSetAsync(int employeeId,
                                   DateTime endDateTime,
                                   DateTime overtime,
                                   DateTime pauseTime);

        Task<List<Model.Timesheet>> PeriodTimesheetGetAsync(int employeeId,
                                                            DateTime startDate,
                                                            DateTime endDate);
    }
}
