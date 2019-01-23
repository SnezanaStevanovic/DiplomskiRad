using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Model;

namespace Timesheet.BLL.Interfaces
{
    public interface IEmployeeProjectService
    {
        Task AddNewAsync(int employeeId, int projectId);
    }
}
