using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms.DataStructures.Trees
{
    public class BSTNode
    {
        public int Data { get; set; }

        public BSTNode Right { get; set; }

        public BSTNode Left { get; set; }
    }

    //Does not support duplicate nodes.
    public class BinarySearchTree
    {
        public BSTNode Root { get; set; }

        public BinarySearchTree()
        {
            Root = null;
        }

        //Create a new node with passed value in tree acc. to BST criteria.
        public void Insert(int value)
        {
            var newNode = new BSTNode
            {
                Data = value
            };

            //Set new node as root node if tree is empty.
            if (Root == null)
                Root = newNode;
            else
            {
                var node = Root;
                BSTNode parent = null;

                //Find the new node's parent node.
                while (node != null)
                {
                    parent = node;
                    if (node.Data > newNode.Data)
                        node = node.Left;
                    else
                        node = node.Right;
                }

                //Insert at right or left.
                if (parent.Data > newNode.Data)
                {
                    parent.Left = newNode;
                }
                else
                {
                    parent.Right = newNode;
                }
            }
        }

        //Avoids a few extra comparisons.
        public void InsertSlightlyFaster(int value)
        {
            var newNode = new BSTNode
            {
                Data = value
            };

            //Set new node as root node if tree is empty.
            if (Root == null)
                Root = newNode;
            else
            {
                var currentNode = Root;
                while (true)
                {
                    //New node needs to be inserted to right of current node.
                    if (currentNode.Data < newNode.Data)
                    {
                        //If current node has no right then set new node as right of current node and exit.
                        if (currentNode.Right == null)
                        {
                            currentNode.Right = newNode;
                            return;
                        }
                        //Else set current node as right of current node and continue traversal.
                        currentNode = currentNode.Right;
                    }
                    //New node needs to be inserted left of current node.
                    else
                    {
                        if (currentNode.Left == null)
                        {
                            currentNode.Left = newNode;
                            return;
                        }
                        currentNode = currentNode.Left;
                    }
                }
            }
        }

        //Find if a node in tree contains the passed value.
        public bool Lookup(int value)
        {
            if (Root == null)
                return false;

            var currentNode = Root;
            while (currentNode != null)
            {
                if (currentNode.Data == value)
                    return true;

                if (currentNode.Data > value)
                    currentNode = currentNode.Left;
                else currentNode = currentNode.Right;
            }

            return false;
        }
    }
}
