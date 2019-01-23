using log4net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheet.BLL.Interfaces;
using Timesheet.DAL.Interfaces;
using Timesheet.Model;

namespace Timesheet.BLL
{
    public class EmployeeProjectService : IEmployeeProjectService
    {
        private ILog Logger { get; } = LogManager.GetLogger(typeof(EmployeeProjectService));

        private readonly IEmployeeProjectDP _employeeProjectDP;

        public EmployeeProjectService(IEmployeeProjectDP employeeProjectDP)
        {
            _employeeProjectDP = employeeProjectDP;
        }

        public async Task AddNewAsync(int employeeId, int projectId)
        {
            try
            {
                EmployeeProject employeeProject = new EmployeeProject
                {
                    EmployeeId = employeeId,
                    ProjectId = projectId
                };

                await _employeeProjectDP.InsertAsync(employeeProject)
                                        .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.Error($"{ex}");
            }
        }
    }
}
