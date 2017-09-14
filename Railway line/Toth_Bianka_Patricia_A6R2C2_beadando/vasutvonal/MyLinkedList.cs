using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace vasutvonal
{
    class MyLinkedList<T> : IEnumerable<T> 
    {

        protected Node<T> head;

        public MyLinkedList()
        {
            head = null;
        }

        public void Add(T key)                              // lista végére 
        {
            Node<T> new_key = new Node<T>(key);

            if (head == null)
                head = new_key;
            else
            {
                Node<T> p = head;
                while (p.Next != null)
                    p = p.Next;
                p.Next = new_key;
            }
        }

        public bool Delete(T value)                         // a legelső előfordulást törli, ha egyáltalán benn van 
        {
            Node<T> p = head;           // aktuális elem
            Node<T> e = null;           // előző elem

            while (p != null && !p.Key.Equals(value))   // == és != operandust nem lehet használni!
            {
                e = p;
                p = p.Next;
            }

            if (p != null)
            {
                if (e == null)              // első elem
                    head = head.Next;
                else
                    e.Next = p.Next;        // nem fog referencia mutatni az elemre, így a GC felszabadítja, amint már p sem mutat rá
                return true;
            }
            else                            // nem találtuk meg
                return false;
        }

        public T Find(T what, out bool found)
        {
            Node<T> current = head;
            while (current != null && !current.Key.Equals(what))
                current = current.Next;

            if (current != null)
            {
                found = true;
                return current.Key;
            }
            else
            {
                found = false;
                return default(T);
            }
        }

        public T FindByIndex(int index, out bool found)
        {
            Node<T> current = head;
            int i = 0;

            while (current != null && i < index)
                current = current.Next;

            if (current != null)
            {
                found = true;
                return current.Key;
            }
            else
            {
                found = false;
                return default(T);
            }
        }

        // foreach-hez
        public IEnumerator<T> GetEnumerator()
        {
            return new ListEnumerator<T>(head);
        }

        IEnumerator IEnumerable.GetEnumerator()         // using System.Collections 
        {
            return this.GetEnumerator();
        }

    }
}
