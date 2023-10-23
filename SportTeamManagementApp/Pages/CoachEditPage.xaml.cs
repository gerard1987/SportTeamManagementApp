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
    public sealed partial class CoachEditPage : Page
    {
        ViewModel viewModel;
        public CoachEditPage()
        {
            this.InitializeComponent();

            viewModel = App.SharedViewModel;

            CoachToEditComboBox.ItemsSource = viewModel.Coaches.Select(c => new { Key = c.Id, Value = c.firstName });
        }

        public async void SelectCoachToEdit(object sender, RoutedEventArgs e)
        {
            try
            {
                Int32.TryParse(CoachToEditComboBox.SelectedValue.ToString(), out int coachId);
                viewModel.CoachSelectedForEdit = viewModel.Coaches.Find(p => p.Id == coachId);

                if (viewModel.CoachSelectedForEdit != null)
                {
                    EditCoachSelectSection.Visibility = Visibility.Collapsed;
                    EditCoachSection.Visibility = Visibility.Visible;

                    Array roles = Enum.GetValues(typeof(SoccerCoachRole));
                    int selectedRole = 0;
                    for (int i = 0; i < roles.Length; i++)
                    {
                        SoccerCoachRole role = (SoccerCoachRole)roles.GetValue(i);
                        if (viewModel.CoachSelectedForEdit.role.Equals(role))
                        {
                            selectedRole = i;
                        }
                    }

                    CoachEditFirstName.Text = viewModel.CoachSelectedForEdit.firstName;
                    CoachEditLastName.Text = viewModel.CoachSelectedForEdit.lastName;
                    CoachEditAge.Text = viewModel.CoachSelectedForEdit.age.ToString();
                    CoachEditSalary.Text = viewModel.CoachSelectedForEdit.salary.ToString();
                    SoccerCoachRoleEditComboBox.ItemsSource = roles;
                    SoccerCoachRoleEditComboBox.SelectedIndex = selectedRole;
                    EditCoachSelectSection.Visibility = Visibility.Collapsed;
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

        private async void EditCoach(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(CoachEditFirstName.Text) || String.IsNullOrEmpty(CoachEditLastName.Text))
                {
                    throw new ArgumentException("First name or last name cannot be empty.");
                }
                if (!Int32.TryParse(CoachEditAge.Text, out int ageResult))
                {
                    throw new FormatException($"Could not parse value {CoachEditAge.Text} to a integer value");
                }
                if (!double.TryParse(CoachEditSalary.Text, out double salaryResult))
                {
                    throw new FormatException($"Could not parse value {CoachEditSalary.Text} to a integer value");
                }
                if (!Enum.TryParse(SoccerCoachRoleEditComboBox.SelectedItem?.ToString(), out SoccerCoachRole soccerCoachRole))
                {
                    throw new FormatException($"Could not parse value {SoccerCoachRoleEditComboBox.Text} to a SoccerCoachRole");
                }

                Coach coachToUpdate = viewModel.Coaches.FirstOrDefault(c => c.Id == viewModel.CoachSelectedForEdit.Id);

                if (coachToUpdate != null)
                {
                    coachToUpdate.firstName = CoachEditFirstName.Text;
                    coachToUpdate.lastName = CoachEditLastName.Text;
                    coachToUpdate.age = ageResult;
                    coachToUpdate.salary = salaryResult;
                    coachToUpdate.role = soccerCoachRole;
                }

                EditCoachSection.Visibility = Visibility.Collapsed;
                viewModel.CoachSelectedForEdit = new Coach();
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

        private async void RemoveCoach(object sender, RoutedEventArgs e)
        {
            try
            {
                Coach coachToRemove = viewModel.Coaches.FirstOrDefault(c => c.Id == viewModel.CoachSelectedForEdit.Id);
                if (coachToRemove != null)
                {
                    foreach (Team team in viewModel.Teams)
                    {
                        if (team.coach != null && team.coach.Id == coachToRemove.Id)
                        {
                            team.coach = null;
                        }
                    }

                    viewModel.Coaches.Remove(coachToRemove);
                }

                //SetAvailableCoaches();
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
