using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Model.APIModel
{
    public class TasksListResponse : BaseResponse
    {
        public List<ProjectTask> ProjectTasks { get; set; }
    }
}