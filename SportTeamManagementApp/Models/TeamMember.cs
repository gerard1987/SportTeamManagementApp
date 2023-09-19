using SportTeamManagementApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportTeamManagementApp.Models
{
    public abstract class TeamMember : ITeamMember
    {
        public string name;
        public int age;
        public double salary;
        private Role Role;

        public void asignRoleToTeamMember(Role role)
        {
            this.Role = role;
        }
    }
}
