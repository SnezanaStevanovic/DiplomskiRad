using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Model.APIModel
{
    public class PeriodTimeheetGetResponse : BaseResponse
    {
        public List<Timesheet> AllTimesheetsForPeriod { get; set; }
    }
}
