using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CurrentProject.Class;
using CurrentProject.Database;
using System.Collections.ObjectModel;
using System.Collections;

namespace CurrentProject.ViewModel
{
    class NewTaskViewModel : Bindable
    {
        
        private TaskManagement taskmanagement;

        public NewTaskViewModel(int projectid)
        {
            taskmanagement = new TaskManagement(projectid);
            currentProjectId = projectid;
            comboBoxMembers = taskmanagement.GetProjectMembers();
            comboBoxMilestones = taskmanagement.GetCurrentMilestones();        
        }

        private int currentProjectId;

        public int CurrentProjectId
        {
            get { return currentProjectId; }
        }

        private string textBoxTaskName;

        public string TextBoxTaskName
        {
            get { return textBoxTaskName; }
            set { textBoxTaskName = value; OnPropertyChanged(); }
        }

        private string textBoxDescription;

        public string TextBoxDescription
        {
            get { return textBoxDescription; }
            set { textBoxDescription = value; OnPropertyChanged(); }
        }

        private List<Milestone> comboBoxMilestones;

        public List<Milestone> ComboBoxMilestones
        {
            get { return comboBoxMilestones; }
        }

        private int selectedMilestoneId = 1;

        public int SelectedMilestoneId
        {
            get { return selectedMilestoneId; }
            set { selectedMilestoneId = value; OnPropertyChanged(); }
        }

        private IEnumerable comboBoxMembers;

        public IEnumerable ComboBoxMembers
        {
            get { return comboBoxMembers; }
        }

        private int selectedMemberUserId = Session.Get<int>("U_Id");

        public int SelectedMemberUserId
        {
            get { return selectedMemberUserId; }
            set { selectedMemberUserId = value; OnPropertyChanged(); }
        }

        private string labelError;

        public string LabelError
        {
            get { return labelError; }
            set { labelError = value; OnPropertyChanged(); }
        }

        public bool OKButtonClick()
        {
            if (textBoxTaskName == null || textBoxTaskName == String.Empty ||
                textBoxDescription == null || textBoxDescription == String.Empty ||
                comboBoxMilestones == null || comboBoxMilestones.Count == 0 ||
                comboBoxMembers == null || selectedMemberUserId < 1)
            {
                LabelError = "Minden mező kitöltése kötelező!";   
            }
            else
            {
                taskmanagement.NewTask(textBoxTaskName, textBoxDescription, selectedMilestoneId, selectedMemberUserId);
                return true;
            }

            return false;
        }

    }


}
