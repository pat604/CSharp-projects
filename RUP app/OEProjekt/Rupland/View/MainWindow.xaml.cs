using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using CurrentProject.Class;
using CurrentProject.ViewModel;
using CurrentProject.ViewModel.Converter;
using System.Windows.Threading;
using System.Threading;

namespace CurrentProject.View
{
    public partial class MainWindow : Window
    {

        MainWindowViewModel MWVM;

        public MainWindow()
        {
            InitializeComponent();
            MWVM = new MainWindowViewModel();
            DataContext = MWVM;
            MWVM.CurrentMileStoneStackPanel = stackPanelMilestones;
        }

        private void buttonNewProject_Click(object sender, RoutedEventArgs e)
        {
            NewProjectWindow NPW = new NewProjectWindow();
            NPW.ShowDialog();
            if (NPW.DialogResult == true)
            {
                MWVM.ManualRefreshData();
            }
        }

        private void buttonNewMember_Click(object sender, RoutedEventArgs e)
        {
            NewMemberWindow NMW = new NewMemberWindow(MWVM.SelectedProjectId);
            NMW.ShowDialog();
            if (NMW.DialogResult == true)
            {
                MWVM.ManualRefreshData();
            }
        }

        private void buttonNewMessage_Click(object sender, RoutedEventArgs e)
        {
            MWVM.NewChatMessageButtonClick();
        }

        private void buttonLogout_Click(object sender, RoutedEventArgs e)
        {
            Session.Clear();
            Session.Set("U_Login", false);
            LoginWindow LW = new LoginWindow();
            LW.Show();
            this.Close();
        }

        private void buttonSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow SW = new SettingsWindow();
            SW.ShowDialog();
            if (SW.DialogResult == true)
            {
                MWVM.ManualRefreshData();
            }
        }

        private void buttonNewTask_Click(object sender, RoutedEventArgs e)
        {
            NewTaskWindow NTW = new NewTaskWindow(MWVM.SelectedProjectId);
            NTW.ShowDialog();
            if (NTW.DialogResult == true)
            {
                MWVM.ManualRefreshData();
                MWVM.BuildMilestoneGUI();
            }
        }

        private void buttonRefreshStackPanel(object sender, RoutedEventArgs e)
        {
            stackPanelMilestones.Children.Clear();
            MWVM.BuildMilestoneGUI();
        }

        private void ListBoxProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            stackPanelMilestones.Children.Clear();
            MWVM.BuildMilestoneGUI();
        }

    }
}
