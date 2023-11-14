using SportTeamManagementApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SportTeamManagementApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateGoalPage : Page
    {
        public ViewModel viewModel;
        public CreateGoalPage()
        {
            this.InitializeComponent();

            viewModel = App.SharedViewModel;

            HomeTeamComboBox.ItemsSource = viewModel.dataProvider.GetAvailableTeams().Select(p => new { Key = p.Id, Value = p.Name });
            AwayTeamComboBox.ItemsSource = viewModel.dataProvider.GetAvailableTeams().Select(p => new { Key = p.Id, Value = p.Name });
        }

        private async void CreateMatch(object sender, RoutedEventArgs e)
        {
            try
            {
                Int32.TryParse(HomeTeamComboBox.SelectedValue.ToString(), out int homeTeamId);
                Int32.TryParse(AwayTeamComboBox.SelectedValue.ToString(), out int awayTeamId);

                if (awayTeamId.Equals(homeTeamId))
                {
                    throw new ArgumentException("Can't have a match with itself!");
                }

                Team awayTeam = viewModel.dataProvider.Teams.Find(t => t.Id.Equals(awayTeamId));
                Team homeTeam = viewModel.dataProvider.Teams.Find(t => t.Id.Equals(homeTeamId));

                Match newMatch = new Match()
                {
                    HomeTeam = homeTeam,
                    HomeTeamId = homeTeam.Id,
                    AwayTeam = awayTeam,
                    AwayTeamId = awayTeam.Id
                };

                viewModel.dataProvider.CreateMatch(newMatch);

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
