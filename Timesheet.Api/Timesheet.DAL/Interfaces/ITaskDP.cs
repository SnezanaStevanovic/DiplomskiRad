using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Model;

namespace Timesheet.DAL.Interfaces
{
    public interface ITaskDP
    {
        Task Insert(ProjectTask task);

        Task<List<ProjectTask>> TasksPerProjectGet(int projectId);
    }
}
