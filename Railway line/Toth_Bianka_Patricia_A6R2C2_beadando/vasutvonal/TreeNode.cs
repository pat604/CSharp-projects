using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vasutvonal
{
    class TreeNode<T>
    {
        T key;
        public T Key
        {
            get { return key; }
            set { key = value; }
        }

        public TreeNode<T> left;        
        public TreeNode<T> right;

        public TreeNode(T value)
        {
            this.key = value;
            left = null;
            right = null;
        }

        public override string ToString()
        {
            return key.ToString();               
        }
    }
}
