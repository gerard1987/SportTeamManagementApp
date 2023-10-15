using SportTeamManagementConsole.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportTeamManagementConsole.Models
{
    public class Team
    {
        public string name;
        public ISoccerCoach coach;
        public List<ISoccerPlayer> players = new List<ISoccerPlayer>();

        public Team(string name, ISoccerCoach coach, List<ISoccerPlayer> players)
        {
            this.name = name;
            this.coach = coach;
            this.players = players;
        }
    }
}
