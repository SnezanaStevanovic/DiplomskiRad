using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Model.APIModel
{
    public class AddNewEmployeeRequest : Employee
    {
        public int ProjectId { get; set; }
    }
}
