using SportTeamManagementApp.Data.Enums;
using SportTeamManagementApp.Data.Models;
using SportTeamManagementApp.Data.Models.Interfaces;
using SportTeamManagementApp.Pages;
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
        public MainPage()
        {
            this.InitializeComponent();

            ViewModel viewModel = App.SharedViewModel;
        }

        private void MainMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainMenu.SelectedItem != null)
            {
                string selectedItem = ((ListViewItem)MainMenu.SelectedItem).Content.ToString();

                switch (selectedItem)
                {
                    case "Create Coach":
                        Frame.Navigate(typeof(CoachCreatePage));
                        break;
                    case "Create Player":
                        Frame.Navigate(typeof(PlayerCreatePage));
                        break;
                    case "Create Team":
                        Frame.Navigate(typeof(TeamCreatePage));
                        break;
                    case "Edit Team":
                        Frame.Navigate(typeof(TeamEditPage));
                        break;
                    case "Edit Player":
                        Frame.Navigate(typeof(PlayerEditPage));
                        break;
                    case "Edit Coach":
                        Frame.Navigate(typeof(CoachEditPage));
                        break;

                    case "Create match":
                        Frame.Navigate(typeof(CreateGoalPage));
                        break;
                    case "Add goal to match":
                        Frame.Navigate(typeof(GoalCreatePage));
                        break;
                    case "Edit match":
                        Frame.Navigate(typeof(MatchEditPage));
                        break;
                    case "Exit":
                        Application.Current.Exit();
                        break;
                }

                MainSplitView.IsPaneOpen = false;
            }
        }

        private async Task ShowExceptionMessage(string errorMessage)
        {
            ErrorDialog.Content = errorMessage;
            await ErrorDialog.ShowAsync();
        }
    }
}
