using SportTeamManagementApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportTeamManagementApp.Models
{
    public class Coach : TeamMember, ISoccerCoach
    {
        private int strategySkill;

        public int StrategySkill
        {
            get
            {
                return this.strategySkill;
            }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    this.strategySkill = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Value needs to be between 0 and 100");
                }
            }
        }

        public void RemovePlayerFromTeam(Team team, ISoccerPlayer soccerPlayer)
        {

            if (this.Equals(team.coach))
            {
                team.players.Remove(soccerPlayer);
            }
            else
            {
                throw new Exception("Player is not from a team that gets managed by this coach");
            }
        }
    }
}
