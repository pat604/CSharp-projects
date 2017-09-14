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

using CurrentProject.Class;
using CurrentProject.ViewModel;

namespace CurrentProject.View
{
    /// <summary>
    /// Interaction logic for NewProjectWindow.xaml
    /// </summary>
    public partial class NewProjectWindow : Window
    {

        NewProjectViewModel NPVM;

        public NewProjectWindow()
        {
            InitializeComponent();
            NPVM = new NewProjectViewModel();
            DataContext = NPVM;
        }

        private void buttonNewProject_Click(object sender, RoutedEventArgs e)
        {
            if (NPVM.NewProjectButtonClick())
            {
                DialogResult = true;
            }
        }
    }
}
