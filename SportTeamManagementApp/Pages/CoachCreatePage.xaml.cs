using SportTeamManagementApp.Enums;
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
                Coach newCoach = new Coach();

                if (String.IsNullOrEmpty(CoachFirstName.Text) || String.IsNullOrEmpty(CoachLastName.Text))
                {
                    throw new ArgumentException("First name or last name cannot be empty.");
                }
                if (!Int32.TryParse(CoachAge.Text, out int ageResult))
                {
                    throw new FormatException($"Could not parse value {CoachAge.Text} to a integer value");
                }
                if (!double.TryParse(CoachSalary.Text, out double salaryResult))
                {
                    throw new FormatException($"Could not parse value {CoachSalary.Text} to a integer value");
                }
                if (!Enum.TryParse(SoccerCoachRoleComboBox.SelectedItem?.ToString(), out SoccerCoachRole soccerCoachRole))
                {
                    throw new FormatException($"Could not parse value {SoccerCoachRoleComboBox.Text} to a SoccerPlayerRole");
                }

                newCoach.firstName = CoachFirstName.Text;
                newCoach.lastName = CoachLastName.Text;
                newCoach.age = ageResult;
                newCoach.salary = salaryResult;
                newCoach.role = (SoccerPlayerRole)soccerCoachRole;

                viewModel.Coaches.Add(newCoach);

                Frame.Navigate(typeof(MainPage));

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
