using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheet.Model.APIModel
{
    public class LoginResponse : BaseResponse
    {
        public string Token { get; set; }
    }
}
