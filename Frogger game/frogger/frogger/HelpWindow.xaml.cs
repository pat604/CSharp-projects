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

namespace frogger
{
    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            canvasfrogger.Background = new ImageBrush(new BitmapImage(new Uri("froggertitle.png", UriKind.Relative)));
            canvashelp.Background = new ImageBrush(new BitmapImage(new Uri("froggerhelp.png", UriKind.Relative)));
            
            textblock.Text =
            "your little frog doesn't wanna be eaten by some ugly sea animal. \n" +
            "help him to reach the top of the sea without colliding with any of the sea animals! \n" +
            "don't worry, all the fish are your friends. \n \n" +
            "your little frog has 30 seconds to reach the surface. \n" +
            "the sea animals are moving fast, the fish are moving slowly. \n \n" +       
            "the more lives and remaining time you have, the more score you get. \n" +
            "good luck!";


            CenterWindowOnScreen();
        }

        private void CenterWindowOnScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}
