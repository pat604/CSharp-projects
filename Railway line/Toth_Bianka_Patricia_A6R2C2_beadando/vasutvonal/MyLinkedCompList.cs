using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vasutvonal
{
    class MyLinkedCompList<T> : MyLinkedList<T>, IComparable where T: IComparable
    {

        public int CompareTo(object other)
        {
            return head.Key.CompareTo((other as MyLinkedCompList<T>).head.Key);
        }

        public T GetFirst()
        {
            return head.Key;
        }

    }
}
