using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Model
{
    public class ProjectTask
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Name { get; set; }

        public TaskTypeEnum Type { get; set; }

        public string Description { get; set; }

        public DateTime EstimatedTime { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime SpentTime { get; set; }

        public string Progress { get; set; }

    }
}
