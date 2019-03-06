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
    public class ProjectDP : IProjectDP
    {
        private readonly ILogger<ProjectDP> _logger;

        #region SqlQueries

        private const string GET_ALL =
            @"SELECT
                    *
              FROM
                    Project";

        private const string INSERT =
            @"INSERT INTO
              Project
              (
                Name,
                EstimatedTime,
                DateCreated,
                EndDate,
                SpentTime,
                Progress,
                Description
              )
              VALUES
              (
                @Name,
                @EstimatedTime,
                GETUTCDATE(),
                NULL,
                NULL,
                0,
                @Description
              )
            ";

        private const string GET_BY_ID =
            @"SELECT
                     Id,
                     Name,
                     EstimatedTime,
                     DateCreated,
                     EndDate,
                     SpentTime,
                     Progress
               FROM
                     Project
               WHERE Id = @ProjectId;
             ";

        private const string GET_ALL_PROJECTS_FOR_EMPLOYEE =
            @"SELECT
                     p.Id,
                     p.Name,
                     p.EstimatedTime,
                     p.DateCreated,
                     p.EndDate,
                     p.SpentTime,
                     p.Progress
             FROM Project p
             INNER JOIN EmployeeProject ep
             ON p.Id = ep.ProjectId
             WHERE ep.EmployeeId =  @EmployeeId";

        #endregion SqlQueries

        private readonly AppSettings _config;

        public ProjectDP(IOptions<AppSettings> config, ILogger<ProjectDP> logger)
        {
            _config = config.Value;
            _logger = logger;
        }

        public async Task<List<Project>> GetAllAsync()
        {
            List<Project> retValProjects = new List<Project>();
            try
            {
                using (SqlConnection connection = new SqlConnection(this._config.ConnectionString))
                {
                    await connection.OpenAsync()
                                    .ConfigureAwait(false);

                    using (SqlCommand cmd = new SqlCommand(GET_ALL, connection))
                    {
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync())
                            {
                                Project project = await this.Create(reader);
                                retValProjects.Add(project);
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ProjectDP)}.{MethodBase.GetCurrentMethod().Name}");
                throw;
            }

            return retValProjects;
        }

        private async Task<Project> Create(SqlDataReader reader)
        {
            Project project = new Project();
            try
            {
                project.Id = await SqlParamHelper.ReadReaderValue<int>(reader, "Id");
                project.Name = await SqlParamHelper.ReadReaderValue<string>(reader, "Name");
                project.EstimatedTime = await SqlParamHelper.ReadReaderDateTimeNullableValue(reader, "EstimatedTime");
                project.DateCreated = await SqlParamHelper.ReadReaderValue<DateTime>(reader, "DateCreated");
                project.EndDate = await SqlParamHelper.ReadReaderDateTimeNullableValue(reader, "EndDate");
                project.SpentTime = await SqlParamHelper.ReadReaderDateTimeNullableValue(reader, "SpentTime");
                project.Progress = await SqlParamHelper.ReadReaderValue<int>(reader, "Progress");
                project.Description = await SqlParamHelper.ReadReaderValue<string>(reader, "Description");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ProjectDP)}.{MethodBase.GetCurrentMethod().Name}");
                throw;
            }

            return project;
        }

        public async Task InsertAsync(Project project)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this._config.ConnectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);
                    using (SqlCommand cmd = new SqlCommand(INSERT, connection))
                    {
                        cmd.Parameters.AddWithValue("@Name", project.Name);
                        cmd.Parameters.AddWithValue("@EstimatedTime", (object)project.EstimatedTime ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(project.Description) ? DBNull.Value : (object)project.Description);

                        await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ProjectDP)}.{MethodBase.GetCurrentMethod().Name}");
                throw;
            }
        }

        public async Task<Project> GetByIdAsync(int projectId)
        {
            Project project = new Project();
            try
            {
                using (SqlConnection connection = new SqlConnection(this._config.ConnectionString))
                {
                    await connection.OpenAsync()
                                    .ConfigureAwait(false);

                    using (SqlCommand cmd = new SqlCommand(GET_BY_ID, connection))
                    {
                        cmd.Parameters.AddWithValue("@ProjectId", projectId);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync())
                            {
                                project = await this.Create(reader);
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ProjectDP)}.{MethodBase.GetCurrentMethod().Name}");
                throw;
            }

            return project;
        }

        public async Task<List<Project>> GetAllProjectsForEmployee(int employeeId)
        {
            List<Project> retValProjects = new List<Project>();
            try
            {
                using (SqlConnection connection = new SqlConnection(this._config.ConnectionString))
                {
                    await connection.OpenAsync()
                                    .ConfigureAwait(false);

                    using (SqlCommand cmd = new SqlCommand(GET_ALL_PROJECTS_FOR_EMPLOYEE, connection))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync())
                            {
                                Project project = await this.Create(reader);
                                retValProjects.Add(project);
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ProjectDP)}.{MethodBase.GetCurrentMethod().Name}");
                throw;
            }

            return retValProjects;
        }
    }
}