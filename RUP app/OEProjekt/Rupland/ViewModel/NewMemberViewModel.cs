using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CurrentProject.Class;
using CurrentProject.Database;

namespace CurrentProject.ViewModel
{
    class NewMemberViewModel : Bindable
    {

        ProjectManagement projectManagement;

        private int projectId;

        private string textBoxEmail;

        public string TextBoxEmail
        {
            get { return textBoxEmail; }
            set { textBoxEmail = value; OnPropertyChanged(); }
        }

        private List<Role> comboBoxRole;

        public List<Role> ComboBoxRole
        {
            get { return comboBoxRole; }
            set { comboBoxRole = value; OnPropertyChanged(); }
        }

        private int selectedRoleId = 1;

        public int SelectedRoleId
        {
            get { return selectedRoleId; }
            set { selectedRoleId = value; OnPropertyChanged(); }
        }

        private string labelError;

        public string LabelError
        {
            get { return labelError; }
            set { labelError = value; OnPropertyChanged(); }
        }

        public NewMemberViewModel(int projectId)
        {
            this.projectId = projectId;
            projectManagement = new ProjectManagement();
            ComboBoxRole = projectManagement.GetRole();
        }

        public bool NewMemberButtonClick()
        {
            if (textBoxEmail == null || textBoxEmail == String.Empty)
            {
                LabelError = "A mező kitöltése kötelező!";
            }
            else
            {

                int userId = projectManagement.CheckNewMemberData(textBoxEmail, projectId);

                if (userId > 0)
                {
                    projectManagement.NewProjectMember(projectId, userId, selectedRoleId);
                    return true;
                }
                else
                {
                    LabelError = "Nem található ilyen e-mail cím, vagy már aktív résztvevő!";
                }
            }

            return false;
        }

    }
}
