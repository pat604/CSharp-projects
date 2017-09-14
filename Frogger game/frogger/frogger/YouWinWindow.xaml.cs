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
    public partial class YouWinWindow : Window      // ha a béka nyert, azaz eljutott az 5. szint végéig
    {
        public YouWinWindow(int score, bool newhighscore)
        {
            this.score = score;
            this.newhighscore = newhighscore;

            InitializeComponent();
        }

        int score;
        bool newhighscore;

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            CenterWindowOnScreen();

            canvas.Background = new ImageBrush(new BitmapImage(new Uri("frogwins.png", UriKind.Relative)));

            labeltext.Content = "congratulations, you won! \n" +
                "       your score is " + score.ToString() + ".";
            if (newhighscore)
                labeltext.Content += "\nNEW HIGH SCORE!";

            labeltextfrog.Foreground = Brushes.LightGreen;
            labeltextfrog.Content = "thank you, my friend!";
                
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
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SeaWindow window = new SeaWindow();
            window.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
