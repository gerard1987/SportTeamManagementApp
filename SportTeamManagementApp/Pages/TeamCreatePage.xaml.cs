using SportTeamManagementApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SportTeamManagementApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TeamCreatePage : Page
    {
        ViewModel viewModel;
        public TeamCreatePage()
        {
            this.InitializeComponent();

            viewModel = App.SharedViewModel;
        }

        public async void AddPlayerToSelectionForTeam(object sender, RoutedEventArgs e)
        {
            try
            {
                Int32.TryParse(TeamPlayersComboBox.SelectedValue.ToString(), out int playerId);
                Player playerToAdd = viewModel.Players.Find(p => p.Id == playerId);

                bool exists = viewModel.SelectedPlayersForTeam.Exists(sp => sp.Id == playerId);
                if (exists)
                {
                    throw new ArgumentException($"Player already in team!");
                }
                viewModel.SelectedPlayersForTeam.Add(playerToAdd);
                SelectedPlayersListBox.ItemsSource = viewModel.SelectedPlayersForTeam.Select(c => new { Key = c.Id, Value = c.firstName }).ToList();
            }
            catch (ArgumentException aEx)
            {
                await ShowExceptionMessage(aEx.Message);
            }
            catch (FormatException fEx)
            {
                await ShowExceptionMessage(fEx.Message);
            }
            catch (Exception ex)
            {
                await ShowExceptionMessage($"Something went wrong {ex.Message} ");
            }
        }

        private async void CreateTeam(object sender, RoutedEventArgs e)
        {
            try
            {
                if (viewModel.Coaches.Count < 1 || viewModel.Players.Count < 1)
                {
                    throw new ArgumentException("Cannot create a team without having created a coach or players first!");
                }
                if (String.IsNullOrEmpty(TeamName.Text))
                {
                    throw new ArgumentException("Team name cannot be empty!");
                }
                if (viewModel.SelectedPlayersForTeam.Count < 1)
                {
                    throw new ArgumentException("Please select at least 1 player for the team");
                }

                Int32.TryParse(TeamCoachesComboBox.SelectedValue.ToString(), out int coachId);
                Coach coach = viewModel.Coaches.Find(c => c.Id == coachId);

                List<Player> playersToAdd = viewModel.SelectedPlayersForTeam;
                Team newTeam = new Team(TeamName.Text, coach, playersToAdd);

                viewModel.Teams.Add(newTeam);

                viewModel.SelectedPlayersForTeam = new List<Player>();
                SelectedPlayersListBox.ItemsSource = null;

                Frame.Navigate(typeof(MainPage));
            }
            catch (ArgumentOutOfRangeException aEx)
            {
                await ShowExceptionMessage(aEx.Message);
            }
            catch (ArgumentException aEx)
            {
                await ShowExceptionMessage(aEx.Message);
            }
            catch (Exception ex)
            {
                await ShowExceptionMessage($"Something went wrong {ex.Message} ");
            }
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async Task ShowExceptionMessage(string errorMessage)
        {
            ErrorDialog.Content = errorMessage;
            await ErrorDialog.ShowAsync();
        }

    }
}
