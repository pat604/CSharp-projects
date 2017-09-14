using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace frogger
{
    class SeaAnimal : Actor
    {
        public SeaAnimal(int x, int y, int width, int height, bool rightwards) // ütközni lehet vele
            : base(x, y, width, height, rightwards)
        {
        }
    }
}
