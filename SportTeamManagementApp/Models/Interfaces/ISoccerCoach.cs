using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportTeamManagementApp.Models.Interfaces
{
    public interface ISoccerCoach
    {
        void RemovePlayerFromTeam(Team team ,Player soccerPlayer);
    }
}
