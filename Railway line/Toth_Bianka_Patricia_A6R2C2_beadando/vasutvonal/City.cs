using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vasutvonal
{
    class City : Obstacle
    {

        public City(string name, Coordinate starting_point, int width, int height)
            : base(name, starting_point, width, height)
        { }

        public override string ToString()
        {
            return Name + " (" + Width + "*" + Height + "), from: (" + Starting_point.X + "," + Starting_point.Y + ")";
        }
    }
}
