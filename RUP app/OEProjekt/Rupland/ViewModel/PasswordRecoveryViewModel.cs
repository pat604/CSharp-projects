using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CurrentProject.Class;
using CurrentProject.Database;

namespace CurrentProject.ViewModel
{
    class PasswordRecoveryViewModel : Bindable
    {

        UserManagement userManagement;

        private string textBoxEmail;

        public string TextBoxEmail
        {
            get { return textBoxEmail; }
            set { textBoxEmail = value; OnPropertyChanged(); }
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

        private string newUserPassword;

        public string NewUserPassword
        {
            get { return newUserPassword; }
        }

        private string labelError;

        public string LabelError
        {
            get { return labelError; }
            set { labelError = value; OnPropertyChanged(); }
        }

        public PasswordRecoveryViewModel()
        {
            userManagement = new UserManagement();
            comboBoxSecurityQuestion = userManagement.GetSecurityQuestion();
        }

        public bool PasswordRecoveryButtonClick()
        {
            if (textBoxEmail == null || textBoxEmail == String.Empty ||
               comboBoxSecurityQuestion == null || comboBoxSecurityQuestion.Count == 0 ||
               textBoxSecurityQuestionAnswer == null || textBoxSecurityQuestionAnswer == String.Empty)
            {
                LabelError = "Minden mező kitöltése kötelező!";
            }
            else
            {
                if (userManagement.PasswordRecoveryCheck(textBoxEmail, selectedSecurityQuestionId, textBoxSecurityQuestionAnswer))
                {
                    newUserPassword = userManagement.PasswordReset(textBoxEmail);
                    return true;
                }
                else
                {
                    LabelError = "A megadott adatok nem megfelelőek!";
                }
            }

            return false;
        }

    }
}
