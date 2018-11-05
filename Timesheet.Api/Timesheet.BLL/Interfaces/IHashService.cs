using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.BLL.Interfaces
{
    public interface IHashService
    {
        string GetMd5Hash(string cryptedText);
    }
}
