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
using System.Windows.Threading;

namespace frogger
{
    public partial class DeadFrogWindow : Window // ha a béka életet vesztett, de még nincs vége a játéknak
    {
        public DeadFrogWindow()
        {
            InitializeComponent();
        }

        DispatcherTimer timer;
        
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            canvas.Background = new ImageBrush(new BitmapImage(new Uri("deadfrog.png", UriKind.Relative)));

            CenterWindowOnScreen();

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 1);
            timer.Tick += timer_Tick;
            timer.Start();
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

        int i = 1;
        void timer_Tick(object sender, EventArgs e)
        {
            i--;
            if (i == 0)
                this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space || e.Key == Key.Escape)
                this.Close();
        }
    }
}
