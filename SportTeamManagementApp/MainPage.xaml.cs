using SportTeamManagementApp.Enums;
using SportTeamManagementApp.Models;
using System;
using System.Collections.Generic;
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
        public Sport soccer = new Sport("Soccer");
        public List<Coach> coaches = new List<Coach>();
        public List<Player> players = new List<Player>();
        public List<Team> teams = new List<Team>();

        public MainPage()
        {
            this.InitializeComponent();
            SoccerCoachRoleComboBox.ItemsSource = Enum.GetValues(typeof(SoccerCoachRole));
            SoccerPlayerRoleComboBox.ItemsSource = Enum.GetValues(typeof(SoccerPlayerRole));

            TeamPlayersComboBox.ItemsSource = players;
            TeamCoachesComboBox.ItemsSource = coaches;
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
                        break;
                    case "Create Player":
                        CreatePlayerSection.Visibility = Visibility.Visible;
                        break;
                    case "Create Team":
                        CreateTeamSection.Visibility = Visibility.Visible;
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
                newCoach.role = soccerCoachRole;

                coaches.Add(newCoach);

                CreateCoachSection.Visibility = Visibility.Collapsed;
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

                players.Add(newPlayer);

                CreatePlayerSection.Visibility = Visibility.Collapsed;
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

        private async void CreateTeam(object sender, RoutedEventArgs e)
        {
            try
            {
                if (coaches.Count < 1 || players.Count < 1)
                {
                    throw new ArgumentException("Cannot create a team without having created a coach or players first!");
                }

                CreateTeamSection.Visibility = Visibility.Collapsed;
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

        private async Task ShowExceptionMessage(string errorMessage)
        {
            ErrorDialog.Content = errorMessage;
            await ErrorDialog.ShowAsync();
        }
    }
}
