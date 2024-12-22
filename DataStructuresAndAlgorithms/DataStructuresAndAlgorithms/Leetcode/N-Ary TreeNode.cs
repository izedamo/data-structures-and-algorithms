using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms.Leetcode
{
    public class NAryNode
    {
        public int val;
        public IList<NAryNode> children;

        public NAryNode() { }

        public NAryNode(int _val)
        {
            val = _val;
        }

        public NAryNode(int _val, IList<NAryNode> _children)
        {
            val = _val;
            children = _children;
        }
    }
}
