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
                 EmployeeId,
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
                 @EmployeeId,
                 @Name,
                 @Type,
                 @Description,
                 @EstimatedTime,
                 GETUTCDATE(),
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

        private const string GET_EMPLOYEE_TASKS =
            @"SELECT
                    pt.Id,
                    pt.Name,
                    pt.Description,
                    pt.Type,
                    pt.StartDate,
                    pt.EndDate,
                    pt.SpentTime,
                    pt.Progress,
                    pt.EstimatedTime,
                    pt.ProjectId,
                    pt.EmployeeId
              FROM ProjectTask pt
              WHERE pt.EmployeeId = @EmployeeId
            ";

        private const string GET_EMPLOYEE_TASKS_PER_PROJECT =
            @"SELECT
                    pt.Id,
                    pt.Name,
                    pt.Description,
                    pt.Type,
                    pt.StartDate,
                    pt.EndDate,
                    pt.SpentTime,
                    pt.Progress,
                    pt.EstimatedTime,
                    pt.ProjectId,
                    pt.EmployeeId
              FROM ProjectTask pt
              WHERE
                   pt.EmployeeId = @EmployeeId
              AND  pt.ProjectId = @ProjectId
            ";

        private readonly string GET_LAST_N_TASKS_FOR_EMPL = @"
                SELECT
	                *
                FROM ProjectTask
	                WHERE EmployeeId = @EmployeeId
	                ORDER BY Id DESC LIMIT @n;";

        #endregion SqlQueries

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
                        cmd.Parameters.AddWithValue("@EmployeeId", task.EmployeeId);
                        cmd.Parameters.AddWithValue("@Name", task.Name);
                        cmd.Parameters.AddWithValue("@Type", task.Type);
                        cmd.Parameters.AddWithValue("@Progress", task.Progress);
                        cmd.Parameters.AddWithValue("@SpentTime", (object)task.SpentTime ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@EndDate", (object)task.EndDate ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@EstimatedTime", (object)task.EstimatedTime ?? DBNull.Value);
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
                projectTask.EmployeeId = await SqlParamHelper.ReadReaderValue<int>(reader, "EmployeeId");
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

        public async Task<List<ProjectTask>> EmployeeTasksGetAsync(int employeeId)
        {
            List<ProjectTask> employeeTasks = new List<ProjectTask>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (SqlCommand cmd = new SqlCommand(GET_EMPLOYEE_TASKS, connection))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync())
                            {
                                ProjectTask projectTask = await this.Create(reader);
                                employeeTasks.Add(projectTask);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                throw;
            }

            return employeeTasks;
        }

        public async Task<List<ProjectTask>> EmployeeTasksPerProjectGetAsync(int employeeId, int projectId)
        {
            List<ProjectTask> employeeTasksPerProject = new List<ProjectTask>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (SqlCommand cmd = new SqlCommand(GET_EMPLOYEE_TASKS_PER_PROJECT, connection))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                        cmd.Parameters.AddWithValue("@ProjectId", projectId);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync())
                            {
                                ProjectTask projectTask = await this.Create(reader);
                                employeeTasksPerProject.Add(projectTask);
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

            return employeeTasksPerProject;
        }

        public async Task<List<ProjectTask>> EmployeeNTasksGetAsync(int employeeId, int n)
        {
            List<ProjectTask> lastNTasks = new List<ProjectTask>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(GET_LAST_N_TASKS_FOR_EMPL, connection))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                        cmd.Parameters.AddWithValue("@n", n);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var task = await Create(reader);
                                lastNTasks.Add(task);
                            }

                            reader.Close();
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

            return lastNTasks;
        }
    }
}