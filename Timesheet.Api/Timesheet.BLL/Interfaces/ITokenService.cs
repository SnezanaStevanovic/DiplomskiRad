using System;
using System.Collections.Generic;
using System.Text;
using Timesheet.Model;

namespace Timesheet.BLL.Interfaces
{
    public interface ITokenService
    {
        string TokenCreate(string email,
                           Role role,
                           int employeeId);
    }
}
