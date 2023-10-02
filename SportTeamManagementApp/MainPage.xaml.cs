using SportTeamManagementApp.Enums;
using SportTeamManagementApp.Models;
using SportTeamManagementApp.Models.Interfaces;
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
        public MainPage()
        {
            this.InitializeComponent();

            Sport soccer = new Sport("Soccer");

            ISoccerCoach coachArteta = new Coach()
            {
                firstName = "Mikel",
                lastName = "Arteta",
                age = 56,
                salary = 87000.0,
                role = SoccerCoachRole.HeadCoach
            };

            ISoccerPlayer playerMessi = new Player()
            {
                firstName = "Lionel",
                lastName = "Messi",
                age = 36,
                salary = 54000000.0,
                role = SoccerPlayerRole.Forward
            };

            ISoccerPlayer playerRamos = new Player()
            {
                firstName = "Sergio",
                lastName = "Ramos",
                age = 37,
                salary = 11700000.0,
                role = SoccerPlayerRole.Defender
            };

            List<ISoccerPlayer> arsenalPlayers = new List<ISoccerPlayer>();
            arsenalPlayers.Add(playerMessi);
            arsenalPlayers.Add(playerRamos);

            Team arsenal = new Team("Arsenal", coachArteta, arsenalPlayers);
            Team liverpool = new Team("Liverpool", coachArteta, new List<ISoccerPlayer>() { });

            soccer.teams.Add(arsenal);

            coachArteta.RemovePlayerFromTeam(arsenal, playerMessi);
            playerRamos.ChangeTeams(arsenal, liverpool);
        }
    }
}
