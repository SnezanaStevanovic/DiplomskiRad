using log4net;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Common;
using Timesheet.DAL.Interfaces;
using Timesheet.Model;

namespace Timesheet.DAL
{
    public class TimesheetDP : ITimesheetDP
    {
        private ILog Logger { get; } = LogManager.GetLogger(typeof(TimesheetDP));

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

        private string INSERT_END_TIME =
            @"INSERT INTO
                   Timesheet
              (
               EmployeeId,
               EndTime
              )
              VALUES
              (
                @EmployeeId,
                @EndTime
                );
             ";

        private string UPDATE_END_TIME =
           @"
             UPDATE 
                  Timesheet
             SET 
                EndTime = @EndTime, 
                Pause = @Pause,
                Overtime = @Overtime
             WHERE 
                EmployeeId = @EmployeeId;
             ";

        private string UPDATE_ONLY_END_TIME =
           @"
             UPDATE 
                  Timesheet
             SET 
                EndTime = GETUTCDATE()
             WHERE 
                EmployeeId = @EmployeeId;
             ";

        #endregion

        public TimesheetDP(IOptions<AppSettings> appSetiings)
        {
            _appSettings = appSetiings.Value;
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
                        cmd.Parameters.AddWithValue("@EndDateTime", endDateTime == DateTime.MinValue ? DBNull.Value : (object)endDateTime);
                        cmd.Parameters.AddWithValue("@StartDateTime", startDateTime == DateTime.MinValue ? DBNull.Value : (object)startDateTime);

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
                Logger.Error($"{ex}");
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
                timesheet.Overtime = await SqlParamHelper.ReadReaderDateTimeNullableValue(reader, "Overtime");
                timesheet.Pause = await SqlParamHelper.ReadReaderDateTimeNullableValue(reader, "Pause");
                timesheet.StartTime = await SqlParamHelper.ReadReaderValue<DateTime>(reader, "StartTime");
                timesheet.EndTime = await SqlParamHelper.ReadReaderDateTimeNullableValue(reader, "EndTime");

            }
            catch (Exception ex)
            {
                this.Logger.Error($"{ex}");
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
                Logger.Error($"{ex}");
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
                Logger.Error($"{ex}");
                throw;
            }

        }


        public async Task<bool> UpdateEndTimeAsync(int EmployeeId,
                                                   DateTime Pause,
                                                   DateTime Overtime,
                                                   DateTime EndTime)
        {
            bool retVal = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionString))
                {
                    await connection.OpenAsync()
                                    .ConfigureAwait(false);

                    using (SqlCommand cmd = new SqlCommand(UPDATE_END_TIME, connection))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                        cmd.Parameters.AddWithValue("@EndTime", EndTime == null ? DBNull.Value : (object)EndTime);
                        cmd.Parameters.AddWithValue("@Overtime", Overtime == null ? DBNull.Value : (object)Overtime);
                        cmd.Parameters.AddWithValue("@Pause", Pause == null ? DBNull.Value : (object)Pause);

                        int affeectedRows = await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                        if (affeectedRows > 0)
                        {
                            retVal = true;
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{ex}");
                throw;
            }

            return retVal;
        }
    }
}
