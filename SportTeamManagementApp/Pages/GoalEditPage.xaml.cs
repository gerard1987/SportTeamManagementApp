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
    public sealed partial class GoalEditPage : Page
    {
        ViewModel viewModel;
        public GoalEditPage()
        {
            this.InitializeComponent();

            viewModel = App.SharedViewModel;

            GoalsToEditComboBox.ItemsSource = viewModel.dataProvider.Goals.Select(goal => new
            {
                Key = goal.Id,
                Value = $"Goal scored by {goal.Player.firstName} for {goal.GoalScoredForTeam.Name} against {goal.GoalScoredAgainstTeam.Name}",
            })
            .ToList();
        }


        public async void SelectGoalToEdit(object sender, RoutedEventArgs e)
        {
            try
            {
                if (GoalsToEditComboBox.SelectedValue != null)
                {
                    Int32.TryParse(GoalsToEditComboBox.SelectedValue.ToString(), out int goalId);
                    viewModel.GoalSelectedForEdit = viewModel.dataProvider.Goals.FirstOrDefault(g => g.Id.Equals(goalId));

                    if (viewModel.GoalSelectedForEdit != null)
                    {
                        PlayerComboBox.ItemsSource = viewModel.dataProvider.Players.Select(p => new { Key = p.Id, Value = p.firstName }).ToList();
                        MatchScoredIn.ItemsSource = viewModel.dataProvider.Matches.Select(m => new { Key = m.Id, Value = $"{m.HomeTeam.Name} vs {m.AwayTeam.Name}" }).ToList();

                        EditGoalSelectSection.Visibility = Visibility.Collapsed;
                        AsignGoalSection.Visibility = Visibility.Visible;
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


        public async void AssignGoal(object sender, RoutedEventArgs e)
        {
            try
            {
                Goal goalToUpdate = viewModel.dataProvider.Goals.FirstOrDefault(g => g.Id == viewModel.GoalSelectedForEdit.Id);

                Int32.TryParse(MatchScoredIn.SelectedValue.ToString(), out int matchTeamId);
                Match match = viewModel.dataProvider.Matches.FirstOrDefault(m => m.Id.Equals(matchTeamId));

                if (!goalToUpdate.MatchId.Equals(match.Id))
                {
                    match.Goals.Add(goalToUpdate);
                }

                await viewModel.dataProvider.EditMatchAsync(match);

                TeamScoredFor.ItemsSource = viewModel.dataProvider.Teams.Where(team => team.Id.Equals(match.HomeTeamId) || team.Id.Equals(match.AwayTeamId)).Select(t => new { Key = t.Id, Value = t.Name }).ToList();
                TeamScoredAgainst.ItemsSource = viewModel.dataProvider.Teams.Where(team => team.Id.Equals(match.HomeTeamId) || team.Id.Equals(match.AwayTeamId)).Select(t => new { Key = t.Id, Value = t.Name }).ToList();

                AsignGoalSection.Visibility = Visibility.Collapsed;
                EditGoalSection.Visibility = Visibility.Visible;
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

        public async void EditGoal(object sender, RoutedEventArgs e)
        {
            try
            {
                Goal goalToUpdate = viewModel.dataProvider.Goals.FirstOrDefault(g => g.Id == viewModel.GoalSelectedForEdit.Id);

                Int32.TryParse(TeamScoredFor.SelectedValue.ToString(), out int homeTeamId);
                Team teamScoredFor = viewModel.dataProvider.Teams.FirstOrDefault(t => t.Id.Equals(homeTeamId));

                Int32.TryParse(TeamScoredAgainst.SelectedValue.ToString(), out int awayTeamId);
                Team teamScoredAgainst = viewModel.dataProvider.Teams.FirstOrDefault(t => t.Id.Equals(awayTeamId));

                Int32.TryParse(PlayerComboBox.SelectedValue.ToString(), out int playerId);
                Player player = viewModel.dataProvider.Players.FirstOrDefault(p => p.Id.Equals(playerId));

                if (goalToUpdate != null)
                {
                    goalToUpdate.GoalScoredForTeam = teamScoredFor;
                    goalToUpdate.GoalScoredForTeamId = teamScoredFor.Id;
                    goalToUpdate.GoalScoredAgainstTeam = teamScoredAgainst;
                    goalToUpdate.GoalScoredAgainstTeamId = teamScoredAgainst.Id;
                    goalToUpdate.Player = player;
                    goalToUpdate.PlayerId = player.Id;
                }

                // Retrieve the associated match for the goal
                Match match = viewModel.dataProvider.Matches.FirstOrDefault(m => m.Id == goalToUpdate.MatchId);

                // Update the goal within the match's list of goals
                Goal goalInMatch = match.Goals.FirstOrDefault(g => g.Id == goalToUpdate.Id);
                if (goalInMatch != null)
                {
                    goalInMatch.GoalScoredForTeam = goalToUpdate.GoalScoredForTeam;
                    goalInMatch.GoalScoredForTeamId = goalToUpdate.GoalScoredForTeamId;
                    goalInMatch.GoalScoredAgainstTeam = goalToUpdate.GoalScoredAgainstTeam;
                    goalInMatch.GoalScoredAgainstTeamId = goalToUpdate.GoalScoredAgainstTeamId;
                    goalInMatch.Player = goalToUpdate.Player;
                    goalInMatch.PlayerId = goalToUpdate.PlayerId;
                }

                await viewModel.dataProvider.EditMatchAsync(match);

                Frame.Navigate(typeof(MainPage));
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
