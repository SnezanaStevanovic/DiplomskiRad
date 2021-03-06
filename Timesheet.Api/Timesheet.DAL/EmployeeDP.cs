﻿using Microsoft.Extensions.Logging;
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
    public class EmployeeDP : IEmployeeDP
    {
        private readonly ILogger<EmployeeDP> _logger;
        private readonly AppSettings _config;


        #region SqlQueries
        private const string GET_ALL =
            @"
               SELECT 
                        *
               FROM
                    Employee;
            ";

        private const string INSERT =
            @"
               INSERT INTO
                      Employee(
                                FirstName,
                                LastName,
                                UserId, 
                                Adress,
                                DateOfBirth,
                                Gender,
                                RoleId
                           )
               OUTPUT INSERTED.ID
               VALUES
                           (
                             @FirstName,
                             @LastName,
                             @UserId, 
                             @Adress,
                             @DateOfBirth,
                             @Gender,
                             @RoleId
                            );";

        private const string GET_EMPLOYEE =
           @"
                SELECT 
                        *
                FROM
                        EMPLOYEE empl
                INNER JOIN 
                        USERLOGIN us 
                ON 
                        empl.UserId = us.Id
                WHERE us.Email = @Email 
                AND
                      us.Password = @hashPass
            ";

        private const string GET_PROJECT_EMPLOYEES =
            @"
                SELECT 
                        * 
                FROM EMPLOYEE emp
                INNER JOIN EmployeeProject ep
                ON emp.Id = ep.EmployeeId
                WHERE
                     ep.ProjectId = @ProjectId
            ";
        #endregion

        public EmployeeDP(IOptions<AppSettings> config, ILogger<EmployeeDP> logger)
        {
            this._config = config.Value;
            _logger = logger;
        }


        public async Task<List<Employee>> GetAllAsync()
        {
            List<Employee> retValEmployees = new List<Employee>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (SqlCommand cmd = new SqlCommand(GET_ALL, connection))
                    {
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync())
                            {
                                Employee employee = await this.Create(reader);
                                retValEmployees.Add(employee);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(EmployeeDP)}.{MethodBase.GetCurrentMethod().Name}");
                throw;
            }

            return retValEmployees;
        }




    public async Task InsertAsync(Employee employee)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (SqlCommand cmd = new SqlCommand(INSERT, connection))
                    {

                        cmd.Parameters.AddWithValue("@UserId", employee.UserId);
                        cmd.Parameters.AddWithValue("@RoleId", employee.Role);
                        cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                        cmd.Parameters.AddWithValue("@DateOfBirth", (object)employee.DateOfBirth ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Gender", employee.Gender.ToString());
                        cmd.Parameters.AddWithValue("@Adress", string.IsNullOrEmpty(employee.Adress) ? DBNull.Value : (object)employee.Adress);

                        employee.Id = (int)await cmd.ExecuteScalarAsync()
                                                    .ConfigureAwait(false);
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(EmployeeDP)}.{MethodBase.GetCurrentMethod().Name}");
                throw;
            }

        }

        public async Task<List<Employee>> ProjectEmployeesGetAsync(int projectId)
        {
            List<Employee> retValEmployees = new List<Employee>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (SqlCommand cmd = new SqlCommand(GET_PROJECT_EMPLOYEES, connection))
                    {
                        cmd.Parameters.AddWithValue("@ProjectId", projectId);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync())
                            {
                                Employee employee = await this.Create(reader);
                                retValEmployees.Add(employee);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(EmployeeDP)}.{MethodBase.GetCurrentMethod().Name}");
                throw;
            }

            return retValEmployees;
        }

        public async Task<Employee> GetEmployeeAsync(string email,
                                                     string hashPass)
        {
            Employee retValeEmployee = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
                {
                    await connection.OpenAsync()
                                    .ConfigureAwait(false);

                    using (SqlCommand cmd = new SqlCommand(GET_EMPLOYEE, connection))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@hashPass", hashPass);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync())
                            {
                                retValeEmployee = await this.Create(reader);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(EmployeeDP)}.{MethodBase.GetCurrentMethod().Name}");
                throw;
            }

            return retValeEmployee;
        }

        private async Task<Employee> Create(SqlDataReader reader)
        {
            Employee employee = new Employee();
            try
            {
                employee.Id = await SqlParamHelper.ReadReaderValue<int>(reader, "Id");
                employee.UserId = await SqlParamHelper.ReadReaderValue<int>(reader, "UserId");
                employee.UserId = Convert.ToInt32(reader["UserId"]);
                employee.Role = await SqlParamHelper.ReadReaderValue<Role>(reader, "RoleId");
                employee.FirstName = await SqlParamHelper.ReadReaderValue<string>(reader, "FirstName");
                employee.LastName = await SqlParamHelper.ReadReaderValue<string>(reader, "LastName");
                employee.Adress = await SqlParamHelper.ReadReaderValue<string>(reader, "Adress");
                employee.Gender =  await SqlParamHelper.StringToEnum<Gender>(reader,nameof(employee.Gender));
                employee.DateOfBirth = await SqlParamHelper.ReadReaderDateTimeNullableValue(reader, "DateOfBirth");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(EmployeeDP)}.{MethodBase.GetCurrentMethod().Name}");
                throw;
            }

            return employee;
        }
    }
}
