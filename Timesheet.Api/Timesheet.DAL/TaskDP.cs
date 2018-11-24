using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheet.DAL.Interfaces;

namespace Timesheet.DAL
{

    public class TaskDP : ITaskDP
    {
        public Task Insert(Task task)
        {
            throw new NotImplementedException();
        }

        public List<Task> TasksPerProjectGet(int projectId)
        {
            throw new NotImplementedException();
        }
    }
}
