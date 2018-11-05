using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.DAL.Interfaces
{
    public interface ITaskDP
    {
        Task Insert(Task task);

        List<Task> TasksPerProjectGet(int projectId);
    }
}
