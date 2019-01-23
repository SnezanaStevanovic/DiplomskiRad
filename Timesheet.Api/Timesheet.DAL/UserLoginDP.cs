﻿using log4net;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Timesheet.Common;
using Timesheet.DAL.Interfaces;
using Timesheet.Model;

namespace Timesheet.DAL
{
    public class UserLoginDP : IUserLoginDP
    {

        private ILog Logger { get; } = LogManager.GetLogger(typeof(UserLoginDP));

        private readonly AppSettings _config;

        #region SqlQueries
        private const string GET_ALL =
            @"
               SELECT 
                        *
               FROM
                    UserLogin
            ";

        private const string INSERT =
            @"
               INSERT INTO
                  UserLogin(
                            Email,
                            Password
                           )
               OUTPUT Inserted.Id
               VALUES
                           (
                            @Email,
                            @Password
                            )";

        private const string GET_USER_BY_EMAIL =
            @"SELECT 
                    *
              FROM
                    UserLogin
              WHERE 
                    Email = @Email;
            ";
        #endregion
        public UserLoginDP(IOptions<AppSettings> config)
        {
            this._config = config.Value;
        }

        public async Task<List<UserLogin>> GetAllAsync()
        {
            List<UserLogin> allUsers = new List<UserLogin>();
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
                            while (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                UserLogin dbUser = await this.Create(reader);
                                allUsers.Add(dbUser);
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                this.Logger.Error($"{ex.Message} StackTrace: {ex.StackTrace}");
                
            }

            return allUsers;
        }

        private async Task<UserLogin> Create(SqlDataReader reader)
        {
            UserLogin user = new UserLogin();
            try
            {
                user.Id = await SqlParamHelper.ReadReaderValue<int>(reader,"Id");
                user.Email = await SqlParamHelper.ReadReaderValue<string>(reader, "Email");
                user.Password = await SqlParamHelper.ReadReaderValue<string>(reader, "Password");
            }
            catch (Exception ex)
            {
                this.Logger.Error($"{ex.Message} StackTrace: {ex.StackTrace}");
                
            }

            return user;
        }

        public async Task InsertAsync(UserLogin user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this._config.ConnectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);
                    using (SqlCommand cmd = new SqlCommand(INSERT, connection))
                    {
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@Password", user.Password);

                        user.Id = (int)await cmd.ExecuteScalarAsync()
                                                .ConfigureAwait(false);
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                this.Logger.Error($"{ex.Message} StackTrace: {ex.StackTrace}");
                
            }
        }

        public async Task<UserLogin> GetUserByEmailAsync(string email)
        {
            UserLogin user = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(this._config.ConnectionString))
                {
                    await connection.OpenAsync()
                                    .ConfigureAwait(false);

                    using (SqlCommand cmd = new SqlCommand(GET_USER_BY_EMAIL, connection))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                user = await this.Create(reader);
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                this.Logger.Error($"{ex.Message} StackTrace: {ex.StackTrace}");
                
            }

            return user;
        }
    }
}
