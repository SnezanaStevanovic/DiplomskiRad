using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Model;

namespace Timesheet.Model.APIModel
{
    public class RegistrationRequest
    {
        public Employee NewEmployee { get; set; }
        public UserLogin NewUser { get; set; }
        public List<int> ProjectIds { get; set; } = new List<int>();

    }
}
