using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Common;
using Timesheet.Common.Extensions;
using Timesheet.DAL.Interfaces;
using Timesheet.Model;

namespace Timesheet.DAL
{
    public class TimesheetDP : ITimesheetDP
    {
        private readonly ILogger<TimesheetDP> _logger;

        private readonly AppSettings _appSettings;

        #region SqlQueries

        private string PERIOD_TIMESHEET_GET =
            @"
                 SELECT *
                 FROM
                        Timesheet
                 WHERE EmployeeId = @EmployeeId
                 AND StartTime BETWEEN @StartDateTime AND @EndDateTime
            ";

        private string INSERT_START_TIME =
            @"INSERT INTO
                   Timesheet
              (
               EmployeeId,
               StartTime
              )
              VALUES
              (
                @EmployeeId,
                GETUTCDATE()
                );
             ";



        private string UPDATE_ONLY_END_TIME =
           @"
            UPDATE 
                  Timesheet
             SET 
                EndTime = GETUTCDATE()
             WHERE 
                EmployeeId = @EmployeeId
                AND  Id= (SELECT MAX(Id) FROM Timesheet)
             ";

        #endregion

        public TimesheetDP(IOptions<AppSettings> appSetiings, ILogger<TimesheetDP> logger)
        {
            _appSettings = appSetiings.Value;
            _logger = logger;
        }

        public async Task<List<Model.Timesheet>> PeriodTimeshetGetAsync(int employeeId,
                                                                        DateTime startDateTime,
                                                                        DateTime endDateTime)
        {
            List<Model.Timesheet> retValue = new List<Model.Timesheet>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(PERIOD_TIMESHEET_GET, connection))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                        cmd.Parameters.AddWithValue("@EndDateTime", endDateTime);
                        cmd.Parameters.AddWithValue("@StartDateTime",startDateTime);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Model.Timesheet timesheet = await Create(reader);
                                retValue.Add(timesheet);
                            }

                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(TimesheetDP)}.{MethodBase.GetCurrentMethod().Name}");
                throw;
            }

         

            return retValue;
        }

        private async Task<Model.Timesheet> Create(SqlDataReader reader)
        {
            Model.Timesheet timesheet = new Model.Timesheet();
            try
            {
                timesheet.Id = await SqlParamHelper.ReadReaderValue<int>(reader, "Id");
                timesheet.EmployeeId = await SqlParamHelper.ReadReaderValue<int>(reader, "EmployeeId");
                timesheet.StartTime = await SqlParamHelper.ReadReaderValue<DateTime>(reader, "StartTime");
                timesheet.EndTime = await SqlParamHelper.ReadReaderDateTimeNullableValue(reader, "EndTime");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(TimesheetDP)}.{MethodBase.GetCurrentMethod().Name}");
                throw;

            }

            return timesheet;
        }

        public async Task AddStartTimeAsync(int employeeId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionString))
                {
                    await connection.OpenAsync()
                                    .ConfigureAwait(false);

                    using (SqlCommand cmd = new SqlCommand(INSERT_START_TIME, connection))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                        await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(TimesheetDP)}.{MethodBase.GetCurrentMethod().Name}");
                throw;
            }

        }

        public async Task UpdateEndTimeAsync(int employeeId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionString))
                {
                    await connection.OpenAsync()
                                    .ConfigureAwait(false);

                    using (SqlCommand cmd = new SqlCommand(UPDATE_ONLY_END_TIME, connection))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                       
                        await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(TimesheetDP)}.{MethodBase.GetCurrentMethod().Name}");
                throw;
            }

        }
    }
}
