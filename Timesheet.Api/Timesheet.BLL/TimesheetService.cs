using Microsoft.Extensions.Logging;
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
        private readonly ILogger<TimesheetService> _logger;
        private readonly ITimesheetDP _timesheetDP;

        public TimesheetService(ITimesheetDP timesheetDP, ILogger<TimesheetService> logger)
        {
            _timesheetDP = timesheetDP;
            _logger = logger;
        }


        public Task<List<Model.Timesheet>> PeriodTimesheetGetAsync(int employeeId,
                                                                         DateTime startDate,
                                                                         DateTime endDate)
        {

            if (startDate == default || endDate == default)
            {
                throw new Exception("Incorect startDate or endDate");
            }

             return _timesheetDP.PeriodTimeshetGetAsync(employeeId,startDate,endDate);
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
                _logger.LogError(ex, $"{nameof(TimesheetService)}.{MethodBase.GetCurrentMethod().Name}");
                throw;
            }
        }
    }
}
