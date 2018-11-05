using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Model
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime EstimatedTime { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime SpentTime { get; set; }

        public string Progress { get; set; }

        public List<ProjectTask> ProjectTasks { get; set; }

        public List<Employee> ProjectEmployees { get; set; }
    }
}
