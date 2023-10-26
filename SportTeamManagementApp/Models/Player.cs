using SportTeamManagementApp.Enums;
using SportTeamManagementApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportTeamManagementApp.Models
{
    public class Player : TeamMember, ISoccerPlayer
    {
        private int dribbleSkill;

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

        public int DribbleSkill
        {
            get
            {
                return this.dribbleSkill;
            }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    this.dribbleSkill = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Value needs to be between 0 and 100" );
                }
            }
        }

        public void ChangeTeams(Team oldTeam, Team newTeam)
        {
            throw new NotImplementedException();
        }
    }
}
