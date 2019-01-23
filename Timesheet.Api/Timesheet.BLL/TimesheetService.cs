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

        public async Task EndTimeSetAsync(int employeeId,
                                          DateTime endDateTime,
                                          DateTime overtime,
                                          DateTime pauseTime)
        {
            try
            {
                await _timesheetDP.UpdateEndTimeAsync(employeeId,
                                                      endDateTime,
                                                      pauseTime,
                                                      overtime).ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                Logger.Error($"{ex}");
            }

        }

        public async Task<List<Model.Timesheet>> PeriodTimesheetGetAsync(int employeeId,
                                                                         DateTime startDate,
                                                                         DateTime endDate)
        {
            List<Model.Timesheet> retValue = new List<Model.Timesheet>();

            try
            {
                retValue = await _timesheetDP.PeriodTimeshetGetAsync(employeeId,
                                                                     startDate,
                                                                     endDate).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.Error($"{ex}");
            }

            return retValue;
        }

        public async Task StartTimeSetAsync(int employeeId,
                                            DateTime startDateTime)
        {
            try
            {
                await _timesheetDP.InsertStartTimeAsync(employeeId,
                                                        startDateTime).ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                Logger.Error($"{ex}");
            }

        }

    }
}
