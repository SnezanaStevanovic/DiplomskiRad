using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.BLL.Interfaces
{
    public interface IEmployeeTaskService
    {
        Task AddNewAsync(int employeeId, int taskId);
    }
}
