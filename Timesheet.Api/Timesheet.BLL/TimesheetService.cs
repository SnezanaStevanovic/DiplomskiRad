using log4net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheet.BLL.Interfaces;
using Timesheet.DAL.Interfaces;
using Timesheet.Model;
using Timesheet.Model.APIModel;

namespace Timesheet.BLL
{
    public class TimesheetService : ITimesheetService
    {
        private ILog Logger { get; } = LogManager.GetLogger(typeof(TimesheetService));

        private readonly ITimesheetDP _timesheetDP;

        public TimesheetService(ITimesheetDP timesheetDP)
        {
            _timesheetDP = timesheetDP;
        }

        public async Task<bool> EndTimeSetAsync(int employeeId,
                                                DateTime endDateTime,
                                                DateTime overtime,
                                                DateTime pauseTime)
        {

            bool retVal = await _timesheetDP.UpdateEndTimeAsync(employeeId,
                                                                endDateTime,
                                                                pauseTime,
                                                                overtime)
                                            .ConfigureAwait(false);

            return retVal;

        }

        public async Task<List<Model.Timesheet>> PeriodTimesheetGetAsync(int employeeId,
                                                                         DateTime startDate,
                                                                         DateTime endDate)
        {
            List<Model.Timesheet> retValue = await _timesheetDP.PeriodTimeshetGetAsync(employeeId,
                                                                                       startDate,
                                                                                       endDate)
                                                               .ConfigureAwait(false);

            return retValue;
        }

        public async Task SetStartTimeForEmployee(int employeeId)
        {

            await _timesheetDP.AddStartTimeAsync(employeeId)
                              .ConfigureAwait(false);


        }


        public async Task SetEndTimeForEmployee(int employeeId)
        {
            await _timesheetDP.UpdateEndTimeAsync(employeeId)
                              .ConfigureAwait(false);

        }
    }
}
