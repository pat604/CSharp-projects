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
    class RegistrationWindowViewModel : Bindable
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

        public RegistrationWindowViewModel()
        {
            userManagement = new UserManagement();
            comboBoxSecurityQuestion = userManagement.GetSecurityQuestion();
        }

        public bool RegistrationButtonClick()
        {

            if (textBoxFirstname == null || textBoxFirstname == String.Empty ||
                textBoxLastname == null || textBoxLastname == String.Empty ||
                textBoxEmail == null || textBoxEmail == String.Empty ||
                textBoxPassword == null || textBoxPassword == String.Empty ||
                comboBoxSecurityQuestion == null || comboBoxSecurityQuestion.Count == 0 ||
                textBoxSecurityQuestionAnswer == null || textBoxSecurityQuestionAnswer == String.Empty)
            {
                LabelError = "Minden mező kitöltése kötelező!";
            }
            else
            {

                if (!userManagement.CheckEmail(textBoxEmail))
                {
                    if (userManagement.Registration(textBoxFirstname, textBoxLastname, textBoxEmail, textBoxPassword, selectedSecurityQuestionId, textBoxSecurityQuestionAnswer))
                    {
                        return true;
                    }
                }
                else
                {
                    LabelError = "Ez az e-mail cím már használatban van!";
                }
            }

            return false;

        }

    }
}
