using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Model.APIModel
{
    public class TimesheetStartTimeRequest
    {
        public int EmployeeId { get; set; }
        public DateTime StartTime { get; set; }
    }
}
