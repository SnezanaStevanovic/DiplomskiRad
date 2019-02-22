using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Timesheet.DAL.Interfaces;
using Timesheet.Model;

namespace Timesheet.DAL
{
    public class EmployeeTaskDP : IEmployeeTaskDP
    {
        private readonly ILogger<EmployeeTaskDP> _logger;

        #region SQLQueries
        private const string INSERT =
            @"INSERT INTO 
                EmployeeTask
              (
                EmployeeId,
                TaskId
              )
              VALUES
              (
                @EmployeeId,
                @TaskId
              );
             ";
        #endregion

        private readonly AppSettings _appSettings;

        public EmployeeTaskDP(IOptions<AppSettings> appSettings, ILogger<EmployeeTaskDP> logger)
        {
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        public async Task InsertAsync(EmployeeTask employeeTask)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);
                    using (SqlCommand cmd = new SqlCommand(INSERT, connection))
                    {
                        cmd.Parameters.AddWithValue("EmployeeId", employeeTask.EmployeeId);
                        cmd.Parameters.AddWithValue("TaskId", employeeTask.TaskId);

                        await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                throw;
            }
        }
    }
}
