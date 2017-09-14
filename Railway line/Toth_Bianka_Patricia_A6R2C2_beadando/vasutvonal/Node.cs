using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vasutvonal
{
    class Node<T>
    {

        T key;                             // maga az elem
        public T Key
        {
            get { return key; }
            set { key = value; }
        }
        
        Node<T> next;                       // mutató a NEXTre
        public Node<T> Next
        {
            get { return next; }
            set { next = value; }
        }

        public Node(T elem)                 // konstruktor.             itt nem kell a <T>
        {
            this.key = elem;
            next = null;           // ez biztos, hogy ref típusú, mivel Node típusú
        }

        public override string ToString()
        {
            return key.ToString();
        }
    }
}
