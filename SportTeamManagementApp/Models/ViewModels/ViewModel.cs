using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using SportTeamManagementApp.Data.Models;
using SportTeamManagementApp.Data;

namespace SportTeamManagementApp.Pages
{  
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private AppDbContext _dbContext;

        public List<CoachModel> Coaches { get; set; }
        public List<PlayerModel> Players { get; set; }
        public List<TeamModel> Teams { get; set; }

        public List<PlayerModel> SelectedPlayersForTeam { get; set; }

        public TeamModel TeamSelectedForEdit { get; set; }
        public PlayerModel PlayerSelectedForEdit { get; set; }
        public CoachModel CoachSelectedForEdit { get; set; }

        public ViewModel(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;

            Coaches = new List<CoachModel>();
            Players = new List<PlayerModel>();
            Teams = new List<TeamModel>();
            SelectedPlayersForTeam = new List<PlayerModel>();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<PlayerModel> GetAvailablePlayers()
        {
            return Players
                        .Where(p => !Teams
                        .Any(t => t.players.Contains(p)))
                        .Select(player => player)
                        .ToList();
        }

        public List<CoachModel> GetAvailableCoaches()
        {
            return Coaches
                      .Where(c => !Teams
                      .Where(t => t.coach != null)
                      .Any(t => t.coach.Equals(c)))
                      .Select(c => c).ToList();
        }

        public List<TeamModel> GetAvailableTeams()
        {
            return Teams.Select(t => t).ToList();
        }
    }

}
