using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace frogger
{
    abstract class Actor : Creature     // a nem-béka szereplők ősosztálya = halak és tengeri állatok őse
    {
        bool rightwards;
        public bool Rightwards
        {
            get { return rightwards; }
        }

        protected Actor(double x, double y, int width, int height, bool rightwards)
            :base (x, y, width, height)
        {
            this.rightwards = rightwards;
        }

        public void MovingFast()
        {
            if (rightwards)
            {
                if (area.X < Game.columns)
                    area.X += 0.17;
                else
                    area.X = -1;
            }
            else
            {
                if (area.X > -1)
                    area.X -= 0.17;
                else
                    area.X = Game.columns;
            }           
            OnPropertyChanged("Area");
            OnPropertyChanged("AreaXaml");
        }

        public void MovingSlowly()
        {
            if (rightwards)
            {
                if (area.X < Game.columns)
                    area.X += 0.08;
                else
                    area.X = -1;
            }
            else
            {
                if (area.X > -1)
                    area.X -= 0.08;
                else
                    area.X = Game.columns;
            }
            OnPropertyChanged("Area");
            OnPropertyChanged("AreaXaml");
        }
    }
}
