using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CurrentProject.Class;
using CurrentProject.Database;

namespace CurrentProject.ViewModel
{
    class SettingsViewModel : Bindable
    {

        UserManagement userManagement;

        private string textBoxFirstname;

        public string TextBoxFirstname
        {
            get { return textBoxFirstname; }
            set { textBoxFirstname = value; OnPropertyChanged(); }
        }

        private string textBoxLastname;

        public string TextBoxLastname
        {
            get { return textBoxLastname; }
            set { textBoxLastname = value; OnPropertyChanged(); }
        }

        private string textBoxEmail;

        public string TextBoxEmail
        {
            get { return textBoxEmail; }
            set { textBoxEmail = value; OnPropertyChanged(); }
        }

        private string textBoxPassword;

        public string TextBoxPassword
        {
            get { return textBoxPassword; }
            set { textBoxPassword = value; OnPropertyChanged(); }
        }

        private List<SecurityQuestion> comboBoxSecurityQuestion;

        public List<SecurityQuestion> ComboBoxSecurityQuestion
        {
            get { return comboBoxSecurityQuestion; }
        }

        private int selectedSecurityQuestionId = 1;

        public int SelectedSecurityQuestionId
        {
            get { return selectedSecurityQuestionId; }
            set { selectedSecurityQuestionId = value; OnPropertyChanged(); }
        }

        private string textBoxSecurityQuestionAnswer;

        public string TextBoxSecurityQuestionAnswer
        {
            get { return textBoxSecurityQuestionAnswer; }
            set { textBoxSecurityQuestionAnswer = value; OnPropertyChanged(); }
        }

        private string labelError;

        public string LabelError
        {
            get { return labelError; }
            set { labelError = value; OnPropertyChanged(); }
        }

        public SettingsViewModel()
        {
            userManagement = new UserManagement();
            comboBoxSecurityQuestion = userManagement.GetSecurityQuestion();

            User currentUser = userManagement.CurrentUser(Session.Get<int>("U_Id"));

            TextBoxFirstname = currentUser.Firstname;
            TextBoxLastname = currentUser.Lastname;
            TextBoxEmail = currentUser.Email;
            SelectedSecurityQuestionId = currentUser.SecurityQuestionId;
            TextBoxSecurityQuestionAnswer = currentUser.SecurityQuestionAnswer;
        }

        public bool UpdateButtonClick()
        {

            if (comboBoxSecurityQuestion == null || comboBoxSecurityQuestion.Count == 0 ||
                textBoxSecurityQuestionAnswer == null || textBoxSecurityQuestionAnswer == String.Empty)
            {
                LabelError = "Minden mező kitöltése kötelező!";
            }
            else
            {

                bool res = false;

                if (textBoxPassword != null || textBoxPassword != String.Empty)
                {
                    res = userManagement.UpdateUserData(textBoxFirstname, textBoxLastname, selectedSecurityQuestionId, textBoxSecurityQuestionAnswer, textBoxPassword);
                }
                else
                {
                    res = userManagement.UpdateUserData(textBoxFirstname, textBoxLastname, selectedSecurityQuestionId, textBoxSecurityQuestionAnswer);
                }

                return res;

            }

            return false;

        }

    }
}
