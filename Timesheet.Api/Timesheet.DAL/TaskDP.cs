using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Common;
using Timesheet.DAL.Interfaces;
using Timesheet.Model;

namespace Timesheet.DAL
{
    public class TaskDP : ITaskDP
    {
        private readonly ILogger<TaskDP> _logger;
       
        private readonly AppSettings _appSettings;

        #region SqlQueries

        private const string INSERT =
            @"
              INSERT INTO
              ProjectTask
              (  
                 ProjectId,
                 Name,
                 Type,
                 Description,
                 EstimatedTime,
                 StartDate,
                 EndDate,
                 SpentTime,
                 Progress
                )
              VALUES
              ( 
                 @ProjectId,
                 @Name,
                 @Type,
                 @Description,
                 @EstimatedTime,
                 @StartDate,
                 @EndDate,
                 @SpentTime,
                 @Progress
              )";

        private const string GET_TASKS_FOR_PROJECT =
            @"
                SELECT 
                        *
                FROM
                      ProjectTask
                WHERE
                      ProjectId = @ProjectId;
            ";

        #endregion

        public TaskDP(IOptions<AppSettings> appSettings, ILogger<TaskDP> logger)
        {
            _appSettings = appSettings.Value;
            _logger = logger;
        }
        public async Task InsertAsync(ProjectTask task)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(INSERT, connection))
                    {
                        cmd.Parameters.AddWithValue("@ProjectId", task.ProjectId);
                        cmd.Parameters.AddWithValue("@Name", task.Name);
                        cmd.Parameters.AddWithValue("@Type", task.Type);
                        cmd.Parameters.AddWithValue("@Progress", task.Progress);
                        cmd.Parameters.AddWithValue("@SpentTime", task.SpentTime == null ? DBNull.Value : (object)task.SpentTime);
                        cmd.Parameters.AddWithValue("@StartDate", DateTime.UtcNow);
                        cmd.Parameters.AddWithValue("@EndDate", task.EndDate == null ? DBNull.Value : (object)task.EndDate);
                        cmd.Parameters.AddWithValue("@EstimatedTime", task.EstimatedTime == null ? DBNull.Value : (object)task.EstimatedTime);
                        cmd.Parameters.AddWithValue("@Description", task.Description);

                        await cmd.ExecuteNonQueryAsync();
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(TaskDP)}.{MethodBase.GetCurrentMethod().Name}");
                throw;
            }
        }

        public async Task<List<ProjectTask>> TasksPerProjectGetAsync(int projectId)
        {
            List<ProjectTask> allTasksForProject = new List<ProjectTask>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(GET_TASKS_FOR_PROJECT, connection))
                    {
                        cmd.Parameters.AddWithValue("@ProjectId", projectId);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync())
                            {
                                ProjectTask projectTask = await this.Create(reader);
                                allTasksForProject.Add(projectTask);
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(TaskDP)}.{MethodBase.GetCurrentMethod().Name}");
                throw;
            }

            return allTasksForProject;
        }

        private async Task<ProjectTask> Create(SqlDataReader reader)
        {
            ProjectTask projectTask = new ProjectTask();
            try
            {
                projectTask.Id = await SqlParamHelper.ReadReaderValue<int>(reader, "Id");
                projectTask.Description = await SqlParamHelper.ReadReaderValue<string>(reader, "Description");
                projectTask.Name = await SqlParamHelper.ReadReaderValue<string>(reader, "Name");
                projectTask.Progress = await SqlParamHelper.ReadReaderValue<int>(reader, "Progress");
                projectTask.ProjectId = await SqlParamHelper.ReadReaderValue<int>(reader, "ProjectId");
                projectTask.SpentTime = await SqlParamHelper.ReadReaderDateTimeNullableValue(reader, "SpentTime");
                projectTask.StartDate = await SqlParamHelper.ReadReaderValue<DateTime>(reader, "StartDate");
                projectTask.EndDate = await SqlParamHelper.ReadReaderDateTimeNullableValue(reader, "EndDate");
                projectTask.Type = await SqlParamHelper.ReadReaderValue<TaskType>(reader, "Type");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(TaskDP)}.{MethodBase.GetCurrentMethod().Name}");
                throw;
            }

            return projectTask;
        }
    }
}
