using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CurrentProject.Class;
using CurrentProject.Database;
using System.Collections.ObjectModel;

namespace CurrentProject.ViewModel
{
    class NewProjectViewModel : Bindable
    {

        ProjectManagement projectManagement;

        private string textBoxName;

        public string TextBoxName
        {
            get { return textBoxName; }
            set { textBoxName = value; OnPropertyChanged(); }
        }

        private string textBoxDescription;

        public string TextBoxDescription
        {
            get { return textBoxDescription; }
            set { textBoxDescription = value; OnPropertyChanged(); }
        }

        private List<Difficulty> comboBoxDifficulty;

        public List<Difficulty> ComboBoxDifficulty
        {
            get { return comboBoxDifficulty; }
        }

        private int selectedDifficultyId = 1;

        public int SelectedDifficultyId
        {
            get { return selectedDifficultyId; }
            set { selectedDifficultyId = value; OnPropertyChanged(); }
        }

        private List<Methodology> comboBoxMethodology;

        public List<Methodology> ComboBoxMethodology
        {
            get { return comboBoxMethodology; }
        }

        private int selectedMethodologyId = 1;

        public int SelectedMethodologyId
        {
            get { return selectedMethodologyId; }
            set { selectedMethodologyId = value; OnPropertyChanged(); BuildAndCalculateInformation(); }
        }

        private DateTime datePickerStartDate = DateTime.Now;

        public DateTime DatePickerStartDate
        {
            get { return datePickerStartDate; }
            set {
                if (value <= DateTime.Now || value <= DatePickerEndDate)
                {
                    datePickerStartDate = value;
                }
                else
                {
                    value = DateTime.Now;
                }
                BuildAndCalculateInformation();
                OnPropertyChanged();
            }
        }

        private DateTime datePickerEndDate = DateTime.Now;

        public DateTime DatePickerEndDate
        {
            get { return datePickerEndDate; }
            set { datePickerEndDate = value; OnPropertyChanged(); BuildAndCalculateInformation(); }
        }

        private string labelError;

        public string LabelError
        {
            get { return labelError; }
            set { labelError = value; OnPropertyChanged(); }
        }

        private List<Milestone> methodologyMilestones;

        private string information;

        public string Information
        {
            get { return information; }
            set { information = value; OnPropertyChanged(); }
        }

        public NewProjectViewModel()
        {
            projectManagement = new ProjectManagement();
            comboBoxDifficulty = new List<Difficulty>();
            comboBoxMethodology = new List<Methodology>();
            comboBoxDifficulty = projectManagement.GetDifficulty();
            comboBoxMethodology = projectManagement.GetMethodology();
            methodologyMilestones = new List<Milestone>();
            BuildAndCalculateInformation();
        }

        public bool NewProjectButtonClick()
        {
            if (textBoxName == null || textBoxName == String.Empty ||
                textBoxDescription == null || textBoxDescription == String.Empty ||
                comboBoxDifficulty == null || comboBoxDifficulty.Count == 0 ||
                comboBoxMethodology == null || comboBoxMethodology.Count == 0 ||
                selectedDifficultyId == 0 || selectedMethodologyId == 0 ||
                datePickerStartDate == null || datePickerEndDate == null)
            {
                LabelError = "Minden mező kitöltése kötelező!";
            }
            else
            {

                if (projectManagement.NewProject(textBoxName, textBoxDescription, selectedDifficultyId, selectedMethodologyId, datePickerStartDate, datePickerEndDate))
                {
                    return true;
                }

            }

            return false;
        }

        public void BuildAndCalculateInformation()
        {
            methodologyMilestones = projectManagement.GetCurrentMilestones(selectedMethodologyId);

            TimeSpan diff = datePickerEndDate.Subtract(datePickerStartDate);
            int days = (int)Math.Ceiling(diff.TotalDays);

            string informationString = "";
            DateTime start = datePickerStartDate;

            foreach (Milestone item in methodologyMilestones)
            {
                informationString += item.Name + ": ~ " + (days * item.Scale / 100) + " nap. Dátum: " + start.AddDays((days * item.Scale / 100)) + Environment.NewLine;
            }

            Information = informationString;
        }

    }
}
