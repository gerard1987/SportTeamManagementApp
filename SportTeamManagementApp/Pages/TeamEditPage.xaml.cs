using SportTeamManagementApp.Data.Entities;
using SportTeamManagementApp.Data.Models;
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

            AvailablePlayersComboBox.ItemsSource = viewModel.dataProvider.GetAvailablePlayers().Select(p => new { Key = p.Id, Value = p.FirstName });
            AvailableCoachesComboBox.ItemsSource = viewModel.dataProvider.GetAvailableCoaches().Select(c => new { Key = c.Id, Value = c.FirstName });
            TeamsToEditComboBox.ItemsSource = viewModel.dataProvider.GetAvailableTeams().Select(t => new { Key = t.Id, Value = t.Name });
        }

        public async void SelectTeamToEdit(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TeamsToEditComboBox.SelectedValue != null)
                {
                    Int32.TryParse(TeamsToEditComboBox.SelectedValue.ToString(), out int teamId);
                    viewModel.TeamSelectedForEdit = viewModel.dataProvider.GetTeamComplete(teamId);

                    if (viewModel.TeamSelectedForEdit != null)
                    {
                        PlayersInTeamComboBox.ItemsSource = viewModel.TeamSelectedForEdit.Players.Select(p => new { Key = p.Id, Value = p.FirstName }).ToList();
                        EditTeamSelectSection.Visibility = Visibility.Collapsed;
                        EditTeamSection.Visibility = Visibility.Visible;
                        TeamNameEdit.Text = viewModel.TeamSelectedForEdit.Name;

                        AvailablePlayersComboBox.ItemsSource = viewModel.dataProvider.GetAvailablePlayers().Select(p => new { Key = p.Id, Value = p.FirstName });
                        AvailableCoachesComboBox.ItemsSource = viewModel.dataProvider.GetAvailableCoaches().Select(c => new { Key = c.Id, Value = c.FirstName });

                        if (viewModel.TeamSelectedForEdit.Coach != null)
                        {
                            List<object> items = new List<object>();
                            items.Add(new { Key = viewModel.TeamSelectedForEdit.Coach.Id, Value = viewModel.TeamSelectedForEdit.Coach.FirstName });
                            CoachesInTeamComboBox.ItemsSource = items;
                        }
                        else
                        {
                            CoachesInTeamComboBox.ItemsSource = null;
                        }

                    }
                }
                else
                {
                    throw new ArgumentException("No team selected");
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

                Team teamToUpdate = viewModel.dataProvider.Teams.Find(t => t.Id == viewModel.TeamSelectedForEdit.Id);
                teamToUpdate.Name = TeamNameEdit.Text;

                viewModel.dataProvider.EditTeam(teamToUpdate);
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
                if (AvailablePlayersComboBox.SelectedValue != null)
                {
                    Int32.TryParse(AvailablePlayersComboBox.SelectedValue.ToString(), out int playerId);
                    if (viewModel.TeamSelectedForEdit == null)
                    {
                        throw new ArgumentException("No team selected");
                    }

                    Player playerToAdd = viewModel.dataProvider.Players.Find(p => p.Id == playerId);
                    Team teamToEdit = viewModel.dataProvider.Teams.Find(t => t.Id == viewModel.TeamSelectedForEdit.Id);

                    teamToEdit.Players.Add(playerToAdd);
                    viewModel.dataProvider.EditTeam(teamToEdit);

                    PlayersInTeamComboBox.ItemsSource = teamToEdit.Players.Select(p => new { Key = p.Id, Value = p.FirstName }).ToList();
                    AvailablePlayersComboBox.ItemsSource = viewModel.dataProvider.GetAvailablePlayers().Select(p => new { Key = p.Id, Value = p.FirstName });
                }
                else
                {
                    throw new ArgumentException("No player selected!");
                }

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
                if (AvailableCoachesComboBox.SelectedValue != null)
                {
                    Int32.TryParse(AvailableCoachesComboBox.SelectedValue.ToString(), out int coachId);
                    if (viewModel.TeamSelectedForEdit == null)
                    {
                        throw new ArgumentException("No team selected");
                    }

                    Coach coachToAdd = viewModel.dataProvider.Coaches.Find(c => c.Id == coachId);
                    Team teamToEdit = viewModel.dataProvider.Teams.Find(t => t.Id == viewModel.TeamSelectedForEdit.Id);

                    teamToEdit.Coach = coachToAdd;
                    viewModel.dataProvider.EditTeam(teamToEdit);

                    List<object> items = new List<object>();
                    items.Add(new { Key = viewModel.TeamSelectedForEdit.Coach.Id, Value = viewModel.TeamSelectedForEdit.Coach.FirstName });
                    CoachesInTeamComboBox.ItemsSource = items;
                }
                else
                {
                    throw new ArgumentException("No coach selected!");
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

        public async void RemovePlayerFromTeam(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PlayersInTeamComboBox.SelectedValue != null)
                {
                    Int32.TryParse(PlayersInTeamComboBox.SelectedValue.ToString(), out int playerId);
                    if (viewModel.TeamSelectedForEdit == null)
                    {
                        throw new ArgumentException("No team selected");
                    }
                    Player teamPlayer = viewModel.TeamSelectedForEdit.Players.Find(p => p.Id == playerId);
                    Team teamToEdit = viewModel.dataProvider.Teams.Find(t => t.Id == viewModel.TeamSelectedForEdit.Id);

                    teamToEdit.Players.Remove(teamPlayer);
                    viewModel.dataProvider.EditTeam(teamToEdit);

                    PlayersInTeamComboBox.ItemsSource = teamToEdit.Players.Select(p => new { Key = p.Id, Value = p.FirstName }).ToList();
                    AvailablePlayersComboBox.ItemsSource = viewModel.dataProvider.GetAvailablePlayers().Select(p => new { Key = p.Id, Value = p.FirstName });
                }
                else
                {
                    throw new ArgumentException("No player selected");
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

        public async void RemoveCoachFromTeam(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CoachesInTeamComboBox.SelectedValue != null)
                {
                    Int32.TryParse(CoachesInTeamComboBox.SelectedValue.ToString(), out int coachId);
                    if (viewModel.TeamSelectedForEdit == null)
                    {
                        throw new ArgumentException("No team selected");
                    }
                    Coach teamCoach = viewModel.TeamSelectedForEdit.Coach;
                    Team teamToEdit = viewModel.dataProvider.Teams.Find(t => t.Id == viewModel.TeamSelectedForEdit.Id);

                    teamToEdit.Coach = null;
                    viewModel.dataProvider.EditTeam(teamToEdit);

                    CoachesInTeamComboBox.ItemsSource = null;

                    AvailableCoachesComboBox.ItemsSource = viewModel.dataProvider.GetAvailableCoaches().Select(c => new { Key = c.Id, Value = c.FirstName });
                }
                else
                {
                    throw new ArgumentException("No coach selected!");
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

        private async void RemoveTeam(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TeamsToEditComboBox.SelectedValue != null)
                {
                    Int32.TryParse(TeamsToEditComboBox.SelectedValue.ToString(), out int teamId);
                    Team teamToRemove = viewModel.dataProvider.Teams.FirstOrDefault(t => t.Id == teamId);
                    if (teamToRemove != null)
                    {
                        viewModel.dataProvider.RemoveTeam(teamToRemove);
                    }

                    Frame.Navigate(typeof(MainPage));
                }
                else
                {
                    throw new ArgumentException("No team selected");
                }
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
