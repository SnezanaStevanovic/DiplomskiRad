using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Model
{
    public class EmployeeProject
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int ProjectId { get; set; }
    }
}
