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
    public partial class EndWindow : Window // ha vége van a játéknak (elfogyott az idő vagy az életek)
    {
        public EndWindow(bool drowned, bool newhighscore)
        {
            this.drowned = drowned;        
            this.newhighscore = newhighscore;

            InitializeComponent();
        }

        bool drowned;               // ha az idő fogyott el
        bool newhighscore;

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            CenterWindowOnScreen();

            canvas.Background = new ImageBrush(new BitmapImage(new Uri("theend.png", UriKind.Relative)));

            if (!drowned)
                labeldrowned.Visibility = Visibility.Hidden;

            if (!newhighscore)
                labelnewhighscore.Visibility = Visibility.Hidden;
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
            SeaWindow window = new SeaWindow();
            window.Show();
            this.Close();
        } 

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }  

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SeaWindow window = new SeaWindow();
                window.Show();
                this.Close();
            }
            else if (e.Key == Key.Escape)
                this.Close();
        }
    }
}
