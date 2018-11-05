using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Model
{
    public class Employee
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }

        public int ProjectId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Adress { get; set; }

        public string Gender { get; set; }

    }
}
