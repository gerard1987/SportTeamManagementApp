using SportTeamManagementApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SportTeamManagementApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Sport soccer;
        private Team selectedTeam;

        public MainPage()
        {
            this.InitializeComponent();

            Sport soccer = new Sport("Soccer");
            Team arsenal = new Team("Arsenal");
            Team liverPool = new Team("Liverpool");
            Team ManUnited = new Team("Manchester united");

            this.soccer = soccer;

            this.soccer.teams.Add(arsenal);
            this.soccer.teams.Add(liverPool);
            this.soccer.teams.Add(ManUnited);

            // Fill select team dropdown
            foreach (Team team in soccer.teams)
            {
                selectteam.Items.Add(new ComboBoxItem() { Content = team.name });
            }

            //Player playerOne = new Player()
            //{
            //    name = "Messi",
            //    age = 36,
            //    salary = 54000000.0
            //};
            //playerOne.asignRoleToTeamMember(Role.Forward);

            //Player playerTwo = new Player()
            //{
            //    name = "Van der Sar",
            //    age = 52,
            //    salary = 417000.0
            //};

            //playerTwo.asignRoleToTeamMember(Role.Goalkeeper);
        }

        private void CreateTeam(object sender, RoutedEventArgs e)
        {
            string teamName = TeamName.Text;

            Team newTeam = new Team(teamName);
            soccer.teams.Add(newTeam);

            selectteam.Items.Add(new ComboBoxItem() { Content = newTeam.name });
        }

        private void SelectTeam(object sender, RoutedEventArgs e)
        {
            if (selectteam.SelectedItem != null)
            {
                string selectedTeamName = ((ComboBoxItem)selectteam.SelectedItem).Content.ToString();

                this.selectedTeam = this.soccer.teams.Find(t => t.name == selectedTeamName);

                foreach (Player player in this.selectedTeam.players)
                {
                    selectplayer.Items.Add(new ComboBoxItem() { Content = player.name });
                }
            }
            
        }

        private void SelectPlayer(object sender, RoutedEventArgs e)
        {

        }
    }
}
