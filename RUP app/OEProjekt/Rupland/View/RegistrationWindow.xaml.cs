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

using CurrentProject.ViewModel;

namespace CurrentProject.View
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {

        RegistrationWindowViewModel RWVM;

        public RegistrationWindow()
        {
            InitializeComponent();
            RWVM = new RegistrationWindowViewModel();
            DataContext = RWVM;
        }

        private void buttonRegistration_Click(object sender, RoutedEventArgs e)
        {
            if (RWVM.RegistrationButtonClick())
            {
                MessageBox.Show("Sikeres regisztráció!", "Rupland - Sikeres regisztráció", MessageBoxButton.OK, MessageBoxImage.Information);
                buttonLogin_Click(sender, e);
            }
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow LW = new LoginWindow();
            LW.Show();
            this.Close();
        }
    }
}
