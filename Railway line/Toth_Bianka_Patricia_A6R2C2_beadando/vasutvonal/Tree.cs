
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vasutvonal
{
    class Tree<T> : IEnumerable<T> where T : IComparable
    {

        TreeNode<T> root;

        public Tree()
        {
            root = null;
        }

        public void Add(T new_value)
        {
            Add(ref root, new_value);
        }

        private void Add(ref TreeNode<T> current_root, T new_value)
        {
            if (current_root == null)
                current_root = new TreeNode<T>(new_value);
            else
            {
                if (current_root.Key.CompareTo(new_value) < 0)      // az új elem nagyobb, mint a gyökér
                    Add(ref current_root.right, new_value);
                else                                                // ha kisebb v egyenlő
                    Add(ref current_root.left, new_value);
            }
        }

        private void InOrder(List<T> l, TreeNode<T> n)
        {
            if (n != null)
            {
                InOrder(l, n.left);
                l.Add(n.Key);
                InOrder(l, n.right);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            List<T> l = new List<T>();
            InOrder(l, root);
            return l.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

}
