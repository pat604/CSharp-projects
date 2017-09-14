using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vasutvonal
{
    class ListEnumerator<T> : IEnumerator<T>
    {

        Node<T> current;
        Node<T> first;

        public ListEnumerator(Node<T> head)
        {
            first = head;
            current = null;
        }

        public T Current
        {
            get { return current.Key; }
        }

        public void Dispose()
        {
            current = null;
            first = null;
        }

        object System.Collections.IEnumerator.Current
        {
            get { return this.Current; }
        }

        public bool MoveNext()
        {
            if (current == null)
                current = first;
            else
                current = current.Next;

            return current != null;
        }

        public void Reset()
        {
            current = null;
        }
    }
}
