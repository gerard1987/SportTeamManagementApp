using SportTeamManagementApp.Data;
using SportTeamManagementApp.Data.Entities;
using SportTeamManagementApp.Data.Enums;
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
                var newPlayer = new Player
                {
                    FirstName = PlayerFirstName.Text,
                    LastName = PlayerLastName.Text,
                    Age = PlayerAge.Text,
                    Salary = PlayerSalary.Text,
                    Role = SoccerPlayerRoleComboBox.SelectedItem?.ToString()
                };

                viewModel.dataProvider.CreatePlayer(newPlayer);

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
