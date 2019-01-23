using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Model.APIModel
{
    public class GetProjectByIdResponse : BaseResponse
    {
        public Project Project { get; set; }
    }
}
