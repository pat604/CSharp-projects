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
    /// Interaction logic for PasswordRecoveryWindow.xaml
    /// </summary>
    public partial class PasswordRecoveryWindow : Window
    {

        PasswordRecoveryViewModel PRVM;

        public PasswordRecoveryWindow()
        {
            InitializeComponent();
            PRVM = new PasswordRecoveryViewModel();
            DataContext = PRVM;
        }

        private void buttonPasswordRecovery_Click(object sender, RoutedEventArgs e)
        {
            if (PRVM.PasswordRecoveryButtonClick())
            {
                MessageBox.Show("Sikeres jelszómódosítás!\nAz új jelszava: " + PRVM.NewUserPassword, "Sikeres jelszómódosítás", MessageBoxButton.OK, MessageBoxImage.Information);
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
