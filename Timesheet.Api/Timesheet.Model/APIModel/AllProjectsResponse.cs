using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Model.APIModel
{
    public class AllProjectsResponse : BaseResponse
    {
        public List<Project> Projects { get; set; }
    }
}
