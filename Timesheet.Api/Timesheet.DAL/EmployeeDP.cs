﻿using log4net;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Timesheet.DAL.Interfaces;
using Timesheet.Model;

namespace Timesheet.DAL
{
    public class EmployeeDP : IEmployeeDP
    {
        private ILog Logger { get; } = LogManager.GetLogger(typeof(UserLoginDP));

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
                                ProjectId,
                                Role
                           )
               VALUES
                           (
                             @FirstName,
                             @LastName,
                             @UserId, 
                             @Adress,
                             @DateOfBirth,
                             @Gender,
                             @ProjectId,
                             @Role
                            )";

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
                FROM
                    EMPLOYEE
                WHERE
                     ProjectId = @ProjectId
            ";
        #endregion

        public EmployeeDP(IOptions<AppSettings> config)
        {
            this._config = config.Value;
        }


        public async Task<List<Employee>> GetAll()
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
                            Employee employee = this.Create(reader);
                            retValEmployees.Add(employee);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"ERROR EmployeeDP.GetAll() method. Details: {ex}");
            }

            return retValEmployees;
        }

        public async Task Insert(Employee employee)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (SqlCommand cmd = new SqlCommand(INSERT, connection))
                    {

                        cmd.Parameters.AddWithValue("@UserId", employee.UserId);
                        cmd.Parameters.AddWithValue("@Role", employee.Role);
                        cmd.Parameters.AddWithValue("@ProjectId", employee.ProjectId);
                        cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                        cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                        cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                        cmd.Parameters.AddWithValue("@Adress", employee.Adress);

                        await cmd.ExecuteNonQueryAsync()
                                 .ConfigureAwait(false);
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"ERROR EmployeeDP.Insert() method. Details: {ex}");
            }

        }

        public async Task<List<Employee>> ProjectEmployeesGet(int projectId)
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
                            Employee employee = this.Create(reader);
                            retValEmployees.Add(employee);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"ERROR EmployeeDP.GetAll() method. Details: {ex}");
            }

            return retValEmployees;
        }

        public async Task<Employee> GetEmployee(string email,
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
                                retValeEmployee = this.Create(reader);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"ERROR EmployeeDP.GetEmployee() method. Details: {ex}");
            }

            return retValeEmployee;
        }

        private Employee Create(SqlDataReader reader)
        {
            Employee employee = new Employee();
            try
            {
                employee.Id = Convert.ToInt32(reader["Id"]);
                employee.UserId = Convert.ToInt32(reader["UserId"]);
                employee.Role = (Role)Convert.ToInt32(reader["Role"]);
                employee.ProjectId = Convert.ToInt32(reader["ProjectId"]);
                employee.FirstName = reader["FirstName"].ToString();
                employee.LastName = reader["LastName"].ToString();
                employee.Adress = reader["Adress"].ToString();
                employee.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);

            }
            catch (Exception ex)
            {
                Logger.Error($"ERROR EmployeeDP.Create() method. Details: {ex}");
            }

            return employee;
        }
    }
}
