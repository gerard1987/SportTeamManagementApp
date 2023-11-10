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
    public sealed partial class PlayerEditPage : Page
    {
        ViewModel viewModel;
        public PlayerEditPage()
        {
            this.InitializeComponent();

            viewModel = App.SharedViewModel;

            PlayerToEditComboBox.ItemsSource = viewModel.dataProvider.Players.Select(p => new { Key = p.Id, Value = p.FirstName });
        }

        public async void SelectPlayerToEdit(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PlayerToEditComboBox.SelectedValue == null)
                {
                    throw new ArgumentException("No player selected!");
                }

                Int32.TryParse(PlayerToEditComboBox.SelectedValue.ToString(), out int playerId);
                viewModel.PlayerSelectedForEdit = viewModel.dataProvider.Players.Find(p => p.Id == playerId);

                if (viewModel.PlayerSelectedForEdit != null)
                {
                    EditPlayerSelectSection.Visibility = Visibility.Collapsed;
                    EditPlayerSection.Visibility = Visibility.Visible;

                    Array roles = Enum.GetValues(typeof(SoccerPlayerRole));
                    int selectedRole = 0;
                    for (int i = 0; i < roles.Length; i++)
                    {
                        SoccerPlayerRole role = (SoccerPlayerRole)roles.GetValue(i);
                        if (viewModel.PlayerSelectedForEdit.Role.Equals(role))
                        {
                            selectedRole = i;
                        }
                    }

                    Enum.TryParse(viewModel.PlayerSelectedForEdit.Role.ToString(), out SoccerPlayerRole parsedRole);

                    PlayerEditFirstName.Text = viewModel.PlayerSelectedForEdit.FirstName;
                    PlayerEditLastName.Text = viewModel.PlayerSelectedForEdit.LastName;
                    PlayerEditAge.Text = viewModel.PlayerSelectedForEdit.Age.ToString();
                    PlayerEditSalary.Text = viewModel.PlayerSelectedForEdit.Salary.ToString();
                    SoccerPlayerRoleEditComboBox.ItemsSource = roles;
                    SoccerPlayerRoleEditComboBox.SelectedIndex = selectedRole;
                    EditPlayerSelectSection.Visibility = Visibility.Collapsed;
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

        private async void EditPlayer(object sender, RoutedEventArgs e)
        {
            try
            {
                Player playerToUpdate = viewModel.dataProvider.Players.FirstOrDefault(p => p.Id == viewModel.PlayerSelectedForEdit.Id);

                if (playerToUpdate != null)
                {
                    playerToUpdate.FirstName = PlayerEditFirstName.Text;
                    playerToUpdate.LastName = PlayerEditLastName.Text;
                    playerToUpdate.Age = PlayerEditAge.Text;
                    playerToUpdate.Salary = PlayerEditSalary.Text;
                    playerToUpdate.Role = SoccerPlayerRoleEditComboBox.SelectedItem?.ToString();
                }

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

        private async void RemovePlayer(object sender, RoutedEventArgs e)
        {
            try
            {
                Player playerToRemove = viewModel.dataProvider.Players.FirstOrDefault(p => p.Id == viewModel.PlayerSelectedForEdit.Id);
                if (playerToRemove != null)
                {
                    viewModel.dataProvider.Players.Remove(playerToRemove);
                }

                foreach (Team team in viewModel.dataProvider.Teams)
                {
                    Player player = team.Players.Find(p => p.Id == viewModel.PlayerSelectedForEdit.Id);

                    if (player != null)
                    {
                        team.Players.Remove(player);
                        viewModel.dataProvider.EditTeam(team);
                    }
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
