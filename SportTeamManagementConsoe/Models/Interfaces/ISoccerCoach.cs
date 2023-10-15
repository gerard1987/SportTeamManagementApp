using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportTeamManagementConsole.Models.Interfaces
{
    public interface ISoccerCoach
    {
        void RemovePlayerFromTeam(Team team ,ISoccerPlayer soccerPlayer);
    }
}
