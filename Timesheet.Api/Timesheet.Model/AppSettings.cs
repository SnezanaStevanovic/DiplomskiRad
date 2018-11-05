using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Model
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }

        public string PasswordSalt { get; set; }

        public string Secret { get; set; }
    }
}
