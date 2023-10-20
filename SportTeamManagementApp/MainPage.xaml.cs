using SportTeamManagementApp.Enums;
using SportTeamManagementApp.Models;
using SportTeamManagementApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<Player> selectedPlayersForTeam = new List<Player>();
        public Team teamSelectedForEdit;
        public Player playerSelectedForEdit;
        public Coach coachSelectedForEdit;
        List<StackPanel> sections;

        public MainPage()
        {
            this.InitializeComponent();
            SoccerCoachRoleComboBox.ItemsSource = Enum.GetValues(typeof(SoccerCoachRole));
            SoccerPlayerRoleComboBox.ItemsSource = Enum.GetValues(typeof(SoccerPlayerRole));

            sections = new List<StackPanel>()
            {
                CreateCoachSection,
                CreatePlayerSection,
                CreateTeamSection,
                EditTeamSelectSection,
                EditPlayerSelectSection,
                EditCoachSelectSection,
                EditTeamSection,
                EditPlayerSection,
                EditCoachSection
            };
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
                        CollapseSections(CreateCoachSection);
                        break;
                    case "Create Player":
                        CreatePlayerSection.Visibility = Visibility.Visible;
                        CollapseSections(CreatePlayerSection);
                        break;
                    case "Create Team":
                        CreateTeamSection.Visibility = Visibility.Visible;
                        CollapseSections(CreateTeamSection);
                        break;
                    case "Edit Team":
                        EditTeamSelectSection.Visibility = Visibility.Visible;
                        CollapseSections(EditTeamSelectSection);
                        break;
                    case "Edit Player":
                        EditPlayerSelectSection.Visibility = Visibility.Visible;
                        CollapseSections(EditPlayerSelectSection);
                        break;
                    case "Edit Coach":
                        EditCoachSelectSection.Visibility = Visibility.Visible;
                        CollapseSections(EditCoachSelectSection);
                        break;
                    case "Exit":
                        Application.Current.Exit();
                        break;
                }
            }

            // Clear the selection to allow reselection.
            MainMenu.SelectedItem = null;
        }

        private void CollapseSections(StackPanel currentlySelected)
        {
            foreach(StackPanel section in sections)
            {
                if (section != currentlySelected)
                {
                    section.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            foreach (StackPanel section in sections)
            {
                section.Visibility = Visibility.Collapsed;
            }
        }

        private void ClearFields(StackPanel section)
        {
            if (section.Children.Any())
            {
                foreach (var child in section.Children)
                {
                    if (child is TextBox textBox)
                    {
                        textBox.Text = "";
                        string text = textBox.Text;
                    }
                    if (child is ComboBox comboBox)
                    {
                        comboBox.SelectedIndex = 0;
                    }
                }
            }
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

                coaches.Add(newCoach);
                TeamCoachesComboBox.ItemsSource = coaches.Select(c => new { Key = c.Id, Value = c.firstName }).ToList();
                CoachToEditComboBox.ItemsSource = coaches.Select(c => new { Key = c.Id, Value = c.firstName }).ToList();

                CreateCoachSection.Visibility = Visibility.Collapsed;
                ClearFields(CreateCoachSection);
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
                TeamPlayersComboBox.ItemsSource = players.Select(p => new { Key = p.Id, Value = p.firstName }).ToList();
                PlayerToEditComboBox.ItemsSource = players.Select(p => new { Key = p.Id, Value = p.firstName }).ToList();

                CreatePlayerSection.Visibility = Visibility.Collapsed;
                ClearFields(CreatePlayerSection);
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
                if (String.IsNullOrEmpty(TeamName.Text))
                {
                    throw new ArgumentException("Team name cannot be empty!");
                }
                if (selectedPlayersForTeam.Count < 1)
                {
                    throw new ArgumentException("Please select at least 1 player for the team");
                }

                Int32.TryParse(TeamCoachesComboBox.SelectedValue.ToString(), out int coachId);
                Coach coach = coaches.Find(c => c.Id == coachId);

                List<Player> playersToAdd = selectedPlayersForTeam;
                Team newTeam = new Team(TeamName.Text, coach, playersToAdd);

                teams.Add(newTeam);
                TeamsToEditComboBox.ItemsSource = teams.Select(t => new { Key = t.Id, Value = t.name }).ToList();

                CreateTeamSection.Visibility = Visibility.Collapsed;
                selectedPlayersForTeam = new List<Player>();
                ClearFields(CreateTeamSection);
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

        public async void AddPlayerToTeam(object sender, RoutedEventArgs e)
        {
            try
            {
                Int32.TryParse(TeamPlayersComboBox.SelectedValue.ToString(), out int playerId);
                Player playerToAdd = players.Find(p => p.Id == playerId);
                bool exists = selectedPlayersForTeam.Exists(sp => sp.Id == playerId);
                if (exists)
                {
                    throw new ArgumentException($"Player already in team!");
                }
                selectedPlayersForTeam.Add(playerToAdd);
                SelectedPlayersListBox.ItemsSource = selectedPlayersForTeam.Select(c => new { Key = c.Id, Value = c.firstName }).ToList();
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

        public async void SelectTeamToEdit(object sender, RoutedEventArgs e)
        {
            try
            {
                Int32.TryParse(TeamsToEditComboBox.SelectedValue.ToString(), out int teamId);
                teamSelectedForEdit = teams.Find(t => t.Id == teamId);

                if (teamSelectedForEdit != null)
                {
                    PlayersInTeamComboBox.ItemsSource = teamSelectedForEdit.players.Select(p => new { Key = p.Id, Value = p.firstName }).ToList();
                    EditTeamSelectSection.Visibility = Visibility.Collapsed;
                    EditTeamSection.Visibility = Visibility.Visible;
                    TeamNameEdit.Text = teamSelectedForEdit.name;
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
        public async void EditTeam(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(TeamNameEdit.Text))
                {
                    throw new ArgumentException("Team name can't be empty");
                }

                Team teamToUpdate = teams.Find(t => t.Id == teamSelectedForEdit.Id);
                teamToUpdate.name = TeamNameEdit.Text;
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

        public async void RemovePlayerFromTeam(object sender, RoutedEventArgs e)
        {
            try
            {
                Int32.TryParse(PlayersInTeamComboBox.SelectedValue.ToString(), out int playerId);
                if (teamSelectedForEdit == null)
                {
                    throw new ArgumentException("No team selected");
                }
                Player teamPlayer = teamSelectedForEdit.players.Find(p => p.Id == playerId);

                teamSelectedForEdit.players.Remove(teamPlayer);
                PlayersInTeamComboBox.ItemsSource = teamSelectedForEdit.players.Select(p => new { Key = p.Id, Value = p.firstName }).ToList();
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

        public async void SelectPlayerToEdit(object sender, RoutedEventArgs e)
        {
            try
            {
                Int32.TryParse(PlayerToEditComboBox.SelectedValue.ToString(), out int playerId);
                playerSelectedForEdit = players.Find(p => p.Id == playerId);

                if (playerSelectedForEdit != null)
                {
                    EditPlayerSelectSection.Visibility = Visibility.Collapsed;
                    EditPlayerSection.Visibility = Visibility.Visible;

                    Array roles = Enum.GetValues(typeof(SoccerPlayerRole));
                    int selectedRole = 0;
                    for (int i = 0; i < roles.Length; i++)
                    {
                        SoccerPlayerRole role = (SoccerPlayerRole)roles.GetValue(i);
                        if (playerSelectedForEdit.role.Equals(role))
                        {
                            selectedRole = i;
                        }
                    }

                    Enum.TryParse(playerSelectedForEdit.role.ToString(), out SoccerPlayerRole parsedRole);

                    PlayerEditFirstName.Text = playerSelectedForEdit.firstName;
                    PlayerEditLastName.Text = playerSelectedForEdit.lastName;
                    PlayerEditAge.Text = playerSelectedForEdit.age.ToString();
                    PlayerEditSalary.Text = playerSelectedForEdit.salary.ToString();
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

                Player playerToUpdate = players.FirstOrDefault(p => p.Id == playerSelectedForEdit.Id);

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

        public async void SelectCoachToEdit(object sender, RoutedEventArgs e)
        {
            try
            {
                Int32.TryParse(CoachToEditComboBox.SelectedValue.ToString(), out int coachId);
                coachSelectedForEdit = coaches.Find(p => p.Id == coachId);

                if (coachSelectedForEdit != null)
                {
                    EditCoachSelectSection.Visibility = Visibility.Collapsed;
                    EditCoachSection.Visibility = Visibility.Visible;

                    Array roles = Enum.GetValues(typeof(SoccerCoachRole));
                    int selectedRole = 0;
                    for (int i = 0; i < roles.Length; i++)
                    {
                        SoccerCoachRole role = (SoccerCoachRole)roles.GetValue(i);
                        if (coachSelectedForEdit.role.Equals(role))
                        {
                            selectedRole = i;
                        }
                    }

                    CoachEditFirstName.Text = coachSelectedForEdit.firstName;
                    CoachEditLastName.Text = coachSelectedForEdit.lastName;
                    CoachEditAge.Text = coachSelectedForEdit.age.ToString();
                    CoachEditSalary.Text = coachSelectedForEdit.salary.ToString();
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

                Coach coachToUpdate = coaches.FirstOrDefault(c => c.Id == coachSelectedForEdit.Id);

                if (coachToUpdate != null)
                {
                    coachToUpdate.firstName = CoachEditFirstName.Text;
                    coachToUpdate.lastName = CoachEditLastName.Text;
                    coachToUpdate.age = ageResult;
                    coachToUpdate.salary = salaryResult;
                    coachToUpdate.role = soccerCoachRole;
                }

                EditCoachSection.Visibility = Visibility.Collapsed;
                coachSelectedForEdit = new Coach();
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
