using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Model;

namespace Timesheet.DAL.Interfaces
{
    public interface IUserLoginDP
    {
        Task InsertAsync(UserLogin user);

        Task<List<UserLogin>> GetAllAsync();

        Task<UserLogin> GetUserByEmailAsync(string email);

    }
}
