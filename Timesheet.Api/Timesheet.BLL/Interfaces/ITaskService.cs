﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Model;
using Timesheet.Model.APIModel;

namespace Timesheet.BLL.Interfaces
{
    public interface ITaskService
    {
        Task<List<ProjectTask>> TasksPerProjectGetAsync(int projectId);

        Task<List<ProjectTask>> EmployeeTasksGetAsync(int employeeId);

        Task<List<ProjectTask>> EmployeeTasksPerProjectGetAsync(int employeeId, int projectId);

        Task<List<ProjectTask>> EmployeeNTasksGetAsync(int employeeId, int n);

        Task CreateTask(ProjectTask request);
    }
}