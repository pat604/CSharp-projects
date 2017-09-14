using System;
using System.Collections.Generic;
using System.Linq;
using CurrentProject.ViewModel;
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

namespace CurrentProject.View
{
    public partial class NewTaskWindow : Window
    {
        NewTaskViewModel NTVW;

        public NewTaskWindow(int projectid)
        {
            InitializeComponent();
            NTVW = new NewTaskViewModel(projectid);
            DataContext = NTVW;
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            if (NTVW.OKButtonClick())
            {
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
