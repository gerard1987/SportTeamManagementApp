using SportTeamManagementApp.Data.Entities;
using SportTeamManagementApp.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SportTeamManagementApp
{
    public sealed partial class PlayerResults : UserControl
    {
        ViewModel viewModel;

        public PlayerResults()
        {
            this.InitializeComponent();

            viewModel = App.SharedViewModel;

            var playerGoalDetails = viewModel.dataProvider.Goals
                .GroupBy(goal => goal.PlayerId)
                .Select(group => new
                {
                    PlayerId = group.Key,
                    PlayerName = group.FirstOrDefault()?.Player.firstName,
                    GoalsScored = group.Count()
                })
                .ToList();

            PlayerGoalsListBox.ItemsSource = playerGoalDetails;

        }
    }

}
