using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vasutvonal
{

    enum status { occupied_lake, occupied_mountain, occupied_city, available }  // a koordináta állapota (tó/hegy/város/szabad)

    class Coordinate
    {

        protected int x;
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        protected int y;
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        private status status;
        public status Status
        {
            get { return status; }
            set { status = value; }
        }

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
            Status = status.available;
        }

        public Coordinate()
        {
            Status = status.available;
        }

        public void Reset()
        {
            Status = status.available;
        }

        public override string ToString()
        {
            return "(" + this.x + "," + this.y + ")";
        }

        public override bool Equals(object obj)
        {
            if (obj is Coordinate)
            {
                if (x == (obj as Coordinate).X && y == (obj as Coordinate).Y)
                    return true;
                else return false;
            }
            else
                return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
