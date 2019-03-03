using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Model
{
    public class Timesheet
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

    }
}
