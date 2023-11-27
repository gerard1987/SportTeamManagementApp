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
    public sealed partial class MatchEditPage : Page
    {
        ViewModel viewModel;
        public MatchEditPage()
        {
            this.InitializeComponent();

            viewModel = App.SharedViewModel;

            MatchesToEditComboBox.ItemsSource = viewModel.dataProvider.Matches.Select(match => new
            {
                Key = match.Id,
                Value = $"{match.HomeTeam.Name} vs {match.AwayTeam.Name}",
            })
            .ToList();
        }

        public async void SelectMatchToEdit(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MatchesToEditComboBox.SelectedValue != null)
                {
                    Int32.TryParse(MatchesToEditComboBox.SelectedValue.ToString(), out int matchId);
                    viewModel.MatchSelectedForEdit = viewModel.dataProvider.Matches.FirstOrDefault(m => m.Id.Equals(matchId));

                    if (viewModel.MatchSelectedForEdit != null)
                    {
                        EditMatchSelectSection.Visibility = Visibility.Collapsed;

                        HomeTeamsComboBox.ItemsSource = viewModel.dataProvider.Teams.Select(t => new { Key = t.Id, Value = t.Name }).ToList();
                        AwayTeamsComboBox.ItemsSource = viewModel.dataProvider.Teams.Select(t => new { Key = t.Id, Value = t.Name }).ToList();

                        EditMatchSection.Visibility = Visibility.Visible;
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

        public async void EditMatch(object sender, RoutedEventArgs e)
        {
            try
            {
                Match matchToUpdate = viewModel.dataProvider.Matches.FirstOrDefault(m => m.Id == viewModel.MatchSelectedForEdit.Id);

                Int32.TryParse(HomeTeamsComboBox.SelectedValue.ToString(), out int homeTeamId);
                Team homeTeam = viewModel.dataProvider.Teams.FirstOrDefault(t => t.Id.Equals(homeTeamId));

                Int32.TryParse(AwayTeamsComboBox.SelectedValue.ToString(), out int awayTeamId);
                Team awayTeam = viewModel.dataProvider.Teams.FirstOrDefault(t => t.Id.Equals(awayTeamId));

                if (matchToUpdate != null)
                {
                    matchToUpdate.HomeTeam = homeTeam;
                    matchToUpdate.HomeTeamId = homeTeam.Id;
                    matchToUpdate.AwayTeam = awayTeam;
                    matchToUpdate.AwayTeamId = awayTeam.Id;
                }

                await viewModel.dataProvider.EditMatchAsync(matchToUpdate);

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
        private async void RemoveMatch(object sender, RoutedEventArgs e)
        {
            try
            {
                Match matchToRemove = viewModel.dataProvider.Matches.FirstOrDefault(m => m.Id == viewModel.MatchSelectedForEdit.Id);
                if (matchToRemove != null)
                {
                    viewModel.dataProvider.RemoveMatch(matchToRemove);
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
