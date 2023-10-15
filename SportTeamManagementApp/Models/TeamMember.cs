using SportTeamManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportTeamManagementApp.Models
{
    public abstract class TeamMember
    {
        public int id;
        public string firstName;
        public string lastName;
        public int age;
        public double salary;
        public Enum role;
    }
}
