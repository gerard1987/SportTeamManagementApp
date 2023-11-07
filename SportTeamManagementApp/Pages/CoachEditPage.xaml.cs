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
    public sealed partial class CoachEditPage : Page
    {
        ViewModel viewModel;
        public CoachEditPage()
        {
            this.InitializeComponent();

            viewModel = App.SharedViewModel;

            CoachToEditComboBox.ItemsSource = viewModel.Coaches.Select(c => new { Key = c.Id, Value = c.FirstName });
        }

        public async void SelectCoachToEdit(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CoachToEditComboBox.SelectedValue == null)
                {
                    throw new ArgumentException("No coach selected!");
                }

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
                        if (viewModel.CoachSelectedForEdit.Role.Equals(role))
                        {
                            selectedRole = i;
                        }
                    }

                    CoachEditFirstName.Text = viewModel.CoachSelectedForEdit.FirstName;
                    CoachEditLastName.Text = viewModel.CoachSelectedForEdit.LastName;
                    CoachEditAge.Text = viewModel.CoachSelectedForEdit.Age.ToString();
                    CoachEditSalary.Text = viewModel.CoachSelectedForEdit.Salary.ToString();

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
                CoachModel coachToUpdate = viewModel.Coaches.FirstOrDefault(c => c.Id == viewModel.CoachSelectedForEdit.Id);

                if (coachToUpdate != null)
                {
                    coachToUpdate.FirstName = CoachEditFirstName.Text;
                    coachToUpdate.LastName = CoachEditLastName.Text;
                    coachToUpdate.Age = CoachEditAge.Text;
                    coachToUpdate.Salary = CoachEditSalary.Text;
                    coachToUpdate.Role = SoccerCoachRoleEditComboBox.SelectedItem?.ToString();
                }

                viewModel.CoachSelectedForEdit = new CoachModel();

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

        private async void RemoveCoach(object sender, RoutedEventArgs e)
        {
            try
            {
                CoachModel coachToRemove = viewModel.Coaches.FirstOrDefault(c => c.Id == viewModel.CoachSelectedForEdit.Id);
                if (coachToRemove != null)
                {
                    foreach (TeamModel team in viewModel.Teams)
                    {
                        if (team.coach != null && team.coach.Id == coachToRemove.Id)
                        {
                            team.coach = null;
                        }
                    }

                    viewModel.Coaches.Remove(coachToRemove);
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
