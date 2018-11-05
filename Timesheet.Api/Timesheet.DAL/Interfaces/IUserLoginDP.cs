using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Model;

namespace Timesheet.DAL.Interfaces
{
    public interface IUserLoginDP
    {
        Task Insert(UserLogin user);

        Task<List<UserLogin>> GetAll();

        Task<UserLogin> GetUserByEmail(string email);

    }
}
