using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Model.APIModel
{
    public class GetWorkingHoursForPeriodResponse : BaseResponse
    {
        public List<HoursPerDay> HoursPerDay { get; set; }
    }
}
