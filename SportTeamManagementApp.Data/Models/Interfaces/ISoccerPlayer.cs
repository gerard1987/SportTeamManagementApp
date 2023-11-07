using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportTeamManagementApp.Data.Models.Interfaces
{
    public interface ISoccerPlayer
    {
        void ChangeTeams(TeamModel oldTeam,TeamModel newTeam);
    }
}
