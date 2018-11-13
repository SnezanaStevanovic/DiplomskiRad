using log4net;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheet.DAL.Interfaces;
using Timesheet.Model;

namespace Timesheet.DAL
{
    public class TimesheetDP : ITimesheetDP
    {
        private ILog Logger { get; } = LogManager.GetLogger(typeof(TimesheetDP));

        private readonly AppSettings _appSettings;

        #region SqlQueries

        private string TIMESHEET_GET =
            @"
               
            ";
        #endregion

        public TimesheetDP(IOptions<AppSettings> appSetiings)
        {
            _appSettings = appSetiings.Value;
        }

        public Task<List<Model.Timesheet>> PeriodTimeshetGet(DateTime startDate,
                                                             int numberOfDays,
                                                             int employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
