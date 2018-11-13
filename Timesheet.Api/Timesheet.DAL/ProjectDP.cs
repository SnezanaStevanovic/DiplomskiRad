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
        #endregion

        private readonly AppSettings _config;
        public ProjectDP(IOptions<AppSettings> config)
        {
            _config = config.Value;
        }

        public async Task<List<ProjectTask>> GetAll()
        {
            List<ProjectTask> retValProjects = new List<ProjectTask>();
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
                                ProjectTask project = await this.Create(reader);
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
                throw;
            }

            return retValProjects;
        }

        private async Task<ProjectTask> Create(SqlDataReader reader)
        {
            ProjectTask project = new ProjectTask();
            try
            {
                try
                {
                    project.Id = await SqlParamHelper.ReadReaderValue<int>(reader, "Id");
                    project.Name = await SqlParamHelper.ReadReaderValue<string>(reader, "Name");
                    project.Description = await SqlParamHelper.ReadReaderValue<string>(reader, "Desription");
                    project.EstimatedTime = await SqlParamHelper.ReadReaderValue<DateTime>(reader, "EstimatedTime");
                    project.StartDate = await SqlParamHelper.ReadReaderValue<DateTime>(reader, "StartDate");
                    project.EndDate = await SqlParamHelper.ReadReaderValue<DateTime>(reader, "EndDate");
                    project.SpentTime = await SqlParamHelper.ReadReaderValue<DateTime>(reader, "SpentTime");
                    project.Progress = await SqlParamHelper.ReadReaderValue<string>(reader, "Progress");

                }
                catch (Exception ex)
                {
                    this.Logger.Error($"{ex.Message} StackTrace: {ex.StackTrace}");
                    throw;
                }
            }
            catch (Exception ex)
            {
                this.Logger.Error($"ERROR ProjectDP.Create() method. Details: {ex.Message} StackTrace: {ex.StackTrace}");
            }

            return project;
        }

        public async Task Insert(ProjectTask project)
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
                        cmd.Parameters.AddWithValue("@StartDate", project.StartDate == null ? DBNull.Value : (object)project.StartDate);
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
                throw;
            }
        }
    }
}
