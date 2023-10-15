using SportTeamManagementApp.Enums;
using SportTeamManagementApp.Models;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SportTeamManagementApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            SoccerCoachRoleComboBox.ItemsSource = Enum.GetValues(typeof(SoccerCoachRole));
            Sport soccer = new Sport("Soccer");


        }

        private void MainMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainMenu.SelectedItem != null)
            {
                string selectedItem = (MainMenu.SelectedItem as ListViewItem).Content.ToString();

                switch (selectedItem)
                {
                    case "Create Coach":
                        CreateCoachSection.Visibility = Visibility.Visible;
                        // Navigate to the Create Coach page or show a relevant submenu.
                        // You can use the ContentArea grid to display content.
                        break;
                    case "Create Player":
                        // Navigate to the Create Player page or show a relevant submenu.
                        break;
                    case "Create Team":
                        // Navigate to the Create Team page or show a relevant submenu.
                        break;
                    case "Exit":
                        // Handle the exit option.
                        Application.Current.Exit();
                        break;
                        // Add more cases for additional options.
                }
            }

            // Clear the selection to allow reselection.
            MainMenu.SelectedItem = null;
        }

        private async void CreateCoach(object sender, RoutedEventArgs e)
        {
            try
            {
                Coach newCoach = new Coach();
                newCoach = ValidateTeamMember(newCoach) as Coach;
            }
            catch(ArgumentException aEx)
            {
                await ShowExceptionMessage(aEx.Message);
            }
            catch(FormatException fEx)
            {
                await ShowExceptionMessage(fEx.Message);
            }
            catch(Exception ex)
            {
                await ShowExceptionMessage($"Something went wrong {ex.Message} ");
            }
        }

        private async void CreatePlayer(object sender, RoutedEventArgs e)
        {
            try
            {
                Player newPlayer = new Player();
                newPlayer = ValidateTeamMember(newPlayer) as Player;
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

        private object ValidateTeamMember(Object teamMember)
        {
            string firstName = "";
            string lastName = "";
            var age = "";
            var salary = "";
            var role = "";

            if (teamMember is Player)
            {
                teamMember = teamMember as Player;
                firstName = PlayerFirstName.Text;
                lastName = PlayerLastName.Text;
                age = PlayerAge.Text;
                salary = PlayerSalary.Text;
                role = SoccerPlayerRoleComboBox.Text;
            }
            else if (teamMember is Coach)
            {
                teamMember = teamMember as Coach;
                firstName = CoachFirstName.Text;
                lastName = CoachLastName.Text;
                age = CoachAge.Text;
                salary = CoachSalary.Text;
                role = SoccerCoachRoleComboBox.Text;
            }

            if (String.IsNullOrEmpty(firstName) || String.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException("First name or last name cannot be empty.");
            }
            if (!Int32.TryParse(age, out int ageResult))
            {
                throw new FormatException($"Could not parse value {age} to a integer value");
            }
            if (!double.TryParse(salary, out double salaryResult))
            {
                throw new FormatException($"Could not parse value {salary} to a integer value");
            }
            if (!Enum.TryParse(role, out SoccerCoachRole soccerCoachRole))
            {
                throw new FormatException($"Could not parse value {role} to a SoccerCoach role");
            }

            return teamMember;
        }

        private async Task ShowExceptionMessage(string errorMessage)
        {
            ErrorDialog.Content = errorMessage;
            await ErrorDialog.ShowAsync();
        }
    }
}
