using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frogger
{
    class Frog : Creature
    {
        int lives;

        public int Lives
        {
            get { return lives; }
            set { lives = value; }
        }
    }
}
