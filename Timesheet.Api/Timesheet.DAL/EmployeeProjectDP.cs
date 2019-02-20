using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Timesheet.DAL.Interfaces;
using Timesheet.Model;

namespace Timesheet.DAL
{
    public class EmployeeProjectDP : IEmployeeProjectDP
    {
        private readonly ILogger<EmployeeProjectDP> _logger;

        #region SQLQueries
        private const string INSERT =
            @"INSERT INTO 
                EmployeeProject
              (
                EmployeeId,
                ProjectId
              )
              VALUES
              (
                @EmployeeId,
                @ProjectId
              );
             ";

        private const string DELETE =
            @"DELETE FROM 
                    EmployeeProject
              WHERE
                    EmployeeId = @EmployeeId
              AND   ProjectId = @ProjectId;
             ";
        #endregion

        private readonly AppSettings _appSettings;
        public EmployeeProjectDP(IOptions<AppSettings> appSettings, ILogger<EmployeeProjectDP> logger)
        {
            _appSettings = appSettings.Value;
            _logger = logger;
        }


        public async Task InsertAsync(EmployeeProject employeeProject)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (SqlCommand cmd = new SqlCommand(INSERT, connection))
                    {
                        cmd.Parameters.AddWithValue("EmployeeId", employeeProject.EmployeeId);
                        cmd.Parameters.AddWithValue("ProjectId", employeeProject.ProjectId);

                        await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(EmployeeProjectDP)}.{MethodBase.GetCurrentMethod().Name}");
                throw;
            }

        }

        public async Task RemoveAsync(EmployeeProject employeeProject)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (SqlCommand cmd = new SqlCommand(DELETE, connection))
                    {
                        cmd.Parameters.AddWithValue("EmployeeId", employeeProject.EmployeeId);
                        cmd.Parameters.AddWithValue("ProjectId", employeeProject.ProjectId);

                        await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(EmployeeProjectDP)}.{MethodBase.GetCurrentMethod().Name}");
                throw;
            }
        }
    }
}
