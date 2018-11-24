using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model = Timesheet.Model;

namespace Timesheet.DAL.Interfaces
{
    public interface ITimesheetDP
    {
        Task<List<Model.Timesheet>> PeriodTimeshetGet(int employeeId,
                                                      DateTime startDate,
                                                      DateTime endDate);

        Task InsertStartTime(int EmployeeId,
                             DateTime StartTime);

        Task UpdateEndTime(int EmployeeId,
                           DateTime Pause,
                           DateTime Overtime,
                           DateTime EndTime);
    }
}
