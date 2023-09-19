using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportTeamManagementApp.Models
{
    public class Sport
    {
        private readonly string name;

        public List<Team> teams = new List<Team>();

        public Sport(string name)
        {
            this.name = name;
        }

    }
}
