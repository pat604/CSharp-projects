using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace frogger
{
    class Fish : Actor
    {
        public Fish(int x, int y, int width, int height, bool rightwards) // nem lehet vele ütközni
            : base(x, y, width, height, rightwards)
        {
        }
    }
}
