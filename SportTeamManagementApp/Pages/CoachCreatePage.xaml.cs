using SportTeamManagementApp.Data;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SportTeamManagementApp.Data.Enums;
using SportTeamManagementApp.Data.Entities;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SportTeamManagementApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CoachCreatePage : Page
    {
        ViewModel viewModel;
        public CoachCreatePage()
        {
            this.InitializeComponent();

            viewModel = App.SharedViewModel;

            SoccerCoachRoleComboBox.ItemsSource = Enum.GetValues(typeof(SoccerCoachRole));
        }

        private async void CreateCoach(object sender, RoutedEventArgs e)
        {
            try
            {
                var newCoach = new Coach()
                {
                    FirstName = CoachFirstName.Text,
                    LastName = CoachLastName.Text,
                    Age = CoachAge.Text,
                    Salary = CoachSalary.Text,
                    Role = SoccerCoachRoleComboBox.SelectedItem?.ToString()
                };

                viewModel.dataProvider.CreateCoach(newCoach);

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
