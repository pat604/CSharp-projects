using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class SeaWindow : Window
    {
        Game game;
        DispatcherTimer timer;      // az állatok mozgásához
        DispatcherTimer timer2;     // a fennmaradó időhöz

        int timetosolve = 30;       // egy pálya megoldásához adott idő
        int remainingtime;          // a fennmaradó idő (timer által egyre csökken)

        int score;
        int highscore;

        Brush charactercolour;      // a feliratok színe

        public SeaWindow()  // akkor hívódik meg, ha az első szint töltődik be  
        {
            game = new Game(1);
            game.Frog.Lives = 3;

            score = 0;

            this.Width = game.ColumnsXaml;
            this.Height = game.RowsXaml;
            CenterWindowOnScreen();

            InitializeComponent();
        }

        public SeaWindow(int level, int lives, int score) // akkor hívódik meg, ha nem az első szint töltődik be (szintlépés)
        {
            this.score = score;

            game = new Game(level);

            game.Frog.Lives = lives;
            game.Frog.Lives++;      // szintlépéssel a béka kap egy plusz életet

            this.Width = game.ColumnsXaml;
            this.Height = game.RowsXaml;
            CenterWindowOnScreen();

            InitializeComponent();
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
            canvas.DataContext = game;
            canvas.Background = new ImageBrush(new BitmapImage(new Uri("seabottom.jpg", UriKind.Relative)));

            froglife1.Background = new ImageBrush(new BitmapImage(new Uri("froglives.png", UriKind.Relative)));
            froglife2.Background = new ImageBrush(new BitmapImage(new Uri("froglives.png", UriKind.Relative)));
            froglife3.Background = new ImageBrush(new BitmapImage(new Uri("froglives.png", UriKind.Relative)));
            froglife4.Background = new ImageBrush(new BitmapImage(new Uri("froglives.png", UriKind.Relative)));
            froglife5.Background = new ImageBrush(new BitmapImage(new Uri("froglives.png", UriKind.Relative)));
            froglife6.Background = new ImageBrush(new BitmapImage(new Uri("froglives.png", UriKind.Relative)));
            froglife7.Background = new ImageBrush(new BitmapImage(new Uri("froglives.png", UriKind.Relative)));

            remainingtime = timetosolve;
            ReadingHighScore();

            charactercolour = labeltime.Foreground;

            labellevel.Content = "l e v e l  " + game.Level.ToString();
            labelhighscore.Content = "h i g h  s c o r e :  " + highscore.ToString();
            labelscore.Content = "s c o r e :  " + score.ToString();
            labeltime.Content = "t i m e :  " + remainingtime.ToString();

            if (game.Level != 5)
                labelgoal.Visibility = Visibility.Hidden;

            AddFrog();
            AddFishes();
            AddSeaAnimals();

            FrogLives();

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            timer.Tick += timer_Tick;
            timer.Start();

            timer2 = new DispatcherTimer();
            timer2.Interval = new TimeSpan(0, 0, 0, 1, 0);
            timer2.Tick += timer2_Tick;
            timer2.Start();
        }

        private void timer_Tick(object sender, EventArgs e) // állatok mozgatása + az ütközés, a szintlépés és a játék végének vizsgálata
        {
            foreach (SeaAnimal seaanimal in game.Seaanimals)
            {
                // MessageBox.Show(seanimal.ToString() + game.Frog.ToString());         // ütközés ellenőrzésére

                seaanimal.MovingFast();

                if (game.Frog.IntersectsWith(seaanimal))
                {
                    timer2.Stop();

                    game.Frog.Lives--;
                    FrogLives();

                    if (game.Frog.Lives != 0)
                    {
                        DeadFrogWindow w = new DeadFrogWindow();
                        w.Show();

                        remainingtime = timetosolve;
                        labeltime.Content = "t i m e :  " + remainingtime.ToString();
                        timer2.Start();

                        game.Frog.BackToOriginal();
                    }

                    else // if (game.Frog.Lives == 0)
                    {
                        timer.Stop();

                        score += AddScore();
                        score += AddLevelScore(); // csak akkor adom hozzá a szint szerinti pontot is, ha a játéknak vége van
                        
                        bool newhighscore = false;  // leellenőrzöm, hogy született-e új high score. ha igen, módosítom a txt-t
                        if (score > highscore)
                        {
                            highscore = score;
                            ModifyHighScore();
                            newhighscore = true;
                        }

                        EndWindow window = new EndWindow(false, newhighscore);
                        window.Show();
                        this.Close();
                    }
                }
            }

            foreach (Fish fish in game.Fishes)
            {
                fish.MovingSlowly();
            }

            if (game.Frog.Area.Y == 0) // leellenőrzi, hogy a béka elérte-e a legfölső sort, azaz szintet léphet / győzött
            {
                game.End = true;
                timer.Stop();
                timer2.Stop();

                score += AddScore();

                game.Level++;       // szintet lép
                if (game.Level <= 5)
                {
                    SeaWindow window = new SeaWindow(game.Level, game.Frog.Lives, score);
                    window.Show();
                    this.Close();
                }
                else // if (game.Level == 6), azaz vége, nyert
                {
                    score += AddLevelScore();
                    bool newhighscore = false;
                    if (score > highscore)
                    {
                        newhighscore = true;
                        highscore = score;
                        ModifyHighScore();
                    }

                    YouWinWindow window = new YouWinWindow(score, newhighscore);
                    window.Show();
                    this.Close();
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)  // a fennmaradó időt számolja 
        {
            remainingtime--;
            labeltime.Content = "t i m e :  " + remainingtime.ToString();         

            if (remainingtime <= 10)
                labeltime.Foreground = Brushes.Red;
            else
                labeltime.Foreground = charactercolour;

            if (remainingtime == 0)
            {
                labeltime.Foreground = charactercolour;
                
                game.Frog.Lives--;
                FrogLives();

                if (game.Frog.Lives != 0) // ha lejárt az idő a béka megfullad. ha még maradt élete, a deadfrogwindow töltődik be
                {
                    timer2.Stop();
  
                    DeadFrogWindow w = new DeadFrogWindow();
                    w.Show();

                    remainingtime = timetosolve;
                    labeltime.Content = "t i m e :  " + remainingtime.ToString();
                    timer2.Start();

                    game.Frog.BackToOriginal();
                }
                else // ha a béka az összes életét elvesztette
                {
                    timer.Stop();
                    timer2.Stop();
                    game.End = true;

                    bool newhighscore = false;  // új high score leellenőrzése
                    score += AddScore();
                    score += AddLevelScore();
                    if (score > highscore)
                    {
                        highscore = score;
                        ModifyHighScore();
                        newhighscore = true;
                    }

                    EndWindow window = new EndWindow(true, newhighscore);  // játék vége
                    window.Show();
                    this.Close();
                }
            }
        }

        private void FrogLives() // a békaéletek (alsó sáv a xaml-ben) láthatóságát állítja be 
        {
            if (game.Frog.Lives == 7)
            {
                froglife7.Visibility = Visibility.Visible;
                froglife6.Visibility = Visibility.Visible;
                froglife5.Visibility = Visibility.Visible;
                froglife4.Visibility = Visibility.Visible;
            }
            else if (game.Frog.Lives == 6)
            {
                froglife7.Visibility = Visibility.Hidden;
                froglife6.Visibility = Visibility.Visible;
                froglife5.Visibility = Visibility.Visible;
                froglife4.Visibility = Visibility.Visible;
            }
            else if (game.Frog.Lives == 5)
            {
                froglife6.Visibility = Visibility.Hidden;
                froglife5.Visibility = Visibility.Visible;
                froglife4.Visibility = Visibility.Visible;
            }
            else if (game.Frog.Lives == 4)
            {
                froglife5.Visibility = Visibility.Hidden;
                froglife4.Visibility = Visibility.Visible;
            }
            else if (game.Frog.Lives == 3)
            {
                froglife4.Visibility = Visibility.Hidden;
                froglife3.Visibility = Visibility.Visible;
            }
            else if (game.Frog.Lives == 2)
            {
                froglife3.Visibility = Visibility.Hidden;
                froglife2.Visibility = Visibility.Visible;
            }
            else if (game.Frog.Lives == 1)
            {
                froglife2.Visibility = Visibility.Hidden;
                froglife1.Visibility = Visibility.Visible;
            }
            else if (game.Frog.Lives == 0)
            {
                froglife1.Visibility = Visibility.Hidden;
            }
        }

        private int AddScore() // a fennmaradó időből és az életekből számolt pontszám, amit a játékos minden szintlépéskor megkap 
        {
            return remainingtime * 100 + game.Frog.Lives * 50;
        }

        private int AddLevelScore() // a játék véget érésekor elért szint által meghatározott pontszám 
        {
            return game.Level * 200;
        }

        private void ReadingHighScore() // beolvassa a highscore.txt nevű fájlt 
        {
            StreamReader sw = new StreamReader("highscore.txt");
            int number = 0;
            while (!sw.EndOfStream)
                number = Int32.Parse(sw.ReadLine());
            sw.Close();
            highscore = number;
        }

        private void ModifyHighScore() // új high score esetén átírja a highscore.txt nevű fájlt 
        {
            File.Delete("highscore.txt");

            StreamWriter sw = new StreamWriter("highscore.txt");
            sw.Write(highscore);
            sw.Close();
        }

        Random rand = new Random();

        private void AddFrog() // béka hozzáadása 
        {
            Rectangle newfrog = new Rectangle();
            newfrog.Fill = new ImageBrush(new BitmapImage(new Uri("frog4.png", UriKind.Relative)));
            Canvas.SetZIndex(newfrog, (int)99);
            newfrog.DataContext = game.Frog;

            Binding widthbinding = new Binding("AreaXaml.Width");
            widthbinding.Source = game.Frog;
            newfrog.SetBinding(Rectangle.WidthProperty, widthbinding);

            Binding heightbinding = new Binding("AreaXaml.Height");
            heightbinding.Source = game.Frog;
            newfrog.SetBinding(Rectangle.HeightProperty, heightbinding);

            Binding xbinding = new Binding("AreaXaml.X");
            xbinding.Source = game.Frog;
            newfrog.SetBinding(Canvas.LeftProperty, xbinding);

            Binding ybinding = new Binding("AreaXaml.Y");
            ybinding.Source = game.Frog;
            newfrog.SetBinding(Canvas.TopProperty, ybinding);

            canvas.Children.Add(newfrog);
        }

        private void AddFishes() // a halak hozzáadása 
        {
            foreach (Fish fish in game.Fishes)
            {
                Rectangle newfish = new Rectangle();

                int d = rand.Next(1, 62);
                if (fish.Rightwards == true)
                {
                    if (d <= 11)
                        newfish.Fill = new ImageBrush(new BitmapImage(new Uri("fish1right.png", UriKind.Relative)));
                    else if (d > 11 && d <= 21)
                        newfish.Fill = new ImageBrush(new BitmapImage(new Uri("fish2right.png", UriKind.Relative)));
                    else if (d > 21 && d <= 31)
                        newfish.Fill = new ImageBrush(new BitmapImage(new Uri("fish3right.png", UriKind.Relative)));
                    else if (d > 31 && d <= 41)
                        newfish.Fill = new ImageBrush(new BitmapImage(new Uri("fish4right.png", UriKind.Relative)));
                    else if (d > 41 && d <= 51)
                        newfish.Fill = new ImageBrush(new BitmapImage(new Uri("fish5right.png", UriKind.Relative)));
                    else if (d <= 61)
                        newfish.Fill = new ImageBrush(new BitmapImage(new Uri("fish6right.png", UriKind.Relative)));
                }
                else
                {
                    if (d <= 11)
                        newfish.Fill = new ImageBrush(new BitmapImage(new Uri("fish1left.png", UriKind.Relative)));
                    else if (d > 11 && d <= 21)
                        newfish.Fill = new ImageBrush(new BitmapImage(new Uri("fish2left.png", UriKind.Relative)));
                    else if (d > 21 && d <= 31)
                        newfish.Fill = new ImageBrush(new BitmapImage(new Uri("fish3left.png", UriKind.Relative)));
                    else if (d > 31 && d <= 41)
                        newfish.Fill = new ImageBrush(new BitmapImage(new Uri("fish4left.png", UriKind.Relative)));
                    else if (d > 41 && d <= 51)
                        newfish.Fill = new ImageBrush(new BitmapImage(new Uri("fish5left.png", UriKind.Relative)));
                    else if (d <= 61)
                        newfish.Fill = new ImageBrush(new BitmapImage(new Uri("fish6left.png", UriKind.Relative)));
                }

                newfish.DataContext = fish;

                Binding widthbinding = new Binding("AreaXaml.Width");
                widthbinding.Source = fish;
                newfish.SetBinding(Rectangle.WidthProperty, widthbinding);

                Binding heightbinding = new Binding("AreaXaml.Height");
                heightbinding.Source = fish;
                newfish.SetBinding(Rectangle.HeightProperty, heightbinding);

                Binding xbinding = new Binding("AreaXaml.X");
                xbinding.Source = fish;
                newfish.SetBinding(Canvas.LeftProperty, xbinding);

                Binding ybinding = new Binding("AreaXaml.Y");
                ybinding.Source = fish;
                newfish.SetBinding(Canvas.TopProperty, ybinding);

                canvas.Children.Add(newfish);
            }
        }

        private void AddSeaAnimals() // a tengeri állatok hozzáadása 
        {
            foreach (SeaAnimal seaanimal in game.Seaanimals)
            {
                Rectangle newseaanimal = new Rectangle();

                int d = rand.Next(1, 52);
                if (seaanimal.Rightwards == true)
                {
                    if (d <= 11)
                        newseaanimal.Fill = new ImageBrush(new BitmapImage(new Uri("crab1right.png", UriKind.Relative)));
                    else if (d > 11 && d <= 21)
                        newseaanimal.Fill = new ImageBrush(new BitmapImage(new Uri("crab2right.png", UriKind.Relative)));
                    else if (d > 21 && d <= 31)
                        newseaanimal.Fill = new ImageBrush(new BitmapImage(new Uri("snake1right.png", UriKind.Relative)));
                    else if (d > 31 && d <= 45)
                        newseaanimal.Fill = new ImageBrush(new BitmapImage(new Uri("snake2right.png", UriKind.Relative)));
                    else if (d > 45 && d <= 51)
                        newseaanimal.Fill = new ImageBrush(new BitmapImage(new Uri("rayright.png", UriKind.Relative)));
                }

                else
                {
                    if (d <= 11)
                        newseaanimal.Fill = new ImageBrush(new BitmapImage(new Uri("crab1left.png", UriKind.Relative)));
                    else if (d > 11 && d <= 21)
                        newseaanimal.Fill = new ImageBrush(new BitmapImage(new Uri("crab2left.png", UriKind.Relative)));
                    else if (d > 21 && d <= 31)
                        newseaanimal.Fill = new ImageBrush(new BitmapImage(new Uri("snake1left.png", UriKind.Relative)));
                    else if (d > 31 && d <= 41)
                        newseaanimal.Fill = new ImageBrush(new BitmapImage(new Uri("snake2left.png", UriKind.Relative)));
                    else if (d > 41 && d <= 51)
                        newseaanimal.Fill = new ImageBrush(new BitmapImage(new Uri("rayleft.png", UriKind.Relative)));
                }


                newseaanimal.DataContext = seaanimal;

                Binding widthbinding = new Binding("AreaXaml.Width");
                widthbinding.Source = seaanimal;
                newseaanimal.SetBinding(Rectangle.WidthProperty, widthbinding);

                Binding heightbinding = new Binding("AreaXaml.Height");
                heightbinding.Source = seaanimal;
                newseaanimal.SetBinding(Rectangle.HeightProperty, heightbinding);

                Binding xbinding = new Binding("AreaXaml.X");
                xbinding.Source = seaanimal;
                newseaanimal.SetBinding(Canvas.LeftProperty, xbinding);

                Binding ybinding = new Binding("AreaXaml.Y");
                ybinding.Source = seaanimal;
                newseaanimal.SetBinding(Canvas.TopProperty, ybinding);

                canvas.Children.Add(newseaanimal);
            }
        }

        private void Window_PreviewKeyDown_1(object sender, KeyEventArgs e) // a béka mozgatása
        {
            if (!game.End)
            {
                if (e.Key == Key.Left)
                {
                    game.FrogMoves(Direction.left);
                }
                else if (e.Key == Key.Right)
                {
                    game.FrogMoves(Direction.right);
                }
                else if (e.Key == Key.Up)
                {
                    game.FrogMoves(Direction.up);
                }
                else if (e.Key == Key.Down)
                {
                    game.FrogMoves(Direction.down);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) 
        {
            this.Close();
        }

    }
}
