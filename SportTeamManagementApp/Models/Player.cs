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

        public Player()
        {

        }

        public void ChangeTeams(Team oldTeam, Team newTeam)
        {
            throw new NotImplementedException();
        }
    }
}
