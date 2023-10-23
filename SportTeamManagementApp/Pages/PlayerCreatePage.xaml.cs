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
    public sealed partial class PlayerCreatePage : Page
    {
        ViewModel viewModel;
        public PlayerCreatePage()
        {
            this.InitializeComponent();

            viewModel = App.SharedViewModel;

            SoccerPlayerRoleComboBox.ItemsSource = Enum.GetValues(typeof(SoccerPlayerRole));
        }

        private async void CreatePlayer(object sender, RoutedEventArgs e)
        {
            try
            {
                Player newPlayer = new Player();

                if (String.IsNullOrEmpty(PlayerFirstName.Text) || String.IsNullOrEmpty(PlayerLastName.Text))
                {
                    throw new ArgumentException("First name or last name cannot be empty.");
                }
                if (!Int32.TryParse(PlayerAge.Text, out int ageResult))
                {
                    throw new FormatException($"Could not parse value {PlayerAge.Text} to a integer value");
                }
                if (!double.TryParse(PlayerSalary.Text, out double salaryResult))
                {
                    throw new FormatException($"Could not parse value {PlayerSalary.Text} to a integer value");
                }
                if (!Enum.TryParse(SoccerPlayerRoleComboBox.SelectedItem?.ToString(), out SoccerPlayerRole soccerPlayerRole))
                {
                    throw new FormatException($"Could not parse value {SoccerPlayerRoleComboBox.Text} to a SoccerPlayerRole");
                }

                newPlayer.firstName = PlayerFirstName.Text;
                newPlayer.lastName = PlayerLastName.Text;
                newPlayer.age = ageResult;
                newPlayer.salary = salaryResult;
                newPlayer.role = soccerPlayerRole;

                viewModel.Players.Add(newPlayer);

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
