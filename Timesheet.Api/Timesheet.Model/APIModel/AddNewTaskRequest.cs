using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Model.APIModel
{
    public class AddNewTaskRequest : ProjectTask
    {
        public int EmployeeId { get; set; }
    }
}
