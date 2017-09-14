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

namespace frogger
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {         
            InitializeComponent();

            //YouWinWindow w = new YouWinWindow(0, true);
            //w.Show();
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

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            CenterWindowOnScreen();

            canvasfrog.Background = new ImageBrush(new BitmapImage(new Uri("frogg.png", UriKind.Relative)));
            canvastitle.Background = new ImageBrush(new BitmapImage(new Uri("froggertitle.png", UriKind.Relative)));
            canvastitle2.Background = new ImageBrush(new BitmapImage(new Uri("froggertitle2.png", UriKind.Relative)));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SeaWindow window = new SeaWindow();
            window.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {       
            HelpWindow window = new HelpWindow();
            window.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
