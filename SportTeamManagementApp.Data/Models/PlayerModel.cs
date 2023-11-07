using SportTeamManagementApp.Data.Enums;
using SportTeamManagementApp.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportTeamManagementApp.Data.Models
{
    public class PlayerModel : TeamMember, ISoccerPlayer
    {
        public string Role
        {
            get
            {
                return this.role.ToString();
            }
            set
            {
                if (value != null && Enum.TryParse(value.ToString(), out SoccerPlayerRole soccerCoachRole))
                {
                    this.role = soccerCoachRole;
                }
                else
                {
                    throw new FormatException($"Could not parse value {value} to a SoccerCoachRole");
                }
            }
        }

        public void ChangeTeams(TeamModel oldTeam, TeamModel newTeam)
        {
            throw new NotImplementedException();
        }
    }
}
