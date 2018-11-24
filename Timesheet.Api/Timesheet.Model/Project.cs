using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Model
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? EstimatedTime { get; set; }

        public DateTime? SpentTime { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? EndDate { get; set; }

        public string Progress { get; set; }
    }
}
