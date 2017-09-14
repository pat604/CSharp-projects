using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace frogger
{
    enum Direction { up, down, right, left }

    class Frog : Creature
    {
        int lives;
        public int Lives
        {
            get { return lives; }
            set { lives = value; }
        }

        public Frog(int x, int y, int width, int height)
            : base(x, y, width, height)
        { }

        public bool IntersectsWith(Actor actor)
        {
            if (this.Area.Y == actor.Area.Y)
                if (this.Area.X <= actor.Area.X + 0.8 &&
                    this.Area.X >= actor.Area.X - 0.8)
                    return true;
                else
                    return false;
            else return false;
        }

        public void BackToOriginal()        // visszahelyezi a békát a kiindulópontra 
        {
            area = new Rect((Game.columns - 1) / 2, (Game.rows - 2), 1, 1);
            OnPropertyChanged("Area");
            OnPropertyChanged("AreaXaml");
        }

        public void Moves(Direction dir)    // mozgatható
        {
            int ujX = (int)area.X;      // sor
            int ujY = (int)area.Y;      // oszlop

            if (dir == Direction.left)
            {
                ujX -= 1;
            }
            else if (dir == Direction.right)
            {
                ujX += 1;
            }
            else if (dir == Direction.up)
            {
                ujY -= 1;
            }
            else if (dir == Direction.down)
            {
                ujY += 1;
            }

            if (ujX >= 0 && ujX <= Game.columns - 1 &&   // pályán belül van
                ujY >= 0 && ujY <= Game.rows - 2)        
            {
                area.X = ujX;
                area.Y = ujY;
                OnPropertyChanged("Area");
                OnPropertyChanged("AreaXaml");
            }
        }
    }
}
