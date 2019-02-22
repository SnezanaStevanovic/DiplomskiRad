using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Model;

namespace Timesheet.DAL.Interfaces
{
    public interface IEmployeeTaskDP
    {
        Task InsertAsync(EmployeeTask employeeTask);
    }
}
