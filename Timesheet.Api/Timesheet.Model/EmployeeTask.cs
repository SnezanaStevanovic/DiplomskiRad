using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Model
{
    public class EmployeeTask
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int TaskId { get; set; }
    }
}
