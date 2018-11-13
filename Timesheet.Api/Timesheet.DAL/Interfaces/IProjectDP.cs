using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Model;

namespace Timesheet.DAL.Interfaces
{
    public interface IProjectDP
    {
        Task Insert(ProjectTask project);

        Task<List<ProjectTask>> GetAll();
    }
}
