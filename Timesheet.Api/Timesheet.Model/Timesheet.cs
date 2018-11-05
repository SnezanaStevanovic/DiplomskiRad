using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Model
{
    public class Timesheet
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public DateTime WorkTime { get; set; }

        public DateTime Overtime { get; set; }

        public DateTime Pause { get; set; }

        public DateTime VacationStartDate { get; set; }

        public DateTime VacationEndTime { get; set; }

        public DateTime StartPeriod { get; set; }

        public DateTime EndPeriod { get; set; }



    }
}
