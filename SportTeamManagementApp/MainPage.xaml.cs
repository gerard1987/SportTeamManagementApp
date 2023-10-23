using SportTeamManagementApp.Enums;
using SportTeamManagementApp.Models;
using SportTeamManagementApp.Models.Interfaces;
using SportTeamManagementApp.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SportTeamManagementApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public Sport soccer = new Sport("Soccer");
        public List<Coach> coaches = new List<Coach>();
        public List<Player> players = new List<Player>();
        public List<Team> teams = new List<Team>();

        public List<Player> selectedPlayersForTeam = new List<Player>();

        public Team teamSelectedForEdit;
        public Player playerSelectedForEdit;
        public Coach coachSelectedForEdit;

        List<StackPanel> sections;

        public MainPage()
        {
            this.InitializeComponent();

            ViewModel viewModel = App.SharedViewModel;
        }

        private void MainMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainMenu.SelectedItem != null)
            {
                string selectedItem = ((ListViewItem)MainMenu.SelectedItem).Content.ToString();

                switch (selectedItem)
                {
                    case "Create Coach":
                        Frame.Navigate(typeof(CoachCreatePage));
                        break;
                    case "Create Player":
                        Frame.Navigate(typeof(PlayerCreatePage));
                        break;
                    case "Create Team":
                        Frame.Navigate(typeof(TeamCreatePage));
                        break;
                    case "Edit Team":
                        Frame.Navigate(typeof(TeamEditPage));
                        break;
                    case "Edit Player":
                        Frame.Navigate(typeof(PlayerEditPage));
                        break;
                    case "Edit Coach":
                        Frame.Navigate(typeof(CoachEditPage));
                        break;
                    case "Exit":
                        Application.Current.Exit();
                        break;
                }

                MainSplitView.IsPaneOpen = false; // Close the menu pane after selecting an item
            }
        }

        private void ClearFields(StackPanel section)
        {
            if (section.Children.Any())
            {
                foreach (var child in section.Children)
                {
                    if (child is TextBox textBox)
                    {
                        textBox.Text = "";
                        string text = textBox.Text;
                    }
                    if (child is ComboBox comboBox)
                    {
                        comboBox.SelectedIndex = 0;
                    }
                }
            }
        }

        private async Task ShowExceptionMessage(string errorMessage)
        {
            ErrorDialog.Content = errorMessage;
            await ErrorDialog.ShowAsync();
        }

        //private List<Player> GetAvailablePlayers()
        //{
        //    return players
        //                .Where(p => !teams
        //                .Any(t => t.players.Contains(p)))
        //                .Select(player => player)
        //                .ToList();
        //}

        //private void SetAvailablePlayers()
        //{
        //    PlayerToEditComboBox.ItemsSource = players.Select(p => new { Key = p.Id, Value = p.firstName }).ToList();

        //    var availablePlayers = players
        //      .Where(p => !teams.Any(t => t.players.Contains(p)))
        //      .Select(p => new { Key = p.Id, Value = p.firstName });

        //    AvailablePlayersComboBox.ItemsSource = availablePlayers;
        //    TeamPlayersComboBox.ItemsSource = availablePlayers;
        //}

        //private void SetAvailableCoaches()
        //{
        //    var availableCoaches = coaches
        //          .Where(c => !teams
        //          .Where(t => t.coach != null)
        //          .Any(t => t.coach.Equals(c)))
        //          .Select(c => new { Key = c.Id, Value = c.firstName });

        //    TeamCoachesComboBox.ItemsSource = availableCoaches;
        //    CoachToEditComboBox.ItemsSource = coaches.Select(c => new { Key = c.Id, Value = c.firstName }).ToList();
        //    AvailableCoachesComboBox.ItemsSource = availableCoaches;
        //}

        //private void SetAvailableTeams()
        //{
        //    TeamsToEditComboBox.ItemsSource = teams.Select(t => new { Key = t.Id, Value = t.name }).ToList();
        //}
    }
}
