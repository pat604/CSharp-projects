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
    /// Interaction logic for NewMemberWindow.xaml
    /// </summary>
    public partial class NewMemberWindow : Window
    {

        NewMemberViewModel NMVW;

        public NewMemberWindow(int projectId)
        {
            InitializeComponent();
            NMVW = new NewMemberViewModel(projectId);
            DataContext = NMVW;
        }

        private void buttonNewMember_Click(object sender, RoutedEventArgs e)
        {
            if (NMVW.NewMemberButtonClick())
            {
                DialogResult = true;
            }
        }
    }
}
