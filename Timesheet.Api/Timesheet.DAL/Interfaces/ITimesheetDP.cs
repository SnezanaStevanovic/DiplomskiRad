using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model = Timesheet.Model;

namespace Timesheet.DAL.Interfaces
{
    public interface ITimesheetDP
    {
        Task<List<Model.Timesheet>> PeriodTimeshetGetAsync(int employeeId,
                                                           DateTime startDate,
                                                           DateTime endDate);

        Task InsertStartTimeAsync(int EmployeeId,
                                  DateTime StartTime);

        Task UpdateEndTimeAsync(int EmployeeId,
                                DateTime Pause,
                                DateTime Overtime,
                                DateTime EndTime);
    }
}
