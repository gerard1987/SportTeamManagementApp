using SportTeamManagementApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportTeamManagementApp.Models
{
    public class Team
    {
        public string name;
        public Coach coach;
        public List<Player> players = new List<Player>();

        public Team(string name, Coach coach, List<Player> players)
        {
            this.name = name;
            this.coach = coach;
            this.players = players;
        }
    }
}
