using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vasutvonal
{

    public enum Type { single_tracked, double_tracked, electrified_single, electrified_double, TGVcompatible }

    class Track : IComparable       // pályaelem osztály
    {

        Type type;                  // a pályaelem típusa
        public Type Type
        {
            get { return type; }
        }

        public Track(Type type)
        {
            this.type = type;     
        }

        public Track() { }

        public int Cost()
        {
            switch (type)
            {
                case Type.single_tracked:
                    return 100;
                case Type.double_tracked:
                    return 200;
                case Type.electrified_single:
                    return 300;
                case Type.electrified_double:
                    return 400;
                case Type.TGVcompatible:
                    return 500;
                default:                // a default eset nem fordulhat elő, mert az összes típust felsoroltam
                    return 0;
            }
        }       // a pályaelem költsége

        public int CompareTo(object obj)
        {
            return Cost().CompareTo((obj as Track).Cost());
        }

        public override string ToString()
        {
            int cost = Cost();
            return Type.ToString();
        }
    }
}
