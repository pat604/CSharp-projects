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
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {

        SettingsViewModel SVM;

        public SettingsWindow()
        {
            InitializeComponent();
            SVM = new SettingsViewModel();
            DataContext = SVM;
        }

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (SVM.UpdateButtonClick())
            {
                MessageBox.Show("Sikeres adatmódosítás!", "Sikeres adatmódosítás", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
            }
        }
    }
}
