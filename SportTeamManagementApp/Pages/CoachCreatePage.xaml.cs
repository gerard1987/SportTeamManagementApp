using SportTeamManagementApp.Data;
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
using SportTeamManagementApp.Data.Enums;

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
                CoachModel newCoach = new CoachModel
                {
                    FirstName = CoachFirstName.Text,
                    LastName = CoachLastName.Text,
                    Age = CoachAge.Text,
                    Salary = CoachSalary.Text,
                    Role = SoccerCoachRoleComboBox.SelectedItem?.ToString()
                };

                viewModel.Coaches.Add(newCoach);

                using (var context = new AppDbContext())
                {
                    var newEntity = new Data.Entities.Coach() {
                        FirstName = newCoach.firstName,
                        LastName = newCoach.lastName,
                        Age = int.Parse(newCoach.Age),
                        Salary = newCoach.salary,
                        Role = Enum.Parse<SoccerCoachRole>(newCoach.Role)
                    };
                    context.Coaches.Add(newEntity);
                    context.SaveChanges();
                }

                Frame.Navigate(typeof(MainPage));

            }
            catch (ArgumentOutOfRangeException aoEx)
            {
                await ShowExceptionMessage(aoEx.Message);
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
