using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model = Timesheet.Model;

namespace Timesheet.DAL.Interfaces
{
    public interface ITimesheetDP
    {
        Task<List<Model.Timesheet>> PeriodTimeshetGet(DateTime startDate,
                                                      int numberOfDays,
                                                      int employeeId);
    }
}
