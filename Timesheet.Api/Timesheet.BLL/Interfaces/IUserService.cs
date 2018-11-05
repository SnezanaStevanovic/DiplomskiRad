﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Model;
using Timesheet.Model.APIModel;

namespace Timesheet.BLL.Interfaces
{
    public interface IUserService
    {
        Task Register(RegistrationRequest registrationRequest);

    }
}
