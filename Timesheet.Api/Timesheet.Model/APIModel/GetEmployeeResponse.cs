﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Model.APIModel
{
    public class GetEmployeeResponse : BaseResponse
    {
        public Employee Employee { get; set; }
    }
}
