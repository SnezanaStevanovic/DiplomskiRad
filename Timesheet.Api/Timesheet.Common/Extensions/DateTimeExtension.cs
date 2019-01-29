using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Common.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime StartOfDay(this DateTime theDate)
        {
            return theDate.Date;
        }

        public static DateTime EndOfDay(this DateTime theDate)
        {
            return theDate.Date.AddDays(1).AddTicks(-1);
        }
    }
}
