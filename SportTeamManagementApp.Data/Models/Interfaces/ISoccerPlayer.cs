
using SportTeamManagementApp.Data.Entities;

namespace SportTeamManagementApp.Data.Models.Interfaces
{
    public interface ISoccerPlayer
    {
        void ChangeTeams(Team oldTeam,Team newTeam);
    }
}
