
using SportTeamManagementApp.Data.Entities;

namespace SportTeamManagementApp.Data.Models.Interfaces
{
    public interface ISoccerCoach
    {
        void RemovePlayerFromTeam(Team team ,Player soccerPlayer);
    }
}
