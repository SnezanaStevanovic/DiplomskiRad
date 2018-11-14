using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Timesheet.Common
{
    public static class SqlParamHelper
    {


        public async static Task<T?> StringToEnum<T>(SqlDataReader reader, string columnName) where T : struct
        {
            string existingEnum = await ReadReaderValue<string>(reader, columnName);

            return existingEnum == null ? (T?)null : (T)Enum.Parse(typeof(T), existingEnum);
        }

        public static DateTime? DateTimeGet(SqlDataReader reader, string columnName)
        {
           var sqlDateTime = reader[columnName];

            DateTime? dt = (sqlDateTime == System.DBNull.Value)
                            ? (DateTime?)null
                            : Convert.ToDateTime(sqlDateTime);

            return dt;
        }

        public async static Task<T> ReadReaderValue<T>(SqlDataReader reader, string columnName)
        {
            if (!await reader.IsDBNullAsync(reader.GetOrdinal(columnName)))
            {
                return (T)reader[columnName];
            }

            return default(T);

        }

    }
}
