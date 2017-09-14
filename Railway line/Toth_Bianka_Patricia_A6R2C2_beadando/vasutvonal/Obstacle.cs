using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vasutvonal
{
    abstract class Obstacle : IOccupiedArea             // akadály osztály
    {

        public Obstacle(string name, Coordinate starting_point, int width, int height)
        {
            this.name = name;
            this.starting_point = starting_point;
            this.height = height;
            this.width = width;
        }        
        
        private string name;
        public string Name
        {
            get { return name; }
        }  

        private Coordinate starting_point;                       // a bal felső sarok
        public Coordinate Starting_point
        {
            get { return starting_point; }
        }  

        private int width;     
        public int Width
        {
            get { return width; }
        }

        private int height;
        public int Height
        {
            get { return height; }
        }

    }
}
