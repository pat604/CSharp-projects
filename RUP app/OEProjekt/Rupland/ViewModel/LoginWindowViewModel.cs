using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CurrentProject.Class;

namespace CurrentProject.ViewModel
{
    class LoginWindowViewModel : Bindable
    {

        UserManagement userManagement;

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

        private string labelError;

        public string LabelError
        {
            get { return labelError; }
            set { labelError = value; OnPropertyChanged(); }
        }

        public LoginWindowViewModel()
        {
            userManagement = new UserManagement();
            userManagement.CheckEmail(""); // hogy gyorsabb legyen az "első" kapcsolat, kell egy akármilyen lekérdezés
        }

        public bool LoginButtonClick()
        {

            if (textBoxEmail == null || textBoxEmail == String.Empty || 
                textBoxPassword == null || textBoxPassword == String.Empty)
            {
                LabelError = "Minden mező kitöltése kötelező!";
            }
            else
            {

                if (userManagement.Login(textBoxEmail, textBoxPassword))
                {
                    return true;
                }
                else
                {
                    LabelError = "Sikertelen bejelentkezés!";
                }

            }

            return false;

        }

    }
}
