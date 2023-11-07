using SportTeamManagementApp.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportTeamManagementApp.Data.Enums;

namespace SportTeamManagementApp.Data.Models
{
    public class CoachModel : TeamMember, ISoccerCoach
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

        public void RemovePlayerFromTeam(TeamModel team, PlayerModel soccerPlayer)
        {
            throw new NotImplementedException();
        }
    }
}
