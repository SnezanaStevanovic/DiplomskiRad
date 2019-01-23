using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Model.APIModel
{
    public class GetEmployeeRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
