using SportTeamManagementApp.Enums;
using SportTeamManagementApp.Models;
using SportTeamManagementApp.Models.Interfaces;
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
            Dictionary<string, string> inputValues = new Dictionary<string, string>();

            foreach (var control in CreateCoachSection.Children)
            {
                if (control is TextBox textBox)
                {
                    if (!String.IsNullOrEmpty(textBox.Text))
                    {
                        inputValues[textBox.Name] = textBox.Text;
                    }
                    else
                    {
                        await ShowExceptionMessage($"No value provided for {textBox.Name} ");
                    }
                }
                else if (control is ComboBox comboBox)
                {
                    if (comboBox.SelectedItem is SoccerCoachRole selectedRole)
                    {
                        inputValues[comboBox.Name] = selectedRole.ToString();
                    }
                }
            }
        }

        private async Task ShowExceptionMessage(string errorMessage)
        {
            ErrorDialog.Content = errorMessage;
            await ErrorDialog.ShowAsync();
        }
    }
}
