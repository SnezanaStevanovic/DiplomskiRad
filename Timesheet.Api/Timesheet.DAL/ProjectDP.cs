using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheet.DAL.Interfaces;
using Timesheet.Model;

namespace Timesheet.DAL
{
    public class ProjectDP : IProjectDP
    {
        public Task<List<Project>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Insert(Project project)
        {
            throw new NotImplementedException();
        }
    }
}
