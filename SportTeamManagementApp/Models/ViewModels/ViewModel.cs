using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using SportTeamManagementApp.Models;

namespace SportTeamManagementApp.Pages
{  
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Sport Soccer { get; set; }
        public List<Coach> Coaches { get; set; }
        public List<Player> Players { get; set; }
        public List<Team> Teams { get; set; }

        public List<Player> SelectedPlayersForTeam { get; set; }

        public Team TeamSelectedForEdit { get; set; }
        public Player PlayerSelectedForEdit { get; set; }
        public Coach CoachSelectedForEdit { get; set; }

        public ViewModel()
        {
            Soccer = new Sport("Soccer");
            Coaches = new List<Coach>();
            Players = new List<Player>();
            Teams = new List<Team>();
            SelectedPlayersForTeam = new List<Player>();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<Player> GetAvailablePlayers()
        {
            return Players
                        .Where(p => !Teams
                        .Any(t => t.players.Contains(p)))
                        .Select(player => player)
                        .ToList();
        }

        public List<Coach> GetAvailableCoaches()
        {
            return Coaches
                      .Where(c => !Teams
                      .Where(t => t.coach != null)
                      .Any(t => t.coach.Equals(c)))
                      .Select(c => c).ToList();
        }

        public List<Team> GetAvailableTeams()
        {
            return Teams.Select(t => t).ToList();
        }
    }

}
