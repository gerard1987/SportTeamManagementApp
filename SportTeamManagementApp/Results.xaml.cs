using SportTeamManagementApp.Data.Entities;
using SportTeamManagementApp.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SportTeamManagementApp
{
    public sealed partial class Results : UserControl
    {
        ViewModel viewModel;
        public List<Match> Matches { get; set; } = new List<Match>();

        private FileSystemWatcher fileWatcher;
        private string localFolder = ApplicationData.Current.LocalFolder.Path;
        private string jsonMatchesFilePath =  "matches.json"; // Replace with your JSON file path

        public Results()
        {
            this.InitializeComponent();

            viewModel = App.SharedViewModel;

            var matchDetails = viewModel.dataProvider.Matches
                .Select(match => new
                {
                    MatchId = match.Id,
                    HomeTeamName = match.HomeTeam.Name,
                    AwayTeamName = match.AwayTeam.Name,
                    HomeTeamScore = match.HomeTeamScore,
                    AwayTeamScore = match.AwayTeamScore
                })
                .ToList();

            MatchesListBox.ItemsSource = matchDetails;

        }
    }

}
