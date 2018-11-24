using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Model.APIModel
{
    public class PeriodTimeheetGetRequest
    {
        public int EmployeeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
