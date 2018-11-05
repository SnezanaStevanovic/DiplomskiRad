using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Model.APIModel
{
    public class BaseResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }
    }
}
