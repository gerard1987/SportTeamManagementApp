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
    public sealed partial class TeamEditPage : Page
    {
        ViewModel viewModel;
        public TeamEditPage()
        {
            this.InitializeComponent();

            viewModel = App.SharedViewModel;

            AvailablePlayersComboBox.ItemsSource = viewModel.GetAvailablePlayers().Select(p => new { Key = p.Id, Value = p.FirstName });
            AvailableCoachesComboBox.ItemsSource = viewModel.GetAvailableCoaches().Select(c => new { Key = c.Id, Value = c.FirstName });
            TeamsToEditComboBox.ItemsSource = viewModel.GetAvailableTeams().Select(t => new { Key = t.Id, Value = t.name });
        }

        public async void SelectTeamToEdit(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(TeamsToEditComboBox.SelectedValue.ToString()))
                {
                    throw new ArgumentException("No team selected");
                }

                Int32.TryParse(TeamsToEditComboBox.SelectedValue.ToString(), out int teamId);
                viewModel.TeamSelectedForEdit = viewModel.Teams.Find(t => t.Id == teamId);

                if (viewModel.TeamSelectedForEdit != null)
                {
                    PlayersInTeamComboBox.ItemsSource = viewModel.TeamSelectedForEdit.players.Select(p => new { Key = p.Id, Value = p.FirstName }).ToList();
                    EditTeamSelectSection.Visibility = Visibility.Collapsed;
                    EditTeamSection.Visibility = Visibility.Visible;
                    TeamNameEdit.Text = viewModel.TeamSelectedForEdit.name;

                    AvailablePlayersComboBox.ItemsSource = viewModel.GetAvailablePlayers().Select(p => new { Key = p.Id, Value = p.FirstName });

                    if (viewModel.TeamSelectedForEdit.coach != null)
                    {
                        List<object> items = new List<object>();
                        items.Add(new { Key = viewModel.TeamSelectedForEdit.coach.Id, Value = viewModel.TeamSelectedForEdit.coach.FirstName });
                        CoachesInTeamComboBox.ItemsSource = items;
                    }
                    else
                    {
                        CoachesInTeamComboBox.ItemsSource = null;
                    }

                }
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

        public async void EditTeam(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(TeamNameEdit.Text))
                {
                    throw new ArgumentException("Team name can't be empty");
                }

                Team teamToUpdate = viewModel.Teams.Find(t => t.Id == viewModel.TeamSelectedForEdit.Id);
                teamToUpdate.name = TeamNameEdit.Text;
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

        public async void AddPlayerToTeam(object sender, RoutedEventArgs e)
        {
            try
            {
                Int32.TryParse(AvailablePlayersComboBox.SelectedValue.ToString(), out int playerId);
                if (viewModel.TeamSelectedForEdit == null)
                {
                    throw new ArgumentException("No team selected");
                }

                Player playerToAdd = viewModel.Players.Find(p => p.Id == playerId);
                Team teamToEdit = viewModel.Teams.Find(t => t.Id == viewModel.TeamSelectedForEdit.Id);

                teamToEdit.AddPlayer(playerToAdd);

                PlayersInTeamComboBox.ItemsSource = teamToEdit.players.Select(p => new { Key = p.Id, Value = p.FirstName }).ToList();
                AvailablePlayersComboBox.ItemsSource = viewModel.GetAvailablePlayers().Select(p => new { Key = p.Id, Value = p.FirstName });
            }
            catch (ArgumentException aEx)
            {
                await ShowExceptionMessage(aEx.Message);
            }
            catch (InvalidOperationException ioEx)
            {
                await ShowExceptionMessage(ioEx.Message);
            }
            catch (Exception ex)
            {
                await ShowExceptionMessage($"Something went wrong {ex.Message} ");
            }
        }

        public async void AddCoachToTeam(object sender, RoutedEventArgs e)
        {
            try
            {
                Int32.TryParse(AvailableCoachesComboBox.SelectedValue.ToString(), out int coachId);
                if (viewModel.TeamSelectedForEdit == null)
                {
                    throw new ArgumentException("No team selected");
                }

                Coach coachToAdd = viewModel.Coaches.Find(c => c.Id == coachId);
                Team teamToEdit = viewModel.Teams.Find(t => t.Id == viewModel.TeamSelectedForEdit.Id);

                teamToEdit.coach = coachToAdd;

                List<object> items = new List<object>();
                items.Add(new { Key = viewModel.TeamSelectedForEdit.coach.Id, Value = viewModel.TeamSelectedForEdit.coach.FirstName });
                CoachesInTeamComboBox.ItemsSource = items;
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

        public async void RemovePlayerFromTeam(object sender, RoutedEventArgs e)
        {
            try
            {
                Int32.TryParse(PlayersInTeamComboBox.SelectedValue.ToString(), out int playerId);
                if (viewModel.TeamSelectedForEdit == null)
                {
                    throw new ArgumentException("No team selected");
                }
                Player teamPlayer = viewModel.TeamSelectedForEdit.players.Find(p => p.Id == playerId);
                Team teamToEdit = viewModel.Teams.Find(t => t.Id == viewModel.TeamSelectedForEdit.Id);

                teamToEdit.RemovePlayer(teamPlayer);
                PlayersInTeamComboBox.ItemsSource = teamToEdit.players.Select(p => new { Key = p.Id, Value = p.FirstName }).ToList();

                AvailablePlayersComboBox.ItemsSource = viewModel.GetAvailablePlayers().Select(p => new { Key = p.Id, Value = p.FirstName });
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

        public async void RemoveCoachFromTeam(object sender, RoutedEventArgs e)
        {
            try
            {
                Int32.TryParse(CoachesInTeamComboBox.SelectedValue.ToString(), out int coachId);
                if (viewModel.TeamSelectedForEdit == null)
                {
                    throw new ArgumentException("No team selected");
                }
                Coach teamCoach = viewModel.TeamSelectedForEdit.coach;
                Team teamToEdit = viewModel.Teams.Find(t => t.Id == viewModel.TeamSelectedForEdit.Id);

                teamToEdit.coach = null;
                CoachesInTeamComboBox.ItemsSource = null;

                AvailableCoachesComboBox.ItemsSource = viewModel.GetAvailableCoaches().Select(c => new { Key = c.Id, Value = c.FirstName });
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

        private async void RemoveTeam(object sender, RoutedEventArgs e)
        {
            try
            {
                Int32.TryParse(TeamsToEditComboBox.SelectedValue.ToString(), out int teamId);
                Team teamToRemove = viewModel.Teams.FirstOrDefault(t => t.Id == teamId);
                if (teamToRemove != null)
                {
                    viewModel.Teams.Remove(teamToRemove);
                }

                Frame.Navigate(typeof(MainPage));
            }
            catch (InvalidOperationException ioEx)
            {
                await ShowExceptionMessage(ioEx.Message);
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
