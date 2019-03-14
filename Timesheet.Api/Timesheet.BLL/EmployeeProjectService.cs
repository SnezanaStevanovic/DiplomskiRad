using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Timesheet.BLL.Interfaces;
using Timesheet.DAL.Interfaces;
using Timesheet.Model;

namespace Timesheet.BLL
{
    public class EmployeeProjectService : IEmployeeProjectService
    {
        private readonly ILogger<EmployeeProjectService> _logger;

        private readonly IEmployeeProjectDP _employeeProjectDP;

        public EmployeeProjectService(IEmployeeProjectDP employeeProjectDP, ILogger<EmployeeProjectService> logger)
        {
            _employeeProjectDP = employeeProjectDP;
            _logger = logger;
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
                _logger.LogError(ex, $"{nameof(EmployeeProjectService)}.{MethodBase.GetCurrentMethod().Name}");
                throw;
            }
        }

        public async Task AddEmployeesToProject(int projectId, List<int> employeesIds)
        {
            try
            {
                List<Task> pendingTasks = new List<Task>();

                foreach (var employeeId in employeesIds)
                {
                    pendingTasks.Add(AddNewAsync(employeeId, projectId));
                }

                await Task.WhenAll(pendingTasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(EmployeeProjectService)}.{MethodBase.GetCurrentMethod().Name}");
                throw;
            }
        }
    }
}