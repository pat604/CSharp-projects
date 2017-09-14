using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CurrentProject.Class;
using CurrentProject.Database;
using System.Collections.ObjectModel;
using System.Collections;
using System.Windows;
using System.Diagnostics;
using CurrentProject.MNBService;
using System.Xml.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

namespace CurrentProject.ViewModel
{
    class MainWindowViewModel : Bindable
    {

        private ProjectManagement projectManagement;

        public MainWindowViewModel()
        {
            projectManagement = new ProjectManagement();
            topWelcomeLabel = "Üdv " + Session.Get<string>("U_Firstname") + " " + Session.Get<string>("U_Lastname");
            AutoRefreshData();
            AutoRefreshChat();
            MNBWCF = MNBProcess();
        }

        private string topWelcomeLabel;

        public string TopWelcomeLabel
        {
            get { return topWelcomeLabel; }
        }

        private Visibility mainGridVisibility = Visibility.Hidden;

        public Visibility MainGridVisibility
        {
            get { return mainGridVisibility; }
            private set { mainGridVisibility = value; OnPropertyChanged(); }
        }

        private IEnumerable userProjects;

        public IEnumerable UserProjects
        {
            get { return userProjects; }
            set { userProjects = value; OnPropertyChanged(); }
        }

        private int selectedProjectId;

        public int SelectedProjectId
        {
            get { return selectedProjectId; }
            set { selectedProjectId = value; OnPropertyChanged(); SetSelectedProject(); }
        }

        public void SetSelectedProject()
        {
            SelectedProject = projectManagement.CurrentProject(selectedProjectId);
            SelectedProjectMember = projectManagement.CurrentProjectMembers(selectedProjectId);
            MainGridVisibility = Visibility.Visible;
            ChatMessages = projectManagement.GetCurrentChatMessages(selectedProjectId);           
        }

        private Project selectedProject;

        public Project SelectedProject
        {
            get { return selectedProject; }
            set { selectedProject = value; OnPropertyChanged(); }
        }
        
        private IEnumerable selectedProjectMember;

        public IEnumerable SelectedProjectMember
        {
            get { return selectedProjectMember; }
            set { selectedProjectMember = value; OnPropertyChanged(); }
        }

        private List<Milestone> currentMilestones;

        public List<Milestone> CurrentMilestones
        {
            get { return currentMilestones; }
            set { currentMilestones = value; OnPropertyChanged(); }
        }

        private ObservableCollection<IEnumerable> tasksByMilestones;

        public ObservableCollection<IEnumerable> TasksByMilestones
        {
            get { return tasksByMilestones; }
            set { tasksByMilestones = value; OnPropertyChanged(); }
        } 

        private IEnumerable chatMessages;

        public IEnumerable ChatMessages
        {
            get { return chatMessages; }
            set { chatMessages = value; OnPropertyChanged(); }
        }

        private string textBoxChatMessage;

        public string TextBoxChatMessage
        {
            get { return textBoxChatMessage; }
            set { textBoxChatMessage = value; OnPropertyChanged(); }
        }

        private string mNBWCF;

        public string MNBWCF
        {
            get { return mNBWCF; }
            set { mNBWCF = value; OnPropertyChanged(); }
        }

        public string MNBProcess()
        {
            MNBArfolyamServiceSoapClient client = new MNBArfolyamServiceSoapClient();
            XDocument doc = XDocument.Parse(client.GetCurrentExchangeRates());

            string retString = "";

            retString += "EUR: " + doc.Descendants("Rate").Where(rate => rate.Attribute("curr").Value == "EUR").SingleOrDefault().Value + " Ft";
            retString += "\t USD: " + doc.Descendants("Rate").Where(rate => rate.Attribute("curr").Value == "USD").SingleOrDefault().Value + " Ft" + Environment.NewLine;
            retString += "GBP: " + doc.Descendants("Rate").Where(rate => rate.Attribute("curr").Value == "GBP").SingleOrDefault().Value + " Ft";
            retString += "\t CHF: " + doc.Descendants("Rate").Where(rate => rate.Attribute("curr").Value == "CHF").SingleOrDefault().Value + " Ft";

            return retString;
        }

        public void ManualRefreshData()
        {
            UserProjects = projectManagement.GetUserProjects();

            if (SelectedProjectId != 0)
            {
                SetSelectedProject();
            }
        }

        object refreshLock = new object();

        private void AutoRefreshData()
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                while (true && Session.Get<bool>("U_Login"))
                {
                    lock (refreshLock)
                    {
                        ManualRefreshData();
                        System.Threading.Thread.Sleep(10000);
                    }
                }
            });
        }

        public void NewChatMessageButtonClick()
        {
            if (textBoxChatMessage != null && textBoxChatMessage != String.Empty)
            {
                projectManagement.CreateMessageSend(selectedProjectId, textBoxChatMessage);
                TextBoxChatMessage = String.Empty;
            }
        }

        private void ManualRefreshChat()
        {
            if (selectedProjectId != 0)
            {
                ChatMessages = projectManagement.GetCurrentChatMessages(selectedProjectId);
            }
        }

        object refreshChatLock = new object();

        private void AutoRefreshChat()
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                while (true && Session.Get<bool>("U_Login"))
                {
                    lock (refreshChatLock)
                    {
                        ManualRefreshChat();
                        System.Threading.Thread.Sleep(1000);
                    }
                }
            });
        }

        public void ManualRefreshTask()
        {
            TasksByMilestones = projectManagement.GetAllTasksByMilestones(selectedProjectId);
            CurrentMilestones = projectManagement.GetCurrentMilestones(selectedProject.MethodologyId);
        }

        private StackPanel currentMileStoneStackPanel;

        public StackPanel CurrentMileStoneStackPanel
        {
            get { return currentMileStoneStackPanel; }
            set { currentMileStoneStackPanel = value; }
        }

        public void BuildMilestoneGUI()
        {

            if (selectedProjectId != 0)
            {

                ManualRefreshTask();

                CurrentMileStoneStackPanel.Children.Clear();

                for (int i = 0; i < CurrentMilestones.Count; i++)
                {
                    StackPanel stackpanelCurrentMilestone = new StackPanel()
                    {
                        Margin = new Thickness(0, 0, 0, 20)
                    };

                    Label labelMilestoneName = new Label()
                    {
                        Height = 25
                    };
                    Binding binding = new Binding("Name");
                    binding.Source = CurrentMilestones.ElementAt(i);
                    labelMilestoneName.SetBinding(Label.ContentProperty, binding);

                    DataTemplate tasksLayout = new DataTemplate();

                    ListBox listboxTasks = new ListBox()
                    {
                        Margin = new Thickness(5, 0, 5, 0)
                    };

                    listboxTasks.ItemsSource = TasksByMilestones.ElementAt(i);

                    FrameworkElementFactory task = new FrameworkElementFactory(typeof(CheckBox));
                    task.SetBinding(CheckBox.ContentProperty, new Binding("FullString"));

                    Binding completedBinding = new Binding("Completed");
                    completedBinding.Mode = BindingMode.OneWay;

                    task.SetBinding(CheckBox.IsCheckedProperty, completedBinding);
                    task.SetValue(CheckBox.FontSizeProperty, 12.0);
                    task.SetValue(CheckBox.MarginProperty, new Thickness(0, 0, 15, 0));
                    task.SetValue(CheckBox.VerticalContentAlignmentProperty, VerticalAlignment.Center);

                    FrameworkElementFactory spFactory = new FrameworkElementFactory(typeof(StackPanel));
                    spFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);
                    spFactory.AppendChild(task);

                    tasksLayout.VisualTree = spFactory;
                    listboxTasks.ItemTemplate = tasksLayout;

                    stackpanelCurrentMilestone.Children.Add(labelMilestoneName);
                    stackpanelCurrentMilestone.Children.Add(listboxTasks);
                    CurrentMileStoneStackPanel.Children.Add(stackpanelCurrentMilestone);
                }

            }
        }

    }
}
