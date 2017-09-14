using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace frogger
{
    abstract class Creature : Bindable  // az állatok ősosztálya = béka, halak, tengeri állatok
    {
        // x, y, width, height.: sor és oszlop, nem pixel

        protected Creature(double x, double y, int width, int height)
        {
            area = new Rect(x, y, width, height);
        }

        protected Rect area;        
        public Rect Area
        {
            get { return area; }
            set { area = value; OnPropertyChanged("Area"); }
        }

        public Rect AreaXaml // az area értékeinek pixelszámmal felszorzott értéke (ez megy a SeaWindow.xaml ablakba)
        {
            get { return new Rect(area.X * Game.p, area.Y * Game.p, area.Width * Game.p, area.Height * Game.p); }
            set { area = new Rect(value.X / Game.p, value.Y / Game.p, value.Width / Game.p, value.Height / Game.p); OnPropertyChanged("AreaXaml"); }
        }

        public override string ToString()
        {
            return this.GetType() + " " + area.X + " " + area.Y + "\n"; ;
        }
    }
}
