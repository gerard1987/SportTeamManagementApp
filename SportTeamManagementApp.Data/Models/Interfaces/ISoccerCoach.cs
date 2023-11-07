using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportTeamManagementApp.Data.Models.Interfaces
{
    public interface ISoccerCoach
    {
        void RemovePlayerFromTeam(TeamModel team ,PlayerModel soccerPlayer);
    }
}
