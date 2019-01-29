using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Timesheet.BLL.Interfaces;
using Timesheet.Common.Extensions;
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

        public  Task SetStartTimeForEmployee(int employeeId)
        {
            return _timesheetDP.AddStartTimeAsync(employeeId);
        }


        public Task SetEndTimeForEmployee(int employeeId)
        {
             return _timesheetDP.UpdateEndTimeAsync(employeeId);
        }

        public async Task<long> GetTimesheetStateOfDayAsync(int employeeId)
        {
            try
            {
                long res = default;

                DateTime date = DateTime.Today;

                List<Model.Timesheet> timesheets = await _timesheetDP.PeriodTimeshetGetAsync(employeeId,
                                                                           date.StartOfDay().ToUniversalTime(),
                                                                           date.EndOfDay().ToUniversalTime())
                                                               .ConfigureAwait(false);

                if(timesheets.Count == 0)
                {
                    return res;
                }

                Model.Timesheet todayTimesheet = null;

                if(timesheets.Count > 1)
                {
                    todayTimesheet = timesheets.OrderBy(x=> x.Id).FirstOrDefault();
                }
                else
                {
                    todayTimesheet = timesheets[0];
                }

                if(todayTimesheet is null)
                {
                    return res;
                }

                if (todayTimesheet.EndTime.HasValue)
                {
                    return res;
                }

                // get elapsed secounds from start type 
                res = (long)(DateTime.UtcNow - todayTimesheet.StartTime).TotalSeconds;

                return res;
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(TimesheetService)}.{MethodBase.GetCurrentMethod().Name}", ex);
                throw;
            }

        }
    }
}
