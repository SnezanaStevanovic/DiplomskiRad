﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Model;

namespace Timesheet.DAL.Interfaces
{
    public interface ITaskDP
    {
        Task InsertAsync(ProjectTask task);

        Task<List<ProjectTask>> TasksPerProjectGetAsync(int projectId);
    }
}
