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
using System.Windows.Navigation;
using System.Windows.Shapes;

using CurrentProject.ViewModel;

namespace CurrentProject.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        LoginWindowViewModel LWVM;

        public LoginWindow()
        {
            InitializeComponent();
            LWVM = new LoginWindowViewModel();
            DataContext = LWVM;
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (LWVM.LoginButtonClick())
            {
                MainWindow MW = new MainWindow();
                MW.Show();
                this.Close();
            }
        }

        private void buttonRegistration_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow RW = new RegistrationWindow();
            RW.Show();
            this.Close();
        }

        private void buttonPasswordRecovery_Click(object sender, RoutedEventArgs e)
        {
            PasswordRecoveryWindow PRW = new PasswordRecoveryWindow();
            PRW.Show();
            this.Close();
        }
    }
}
