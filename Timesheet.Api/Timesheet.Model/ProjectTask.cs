using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Model
{
    public class ProjectTask
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public int EmployeeId { get; set; }

        public string Name { get; set; }

        public TaskType Type { get; set; }

        public string Description { get; set; }

        public DateTime? EstimatedTime { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? SpentTime { get; set; }

        public int Progress { get; set; }

        public TaskStatus Status { get; set; }
    }
}