using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vasutvonal
{
    class Field : Coordinate        // mezők, azaz olyan koordináták, amiknek vannak arra vonatkozó értékei, 
                                    // hogy milyen messze állnak a starttól és a céltól
    {
        Field parent;               // a mező szülője, azaz honnan lehet vagy érdemes rálépni
        public Field Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public int F                
        {
            get { return g + h; }
        }
        
        int g;                  // azt adja meg, hogy a starttól az adott mezőig hány pályaelemet kell lefektetni. átló = 14, függ/vízsz. lépés = 10
        public int G
        {
            get { return g; }
            set { g = value; }
        }

        int h;                  // becsült távolság a jelenlegi mezőtől a célig (csak vízszintes és függőleges lépéseket számolva)
        public int H
        {
            get { return h; }
            set { h = value; }
        }

        public Field(Field parent, int x, int y) 
            : base(x, y)
        {
            this.parent = parent;
            this.x = x;
            this.y = y;
        }

        public Field(Field parent, Coordinate C) 
            : base(C.X, C.Y)
        {
            this.parent = parent;
            this.x = C.X;
            this.y = C.Y;
        }

        public Field(Coordinate C)                  // csak a kezdőkoordinátának! (nincs szülője)
            : base(C.X, C.Y)
        {
            this.x = C.X;
            this.y = C.Y;
        }

        public Field(Field parent, int x, int y, int g, int h) 
            : base(x, y)
        {
            this.parent = parent;
            this.x = x;
            this.y = y;
            this.g = parent.G + g;
            this.h = h;
        }

        public override string ToString()
        {
            return "(" + this.x + "," + this.y + "), F:" + this.F + ", G:" + this.g + ", H:" + this.h; 
        }


    }
}
