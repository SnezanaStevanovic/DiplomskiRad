using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.BLL.Interfaces
{
    public interface ITokenService
    {
        string TokenCreate(string email,
                           string role);
    }
}
