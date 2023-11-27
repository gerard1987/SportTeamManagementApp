using SportTeamManagementApp.Data.Entities;
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
    public sealed partial class GoalCreatePage : Page
    {
        public ViewModel viewModel;
        public GoalCreatePage()
        {
            this.InitializeComponent();

            viewModel = App.SharedViewModel;

            SelectMatchComboBox.ItemsSource = viewModel.dataProvider.Matches.Select(m => new { Key = m.Id, Value = ($"{m.HomeTeam.Name} vs {m.AwayTeam.Name}") });
        }

        private async void SelectMatch(object sender, RoutedEventArgs e)
        {
            try
            {
                Int32.TryParse(SelectMatchComboBox.SelectedValue.ToString(), out int matchId);
                viewModel.MatchSelectedForEdit = viewModel.dataProvider.Matches.Find(m => m.Id.Equals(matchId));

                List<Team> teamsInMatch = new List<Team>();
                teamsInMatch.Add(viewModel.MatchSelectedForEdit.HomeTeam);
                teamsInMatch.Add(viewModel.MatchSelectedForEdit.AwayTeam);

                TeamScoredForComboBox.ItemsSource = teamsInMatch.Select(t => new { Key = t.Id, Value = t.Name });
                TeamScoredAgainstComboBox.ItemsSource = teamsInMatch.Select(t => new { Key = t.Id, Value = t.Name });

                List<int> teamIdsInMatch = teamsInMatch.Select(team => team.Id).ToList();

                var matchPlayers = viewModel.dataProvider.Teams
                .Where(team => teamIdsInMatch.Contains(team.Id))
                .SelectMany(team => team.Players)
                .ToList();

                PlayerSelectComboBox.ItemsSource = matchPlayers.Select(t => new { Key = t.Id, Value = t.FirstName });

                CreateGoalSection.Visibility = Visibility.Visible;
                SelectMatchSection.Visibility = Visibility.Collapsed;
            }
            catch (InvalidOperationException ioEx)
            {
                await ShowExceptionMessage(ioEx.Message);
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

        private async void CreateGoal(object sender, RoutedEventArgs e)
        {
            try
            {
                Int32.TryParse(PlayerSelectComboBox.SelectedValue.ToString(), out int playerId);
                Int32.TryParse(TeamScoredForComboBox.SelectedValue.ToString(), out int TeamScoredForId);
                Int32.TryParse(TeamScoredAgainstComboBox.SelectedValue.ToString(), out int TeamScoredAgainstId);

                Player playerThatScored = viewModel.dataProvider.Players.Find(p => p.Id.Equals(playerId));
                Team TeamScoredFor = viewModel.dataProvider.Teams.Find(p => p.Id.Equals(TeamScoredForId));
                Team TeamScoredAgainst = viewModel.dataProvider.Teams.Find(p => p.Id.Equals(TeamScoredAgainstId));



                var newGoal = new Goal()
                {
                    Match = viewModel.MatchSelectedForEdit,
                    MatchId = viewModel.MatchSelectedForEdit.Id,
                    Player = playerThatScored,
                    PlayerId = playerThatScored.Id,
                    GoalScoredForTeam = TeamScoredFor,
                    GoalScoredForTeamId = TeamScoredFor.Id,
                    GoalScoredAgainstTeam = TeamScoredAgainst,
                    GoalScoredAgainstTeamId = TeamScoredAgainst.Id
                };

                viewModel.MatchSelectedForEdit.Goals.Add(newGoal);
                await viewModel.dataProvider.EditMatchAsync(viewModel.MatchSelectedForEdit);

                viewModel.MatchSelectedForEdit = new Match();

                Frame.Navigate(typeof(MainPage));
            }
            catch (InvalidOperationException ioEx)
            {
                await ShowExceptionMessage(ioEx.Message);
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
