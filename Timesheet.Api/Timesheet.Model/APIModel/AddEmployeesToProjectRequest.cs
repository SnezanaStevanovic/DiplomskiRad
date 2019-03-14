using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Model.APIModel
{
    public class AddEmployeesToProjectRequest
    {
        public int ProjectId { get; set; }

        public List<int> EmployeesIds { get; set; }
    }
}