using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Model.APIModel
{
    public class TimesheetEndTimeRequest
    {
        public int EmployeeId { get; set; }

        public DateTime Overtime { get; set; }

        public DateTime Pause { get; set; }

        public DateTime EndTime { get; set; }
    }
}
