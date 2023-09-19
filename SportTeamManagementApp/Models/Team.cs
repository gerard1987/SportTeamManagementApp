using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportTeamManagementApp.Models
{
    public class Team
    {
        public readonly string name;
        public List<Player> players = new List<Player>();

        public Team(string name)
        {
            this.name = name;
        }
    }
}
