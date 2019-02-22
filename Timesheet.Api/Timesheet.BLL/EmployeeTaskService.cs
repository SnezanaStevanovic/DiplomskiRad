using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheet.BLL.Interfaces;
using Timesheet.DAL.Interfaces;
using Timesheet.Model;

namespace Timesheet.BLL
{
    public class EmployeeTaskService : IEmployeeTaskService
    {
        private readonly IEmployeeTaskDP _employeeTaskDP;

        public EmployeeTaskService(IEmployeeTaskDP employeeTaskDP)
        {
            _employeeTaskDP = employeeTaskDP;
        }

        public Task AddNewAsync(int employeeId, int taskId)
        {
            EmployeeTask employeeTask = new EmployeeTask
            {
                EmployeeId = employeeId,
                TaskId = taskId
            };

            return _employeeTaskDP.InsertAsync(employeeTask);
        }
    }
}
