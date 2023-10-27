using SportTeamManagementApp.Enums;
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
        public string Role
        {
            get
            {
                return this.role.ToString();
            }
            set
            {
                if (value != null && Enum.TryParse(value.ToString(), out SoccerCoachRole soccerCoachRole))
                {
                    this.role = soccerCoachRole;
                }
                else
                {
                    throw new FormatException($"Could not parse value {value} to a SoccerCoachRole");
                }
            }
        }

        public void RemovePlayerFromTeam(Team team, Player soccerPlayer)
        {
            throw new NotImplementedException();
        }
    }
}
