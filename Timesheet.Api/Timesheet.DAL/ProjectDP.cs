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
    public class ProjectDP : IProjectDP
    {
        private ILog Logger { get; } = LogManager.GetLogger(typeof(ProjectDP));

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
                StartDate,
                EndDate,
                SpentTime,
                Progress
              )
              VALUES 
              (
                @Name,
                @EstimatedTime,
                @StartDate,
                @EndDate,
                @SpentTime,
                @Progress  
              )
            ";

        private const string GET_BY_ID =
            @"SELECT 
                     Name,
                     EstimatedTime,
                     StartDate,
                     EndDate,
                     SpentTime,
                     Progress
               FROM
                     Project
               WHERE Id = @ProjectId;
             ";

        private const string GET_ALL_PROJECTS_FOR_EMPLOYEE =
            @"SELECT 
                     Name,
                     EstimatedTime,
                     StartDate,
                     EndDate,
                     SpentTime,
                     Progress
             FROM Project p 
             INNER JOIN EmployeeProject ep
             ON p.Id = ep.ProjectId
             WHERE ep.EmployeeId = @EmployeeId";

        #endregion

        private readonly AppSettings _config;
        public ProjectDP(IOptions<AppSettings> config)
        {
            _config = config.Value;
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
                            while (reader != null)
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
                this.Logger.Error($"ERROR ProjectDP.GetAll() method. Details: {ex.Message} StackTrace: {ex.StackTrace}");
            }

            return retValProjects;
        }

        private async Task<Project> Create(SqlDataReader reader)
        {
            Project project = new Project();
            try
            {
                try
                {
                    project.Id = await SqlParamHelper.ReadReaderValue<int>(reader, "Id");
                    project.Name = await SqlParamHelper.ReadReaderValue<string>(reader, "Name");
                    project.EstimatedTime = await SqlParamHelper.ReadReaderValue<DateTime>(reader, "EstimatedTime");
                    project.DateCreated = await SqlParamHelper.ReadReaderValue<DateTime>(reader, "DateCreated");
                    project.EndDate = await SqlParamHelper.ReadReaderValue<DateTime>(reader, "EndDate");
                    project.SpentTime = await SqlParamHelper.ReadReaderValue<DateTime>(reader, "SpentTime");
                    project.Progress = await SqlParamHelper.ReadReaderValue<string>(reader, "Progress");

                }
                catch (Exception ex)
                {
                    this.Logger.Error($"{ex.Message} StackTrace: {ex.StackTrace}");

                }
            }
            catch (Exception ex)
            {
                this.Logger.Error($"ERROR ProjectDP.Create() method. Details: {ex.Message} StackTrace: {ex.StackTrace}");
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
                        cmd.Parameters.AddWithValue("@EstimatedTime", project.EstimatedTime == null ? DBNull.Value : (object)project.EstimatedTime);
                        cmd.Parameters.AddWithValue("@DateCreated", project.DateCreated == null ? DBNull.Value : (object)project.DateCreated);
                        cmd.Parameters.AddWithValue("@EndDate", project.EndDate == null ? DBNull.Value : (object)project.EndDate);
                        cmd.Parameters.AddWithValue("@SpentTime", project.SpentTime == null ? DBNull.Value : (object)project.SpentTime);
                        cmd.Parameters.AddWithValue("@Progress", project.Progress == null ? DBNull.Value : (object)project.Progress);


                        await cmd.ExecuteNonQueryAsync()
                                 .ConfigureAwait(false);
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                this.Logger.Error($"ERROR ProjectDP.Insert() method. Details: {ex.Message} StackTrace: {ex.StackTrace}");

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
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (reader != null)
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
                Logger.Error($"{ex}");
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
                            while (reader != null)
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
                this.Logger.Error($"ERROR ProjectDP.GetAll() method. Details: {ex.Message} StackTrace: {ex.StackTrace}");
            }

            return retValProjects;
        }
    }
}
