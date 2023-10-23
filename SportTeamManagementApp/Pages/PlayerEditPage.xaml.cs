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
    public sealed partial class PlayerEditPage : Page
    {
        ViewModel viewModel;
        public PlayerEditPage()
        {
            this.InitializeComponent();

            viewModel = App.SharedViewModel;

            PlayerToEditComboBox.ItemsSource = viewModel.Players.Select(p => new { Key = p.Id, Value = p.firstName });
        }

        public async void SelectPlayerToEdit(object sender, RoutedEventArgs e)
        {
            try
            {
                Int32.TryParse(PlayerToEditComboBox.SelectedValue.ToString(), out int playerId);
                viewModel.PlayerSelectedForEdit = viewModel.Players.Find(p => p.Id == playerId);

                if (viewModel.PlayerSelectedForEdit != null)
                {
                    EditPlayerSelectSection.Visibility = Visibility.Collapsed;
                    EditPlayerSection.Visibility = Visibility.Visible;

                    Array roles = Enum.GetValues(typeof(SoccerPlayerRole));
                    int selectedRole = 0;
                    for (int i = 0; i < roles.Length; i++)
                    {
                        SoccerPlayerRole role = (SoccerPlayerRole)roles.GetValue(i);
                        if (viewModel.PlayerSelectedForEdit.role.Equals(role))
                        {
                            selectedRole = i;
                        }
                    }

                    Enum.TryParse(viewModel.PlayerSelectedForEdit.role.ToString(), out SoccerPlayerRole parsedRole);

                    PlayerEditFirstName.Text = viewModel.PlayerSelectedForEdit.firstName;
                    PlayerEditLastName.Text = viewModel.PlayerSelectedForEdit.lastName;
                    PlayerEditAge.Text = viewModel.PlayerSelectedForEdit.age.ToString();
                    PlayerEditSalary.Text = viewModel.PlayerSelectedForEdit.salary.ToString();
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
                if (String.IsNullOrEmpty(PlayerEditFirstName.Text) || String.IsNullOrEmpty(PlayerEditLastName.Text))
                {
                    throw new ArgumentException("First name or last name cannot be empty.");
                }
                if (!Int32.TryParse(PlayerEditAge.Text, out int ageResult))
                {
                    throw new FormatException($"Could not parse value {PlayerEditAge.Text} to a integer value");
                }
                if (!double.TryParse(PlayerEditSalary.Text, out double salaryResult))
                {
                    throw new FormatException($"Could not parse value {PlayerEditSalary.Text} to a integer value");
                }
                if (!Enum.TryParse(SoccerPlayerRoleEditComboBox.SelectedItem?.ToString(), out SoccerPlayerRole soccerPlayerRole))
                {
                    throw new FormatException($"Could not parse value {SoccerPlayerRoleEditComboBox.Text} to a SoccerPlayerRole");
                }

                Player playerToUpdate = viewModel.Players.FirstOrDefault(p => p.Id == viewModel.PlayerSelectedForEdit.Id);

                if (playerToUpdate != null)
                {
                    playerToUpdate.firstName = PlayerEditFirstName.Text;
                    playerToUpdate.lastName = PlayerEditLastName.Text;
                    playerToUpdate.age = ageResult;
                    playerToUpdate.salary = salaryResult;
                    playerToUpdate.role = soccerPlayerRole;
                }

                EditPlayerSection.Visibility = Visibility.Collapsed;
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
                Player playerToRemove = viewModel.Players.FirstOrDefault(p => p.Id == viewModel.PlayerSelectedForEdit.Id);
                if (playerToRemove != null)
                {
                    viewModel.Players.Remove(playerToRemove);
                }

                foreach (Team team in viewModel.Teams)
                {
                    Player player = team.players.Find(p => p.Id == viewModel.PlayerSelectedForEdit.Id);

                    team.RemovePlayer(player);
                }

                EditPlayerSection.Visibility = Visibility.Collapsed;
                //SetAvailablePlayers();
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
