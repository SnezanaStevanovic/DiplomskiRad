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
                @StartTime
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

        #endregion

        public TimesheetDP(IOptions<AppSettings> appSetiings)
        {
            _appSettings = appSetiings.Value;
        }

        public async Task<List<Model.Timesheet>> PeriodTimeshetGet(int employeeId,
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
                        cmd.Parameters.AddWithValue("@EndDateTime", endDateTime == null ? DBNull.Value : (object)endDateTime);
                        cmd.Parameters.AddWithValue("@StartDateTime", startDateTime == null ? DBNull.Value : (object)startDateTime);

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
                Logger.Error($"ERROR: TimesheetDP.");
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
                timesheet.Overtime = await SqlParamHelper.ReadReaderValue<DateTime>(reader, "Overtime");
                timesheet.Pause = await SqlParamHelper.ReadReaderValue<DateTime>(reader, "Pause");
                timesheet.StartTime = await SqlParamHelper.ReadReaderValue<DateTime>(reader, "StartTime");
                timesheet.EndTime = await SqlParamHelper.ReadReaderValue<DateTime>(reader, "EndTime");

            }
            catch (Exception ex)
            {
                this.Logger.Error($"{ex.Message} StackTrace: {ex.StackTrace}");

            }

            return timesheet;
        }

        public async Task InsertStartTime(int EmployeeId,
                                          DateTime StartTime)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionString))
                {
                    await connection.OpenAsync()
                                    .ConfigureAwait(false);

                    using (SqlCommand cmd = new SqlCommand(INSERT_START_TIME, connection))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                        cmd.Parameters.AddWithValue("@StartTime", StartTime == null ? DBNull.Value : (object)StartTime);

                        await cmd.ExecuteNonQueryAsync()
                                 .ConfigureAwait(false);
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"ERROR: TimesheetDP.InsertStartTime(). Details: {ex.StackTrace}");
            }
        }

        public async Task UpdateEndTime(int EmployeeId,
                                        DateTime Pause,
                                        DateTime Overtime,
                                        DateTime EndTime)
        {
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

                        await cmd.ExecuteNonQueryAsync()
                                 .ConfigureAwait(false);
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"ERROR: TimesheetDP.UpdateEndTime(). Details: {ex.StackTrace}");
            }
        }
    }
}
