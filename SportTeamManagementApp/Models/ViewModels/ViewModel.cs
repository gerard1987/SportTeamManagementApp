using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using SportTeamManagementApp.Data.Models;
using SportTeamManagementApp.Data;
using SportTeamManagementApp.Data.Entities;

namespace SportTeamManagementApp.Pages
{  
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private AppDbContext _dbContext;
        public DataProvider dataProvider;

        public List<Player> SelectedPlayersForTeam { get; set; } = new List<Player>();
        public Team TeamSelectedForEdit { get; set; }
        public Player PlayerSelectedForEdit { get; set; }
        public Coach CoachSelectedForEdit { get; set; }

        public ViewModel(AppDbContext appDbContext, DataProvider dataProvider)
        {
            _dbContext = appDbContext;
            this.dataProvider = dataProvider;

            SelectedPlayersForTeam = new List<Player>();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
